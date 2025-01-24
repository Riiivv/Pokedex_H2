# Pokédex Application

## Beskrivelse

Pokédex-applikationen er en konsolbaseret applikation skrevet i C#. Den fungerer som en digital Pokédex, der giver brugere mulighed for at:

- **Logge ind** eller **registrere nye brugere**.
- **Se og søge** i listen over alle Pokémon.
- For loggede brugere: **Tilføje**, **redigere** eller **slette Pokémon**.
- Gemme og indlæse Pokémon-data fra en CSV-fil.

Applikationen er designet med et brugervenligt navigationssystem og sikrer sikkerhed gennem hash-baseret adgangskodehåndtering.

---

## Funktioner

### 1. Brugerfunktioner

- **Login**: Brugeren kan logge ind ved at indtaste et brugernavn og en adgangskode.
- **Registrering**: Nye brugere kan registrere sig med et brugernavn og adgangskode.
- **Sikkerhed**: Adgangskoder gemmes som hashes ved hjælp af SHA-256.

### 2. Pokémon Management

- **Se alle Pokémon**: Viser listen over alle Pokémon med pagination.
- **Søg efter Pokémon**: Søg baseret på navn eller type.
- **Tilføj Pokémon**: Loggede brugere kan tilføje nye Pokémon.
- **Rediger Pokémon**: Loggede brugere kan redigere eksisterende Pokémon.
- **Slet Pokémon**: Loggede brugere kan slette Pokémon fra listen.

---

## Teknisk Arkitektur

### 1. Klasser

- **`PokédexManager`**: Håndterer CRUD-operationer (Create, Read, Update, Delete) for Pokémon-data.
- **`Pokémon`**: Repræsenterer en individuel Pokémon med egenskaber som ID, navn, type og styrkeniveau.
- **`User`**: Håndterer bruger-login og registrering.
- **`Menu`**: Giver en interaktiv konsolmenu, der bruger piletaster til navigation.

### 2. Datahåndtering

- **Pokémon-data**: Gemmes i en CSV-fil (`pokemon.csv`).
- **Brugerdata**: Gemmes i en tekstfil (`users.txt`) som brugernavn og hashed adgangskode.

### 3. Sikkerhed

- Adgangskoder hashes ved hjælp af SHA-256 og gemmes i en sikret form for at forhindre uautoriseret adgang.

---

## Installationsguide

### 1. Krav

- **.NET SDK** (version 6.0 eller nyere).
- En IDE som Visual Studio eller Visual Studio Code.

### 2. Opsætning

1. Klon eller download projektet til din lokale maskine.
2. Åbn projektet i din IDE.
3. Kontroller, at filerne `pokemon.csv` og `users.txt` findes i rodmappen. Hvis de ikke findes, opretter applikationen dem automatisk.
4. Kør applikationen.

---

## Brugervejledning

### Hovedmenu

Ved start præsenteres brugeren for en hovedmenu:

1. **Log ind**: Log ind som eksisterende bruger.
2. **Registrer ny bruger**: Registrér en ny bruger.
3. **Se Pokémon-liste**: Se og søg blandt Pokémon (kræver ikke login).
4. **Afslut**: Luk programmet.

### Pokédex Menu (for loggede brugere)

- **Tilføj Pokémon**: Indtast navn, type og styrkeniveau.
- **Rediger Pokémon**: Vælg en eksisterende Pokémon og opdater dens oplysninger.
- **Slet Pokémon**: Fjern en Pokémon fra listen.
- **Se alle Pokémon**: Vis Pokémon med mulighed for at navigere mellem sider.
- **Søg efter Pokémon**: Indtast navn eller type for at finde specifikke Pokémon.

---

## Eksempeldata

### pokemon.csv

```csv
ID,Name,Type,StrengthLevel
1,Bulbasaur,Grass/Poison,55
2,Charmander,Fire,60
3,Squirtle,Water,50
...
```

### users.txt

```txt
Ash,5e884898da28047151d0e56f8dc6292773603d0d6aabbdd64567e84f4a6a7b88
Misty,5e884898da28047151d0e56f8dc6292773603d0d6aabbdd64567e84f4a6a7b88
```

---

## Udvidelsesmuligheder

- Tilføj support til flere typer af Pokémon-data (f.eks. evner og udviklingstrin).
- Implementer avanceret søgning baseret på flere kriterier.
- Tilføj muligheden for at gemme data i en database frem for en CSV-fil.
- Implementer en GUI til at erstatte konsolgrænsefladen.

---
Tak for at bruge Pokédex-applikationen! Gotta catch 'em all! 🐉

hvordan laver jeg en readme fil på github?

