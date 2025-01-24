using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokédex_H2.Models
{
    // Definerer klassen Pokémon, som repræsenterer en Pokémon med dens egenskaber
    public class Pokémon
    {
        // Unikt ID for hver Pokémon
        public int Id { get; set; }

        // Navnet på Pokémon
        public string Name { get; set; }

        // Typen af Pokémon (fx Fire, Water, Grass)
        public string Type { get; set; }

        // Styrkeniveauet for Pokémon
        public int StrengthLevel { get; set; }

        // Konstruktør: Initialiserer en ny Pokémon med ID, navn, type og styrkeniveau
        public Pokémon(int id, string name, string type, int strengthLevel)
        {
            Id = id; // Tildeler unikt ID
            Name = name; // Tildeler navn
            Type = type; // Tildeler type
            StrengthLevel = strengthLevel; // Tildeler styrkeniveau
        }

        // Overrider ToString-metoden for at returnere en læsbar repræsentation af Pokémon
        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Type: {Type}, Strength: {StrengthLevel}";
            // Eksempeloutput: "ID: 1, Name: Pikachu, Type: Electric, Strength: 50"
        }
    }
}
