using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class User
{
    // Stien til filen, der gemmer brugernavne og hashede adgangskoder
    private string usersFile = "users.txt";

    // Metode: Log ind en bruger ved at kontrollere brugernavn og adgangskode
    public bool Login(string username, string password)
    {
        // Hvis filen ikke findes, kan login ikke gennemføres
        if (!File.Exists(usersFile)) return false;

        // Læs alle linjer fra filen
        var lines = File.ReadAllLines(usersFile);
        // Hash adgangskoden for at matche den med gemte data
        var hashedPassword = HashPassword(password);

        // Tjek hver linje for et matchende brugernavn og adgangskode
        foreach (var line in lines)
        {
            var parts = line.Split(','); // Opdel linjen i brugernavn og adgangskode
            if (parts.Length == 2 && parts[0] == username && parts[1] == hashedPassword)
            {
                return true; // Login succesfuldt
            }
        }

        return false; // Forkert brugernavn eller adgangskode
    }

    // Metode: Registrér en ny bruger med brugernavn og adgangskode
    public void Register(string username, string password)
    {
        // Hvis filen ikke findes, opret den
        if (!File.Exists(usersFile)) File.Create(usersFile).Close();

        // Hash adgangskoden før lagring
        var hashedPassword = HashPassword(password);
        // Tilføj det nye brugernavn og hashede adgangskode til filen
        File.AppendAllLines(usersFile, new[] { $"{username},{hashedPassword}" });
        Console.WriteLine("Bruger registreret!");
    }

    // Privat metode: Hash adgangskoden ved hjælp af SHA-256
    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            // Konverter adgangskoden til en byte-array og beregn hash
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();
            // Konverter hver byte til en hexadecimal streng
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString(); // Returner den hashede adgangskode som en streng
        }
    }
}
