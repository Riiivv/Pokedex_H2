using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokédex_H2.Models
{
    // Pokédex-klassen håndterer indlæsning, gemning og manipulation af Pokémon-data
    public class Pokédex
    {
        // Filsti, hvor Pokémon-data gemmes
        private string filePath;

        // Liste over alle Pokémon, der er indlæst fra filen
        private List<Pokémon> pokemonList;

        // Konstruktør: Initialiserer Pokédex med filstien og indlæser Pokémon-data
        public Pokédex(string filePath)
        {
            this.filePath = filePath; // Gemmer filstien
            pokemonList = LoadPokemonData(); // Indlæser data fra filen
        }

        // Metode: Indlæs Pokémon-data fra CSV-filen
        public List<Pokémon> LoadPokemonData()
        {
            var list = new List<Pokémon>(); // Initialiser en tom liste

            // Kontroller, om filen eksisterer
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath).Skip(1); // Spring header-linjen over
                foreach (var line in lines)
                {
                    var data = line.Split(','); // Split linjen i felter baseret på komma
                    if (data.Length == 4)
                    {
                        // Opret en Pokémon og tilføj den til listen
                        list.Add(new Pokémon(
                            int.Parse(data[0]), // ID
                            data[1],            // Navn
                            data[2],            // Type
                            int.Parse(data[3])  // Styrkeniveau
                        ));
                    }
                }
            }

            return list; // Returner listen over Pokémon
        }

        // Metode: Gem Pokémon-data til CSV-filen
        public void SavePokemonData()
        {
            // Header-linje for CSV-filen
            var lines = new List<string> { "ID,Name,Type,StrengthLevel" };
            // Konverter hver Pokémon til en linje i CSV-format
            lines.AddRange(pokemonList.Select(p => $"{p.Id},{p.Name},{p.Type},{p.StrengthLevel}"));
            // Skriv alle linjer til filen
            File.WriteAllLines(filePath, lines);
        }

        // Metode: Tilføj en ny Pokémon til listen og gem den i filen
        public void AddPokemon(Pokémon pokemon)
        {
            // Generer et nyt ID baseret på det højeste ID i listen
            pokemon.Id = pokemonList.Count > 0 ? pokemonList.Max(p => p.Id) + 1 : 1;
            // Tilføj Pokémon til listen
            pokemonList.Add(pokemon);
            // Gem ændringer i filen
            SavePokemonData();
        }

        // Metode: Vis alle Pokémon med pagination
        public void ListAllPokemon(int pageSize)
        {
            int totalPages = (int)Math.Ceiling((double)pokemonList.Count / pageSize); // Beregn det samlede antal sider
            int currentPage = 1; // Start med den første side

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Alle Pokémon - Side {currentPage} af {totalPages}:\n");

                // Hent Pokémon til den aktuelle side
                var paginatedList = pokemonList
                    .Skip((currentPage - 1) * pageSize) // Spring tidligere sider over
                    .Take(pageSize) // Tag kun det angivne antal Pokémon
                    .ToList();

                // Vis Pokémon på den aktuelle side
                foreach (var pokemon in paginatedList)
                {
                    Console.WriteLine($"ID: {pokemon.Id}, Name: {pokemon.Name}, Type: {pokemon.Type}, Strength: {pokemon.StrengthLevel}");
                }

                Console.WriteLine("\nBrug venstre/højre piletaster til at navigere. Tryk 'Escape' for at vende tilbage.");

                // Læs brugerens tastetryk
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.RightArrow && currentPage < totalPages)
                {
                    currentPage++; // Gå til næste side
                }
                else if (key == ConsoleKey.LeftArrow && currentPage > 1)
                {
                    currentPage--; // Gå til forrige side
                }
                else if (key == ConsoleKey.Escape)
                {
                    break; // Afslut navigationen
                }
            }
        }

        // Metode: Søg efter Pokémon baseret på navn eller type
        public void SearchPokemon(string query)
        {
            // Filtrér Pokémon, der matcher søgeforespørgslen
            var results = pokemonList
                .Where(p => p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            p.Type.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Hvis der er resultater, vis dem
            if (results.Count > 0)
            {
                foreach (var pokemon in results)
                {
                    Console.WriteLine(pokemon);
                }
            }
            else
            {
                Console.WriteLine("Ingen resultater fundet."); // Ingen matches
            }
        }
    }
}
