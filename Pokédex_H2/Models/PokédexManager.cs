using Pokédex_H2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class PokédexManager
{
    private string filePath; // Stien til CSV-filen, hvor Pokémon-data gemmes
    private List<Pokémon> pokemonList; // Liste over alle Pokémon

    // Konstruktør: Initialiserer PokédexManager med en filsti og indlæser data fra filen
    public PokédexManager(string filePath)
    {
        this.filePath = filePath;
        pokemonList = LoadPokemonData();
    }

    // Viser alle Pokémon med pagination
    public void ListAllPokemon(int pageSize)
    {
        int totalPages = (int)Math.Ceiling((double)pokemonList.Count / pageSize); // Beregn det samlede antal sider
        int currentPage = 1;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Alle Pokémon - Side {currentPage} af {totalPages}:\n");

            // Hent Pokémon til den aktuelle side
            var paginatedList = pokemonList
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
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

    // Søger efter Pokémon baseret på navn eller type
    public void SearchPokemon(string query)
    {
        Console.Clear();
        Console.WriteLine($"Søger efter Pokémon med navn eller type '{query}':\n");

        // Find Pokémon, hvor navn eller type matcher søgeforespørgslen
        var results = pokemonList
            .Where(p => p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                        p.Type.Contains(query, StringComparison.OrdinalIgnoreCase))
            .ToList();

        // Vis søgeresultater eller en besked, hvis ingen resultater blev fundet
        if (results.Any())
        {
            foreach (var pokemon in results)
            {
                Console.WriteLine($"ID: {pokemon.Id}, Name: {pokemon.Name}, Type: {pokemon.Type}, Strength: {pokemon.StrengthLevel}");
            }
        }
        else
        {
            Console.WriteLine("Ingen resultater fundet.");
        }
    }

    // Indlæser Pokémon-data fra CSV-filen
    private List<Pokémon> LoadPokemonData()
    {
        var list = new List<Pokémon>();
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath).Skip(1); // Spring header-linjen over
            foreach (var line in lines)
            {
                var data = line.Split(','); // Opdel data med komma
                list.Add(new Pokémon(int.Parse(data[0]), data[1], data[2], int.Parse(data[3])));
            }
        }
        return list; // Returner listen over Pokémon
    }

    // Tilføjer en ny Pokémon til listen og gemmer ændringerne i CSV-filen
    public void AddPokemon(Pokémon pokemon)
    {
        pokemon.Id = pokemonList.Count > 0 ? pokemonList.Max(p => p.Id) + 1 : 1; // Generer unikt ID
        pokemonList.Add(pokemon);
        SavePokemonData(); // Gem opdateringer
        Console.WriteLine($"Pokémon '{pokemon.Name}' tilføjet.");
    }

    // Sletter en Pokémon fra listen baseret på ID
    public void DeletePokemon(int id)
    {
        var pokemon = pokemonList.FirstOrDefault(p => p.Id == id); // Find Pokémon med det specifikke ID
        if (pokemon != null)
        {
            pokemonList.Remove(pokemon); // Fjern Pokémon fra listen
            SavePokemonData(); // Gem opdateringer
            Console.WriteLine($"Pokémon '{pokemon.Name}' slettet.");
        }
        else
        {
            Console.WriteLine("Pokémon ikke fundet.");
        }
    }

    // Redigerer en Pokémon baseret på ID og de opdaterede oplysninger
    public void EditPokemon(int id, Pokémon updatedPokemon)
    {
        var pokemon = pokemonList.FirstOrDefault(p => p.Id == id); // Find Pokémon med det specifikke ID
        if (pokemon != null)
        {
            // Opdater Pokémon-egenskaber
            pokemon.Name = updatedPokemon.Name;
            pokemon.Type = updatedPokemon.Type;
            pokemon.StrengthLevel = updatedPokemon.StrengthLevel;
            SavePokemonData(); // Gem opdateringer
            Console.WriteLine($"Pokémon '{pokemon.Name}' opdateret.");
        }
        else
        {
            Console.WriteLine("Pokémon ikke fundet.");
        }
    }

    // Gemmer alle Pokémon-data i CSV-filen
    private void SavePokemonData()
    {
        var lines = new List<string> { "ID,Name,Type,StrengthLevel" }; // Header-linje
        lines.AddRange(pokemonList.Select(p => $"{p.Id},{p.Name},{p.Type},{p.StrengthLevel}")); // Konverter data til CSV-format
        File.WriteAllLines(filePath, lines); // Skriv til filen
    }
}
