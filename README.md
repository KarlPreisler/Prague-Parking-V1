# Prague Parking V1

Inlämningsuppgift 1. Ett konsolprogram i C# för valet parking vid slottet i Prag. Personalen kan parkera, flytta, hämta ut och söka efter fordon via en enkel textmeny.

Gjort av **Karl Preisler** och **Gustav Myhre Gertz**.

## Funktioner

- **Parkera fordon:** bil eller MC. Programmet säger vilken plats fordonet ska köras till. En MC ställs i första hand bredvid en annan ensam MC.
- **Flytta fordon:** manuellt till en annan plats, om fordonet får plats där.
- **Hämta ut fordon:** tar bort fordonet från parkeringen.
- **Sök fordon:** visar vilken plats ett regnr står på.
- **Visa parkeringshuset:** färgat rutnät över alla 100 platser (grön = tom, gul = en MC, röd = full) och en lista över upptagna platser.

## Starta programmet

**Krav:** .NET 10 SDK och Visual Studio 2026 (eller annan miljö med .NET 10).

1. Klona repot eller ladda ner det som ZIP.
2. Öppna `Prague Parking V1/Prague Parking V1.slnx` i Visual Studio.
3. Tryck **F5** eller **Ctrl+F5**.

Utan Visual Studio kan programmet köras från repots rotmapp:

```bash
dotnet run --project "Prague Parking V1/Prague Parking V1"
```

Inga NuGet-paket eller andra inställningar behövs.
