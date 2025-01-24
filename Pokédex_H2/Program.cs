using Pokédex_H2.Models;

class Program
{
    static void Main(string[] args)
    {
        // Filsti for at gemme og hente Pokémon-data
        string filePath = "pokemon.csv";

        // Initialiser Pokédex og brugerhåndtering
        var pokedex = new PokédexManager(filePath);
        var userManager = new User();
        bool loggedIn = false;

        while (true)
        {
            // Hovedmenu for programmet
            Console.Clear();
            Console.WriteLine("Velkommen til Pokédex!");
            Console.WriteLine("1. Log ind");
            Console.WriteLine("2. Registrér ny bruger");
            Console.WriteLine("3. Se Pokémon-liste (kun søgning og visning)");
            Console.WriteLine("4. Afslut");

            var choice = Console.ReadLine();
            if (choice == "1")
            {
                // Log ind
                Console.Write("Brugernavn: ");
                string username = Console.ReadLine();
                Console.Write("Adgangskode: ");
                string password = Console.ReadLine();

                if (userManager.Login(username, password))
                {
                    Console.WriteLine("Login succesfuldt!");
                    loggedIn = true;
                    RunPokedexMenu(pokedex, loggedIn);
                }
                else
                {
                    Console.WriteLine("Forkert brugernavn eller adgangskode.");
                }
                Console.ReadKey();
            }
            else if (choice == "2")
            {
                // Registrer ny bruger
                Console.Write("Vælg et brugernavn: ");
                string username = Console.ReadLine();
                Console.Write("Vælg en adgangskode: ");
                string password = Console.ReadLine();

                userManager.Register(username, password);
                Console.ReadKey();
            }
            else if (choice == "3")
            {
                // Vis Pokédex-menu uden login
                RunPokedexMenu(pokedex, loggedIn: false);
            }
            else if (choice == "4")
            {
                // Afslut programmet
                Console.WriteLine("Farvel!");
                break;
            }
            else
            {
                // Ugyldigt valg
                Console.WriteLine("Ugyldigt valg. Prøv igen.");
                Console.ReadKey();
            }
        }
    }

    static void RunPokedexMenu(PokédexManager pokedex, bool loggedIn)
    {
        // Menuvalg afhænger af login-status
        string[] menuOptions = loggedIn
            ? new[] { "1. Tilføj Pokémon", "2. Rediger Pokémon", "3. Slet Pokémon", "4. Se alle Pokémon", "5. Søg efter Pokémon", "6. Log ud" }
            : new[] { "1. Se alle Pokémon", "2. Søg efter Pokémon", "3. Tilbage til hovedmenu" };

        while (true)
        {
            // Vis menuen
            Console.Clear();
            Console.WriteLine("Pokédex Menu:");
            for (int i = 0; i < menuOptions.Length; i++)
                Console.WriteLine($"{i + 1}. {menuOptions[i]}");

            var choice = Console.ReadLine();
            if (!loggedIn)
            {
                // Handlinger for ikke-loggede brugere
                if (choice == "1")
                {
                    pokedex.ListAllPokemon(10); // Vis 10 Pokémon per side
                    Console.ReadKey();
                }
                else if (choice == "2")
                {
                    Console.Write("Indtast navn eller type: ");
                    string query = Console.ReadLine();
                    pokedex.SearchPokemon(query);
                    Console.ReadKey();
                }
                else if (choice == "3")
                {
                    break; // Tilbage til hovedmenuen
                }
            }
            else
            {
                // Handlinger for loggede brugere
                switch (choice)
                {
                    case "1":
                        // Tilføj en ny Pokémon
                        Console.Write("Navn: ");
                        string name = Console.ReadLine();
                        Console.Write("Type: ");
                        string type = Console.ReadLine();
                        Console.Write("Styrkeniveau: ");
                        int strength = int.Parse(Console.ReadLine());
                        pokedex.AddPokemon(new Pokémon(0, name, type, strength));
                        Console.ReadKey();
                        break;
                    case "2":
                        // Rediger en eksisterende Pokémon
                        Console.Write("Indtast ID for Pokémon, der skal redigeres: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Nyt navn: ");
                        string newName = Console.ReadLine();
                        Console.Write("Ny type: ");
                        string newType = Console.ReadLine();
                        Console.Write("Nyt styrkeniveau: ");
                        int newStrength = int.Parse(Console.ReadLine());
                        pokedex.EditPokemon(id, new Pokémon(id, newName, newType, newStrength));
                        Console.ReadKey();
                        break;
                    case "3":
                        // Slet en Pokémon
                        Console.Write("Indtast ID for Pokémon, der skal slettes: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        pokedex.DeletePokemon(deleteId);
                        Console.ReadKey();
                        break;
                    case "4":
                        // Vis alle Pokémon
                        pokedex.ListAllPokemon(10);
                        Console.ReadKey();
                        break;
                    case "5":
                        // Søg efter en Pokémon
                        Console.Write("Indtast navn eller type: ");
                        string searchQuery = Console.ReadLine();
                        pokedex.SearchPokemon(searchQuery);
                        Console.ReadKey();
                        break;
                    case "6":
                        // Log ud
                        return;
                    default:
                        Console.WriteLine("Ugyldigt valg. Prøv igen.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}