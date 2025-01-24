using System;

public class Menu
{
    // Array, der indeholder menumulighederne
    private string[] options;

    // Index for den aktuelt valgte menuindstilling
    private int selectedIndex;

    // Konstruktør: Initialiserer menuen med de givne muligheder
    public Menu(string[] options)
    {
        this.options = options; // Gemmer menumulighederne
        selectedIndex = 0; // Standardvalg er den første mulighed
    }

    // Metode: Viser menuen på konsollen
    public void DisplayMenu()
    {
        Console.Clear(); // Rydder konsollen
        Console.WriteLine("Velkommen til Pokédex! Naviger med piletasterne og vælg med Enter.\n");

        // Iterér gennem alle menumuligheder
        for (int i = 0; i < options.Length; i++)
        {
            // Fremhæv den aktuelt valgte mulighed
            if (i == selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Black; // Sort tekst
                Console.BackgroundColor = ConsoleColor.White; // Hvid baggrund
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White; // Hvid tekst
                Console.BackgroundColor = ConsoleColor.Black; // Sort baggrund
            }

            // Skriv menuindstillingen
            Console.WriteLine(options[i]);
        }

        Console.ResetColor(); // Nulstil konsolfarverne
    }

    // Metode: Kører menuen og håndterer brugerens input
    public int Run()
    {
        ConsoleKey keyPressed; // Bruges til at gemme den tast, brugeren trykker
        do
        {
            DisplayMenu(); // Vis menuen
            var keyInfo = Console.ReadKey(true); // Læs brugerens tastetryk uden at vise det på skærmen
            keyPressed = keyInfo.Key; // Gem den tast, der blev trykket

            // Håndter navigation opad
            if (keyPressed == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                // Hvis index går under 0, spring til den sidste mulighed
            }
            // Håndter navigation nedad
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex + 1) % options.Length;
                // Hvis index går over det sidste element, spring til den første mulighed
            }
        } while (keyPressed != ConsoleKey.Enter); // Fortsæt indtil Enter-tasten trykkes

        return selectedIndex; // Returner index for den valgte mulighed
    }
}
