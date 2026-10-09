internal class Program
{
    
    const string Bil = "CAR";

    const string MC = "MC";

    const int TotalaPlatser = 100;

    const int TotalRegLängd = 10;

    const char Typskiljetecken = '#';

    const char McSkiljetecken = '|';

    const string TeckenFörReg = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ÜÇĞŞİČĆĐŠŽØÆ";

    static string[] parkeringshus = new string[TotalaPlatser];

    static void Main()
    {
        Console.Title = "Prague Parking V1";
        bool körProgrammet = true;

        while (körProgrammet)
        {
            VisaMeny();

            int menyval = LäsHeltal("Välj ett alternativ (0-5): ", 0, 5);

            switch (menyval)
            {
                case 0:
                    körProgrammet = false;
                    break;
            }

            if (körProgrammet)
            {
                VäntaPåEnter();
            }
        }

        SkrivOK("Programmet avslutas. Tack och hej då!");
    }

    static void VisaMeny()
    {
        VisaRubrik("PRAGUE PARKING - HUVUDMENY");

        int lediga = RäknaTommaPlatser();

        int fordon = RäknaParkeradeFordon();

        SkrivInfo($"Tomma platser: {lediga} av {TotalaPlatser}   Parkerade fordon: {fordon}");

        Console.WriteLine();

        Console.WriteLine("1. Parkera fordon");
        Console.WriteLine("2. Flytta fordon");
        Console.WriteLine("3. Hämta ut fordon");
        Console.WriteLine("4. Sök fordon");
        Console.WriteLine("5. Visa parkeringshuset");
        Console.WriteLine("0. Avsluta");

        Console.WriteLine();
    }

    static void HanteraParkering()
    {
        VisaRubrik("PARKERA FORDON");

        string typ = LäsFordonstyp();

        if (typ == "")
        {
            SkrivInfo("Avbrutet. Inget fordon parkerades.");
            return;
        }

        string regnr = LäsRegnr("Registreringsnummer (tomt = avbryt): ");

        if (regnr == "")
        {
            SkrivInfo("Avbrutet. Inget fordon parkerades.");
            return;
        }

        if (SökFordon(regnr, out int befintligtIndex))
        {
            SkrivFel($"Ett fordon med regnr {regnr} står redan på plats {befintligtIndex + 1}.");
            return;
        }

        int index = ParkeraFordon(typ, regnr);

        if (index == -1)
        {
            SkrivFel($"Tyvärr, det finns ingen ledig plats för {TypTillText(typ)}.");
            return;
        }

        SkrivOK($"{TypTillText(typ)} {regnr} ska köras till plats {index + 1}.");
    }

    static int ParkeraFordon(string typ, string regnr)
    {
        int index = HittaLedigPlats(typ);

        if (index == -1)
        {
            return -1;
        }

        LäggTillFordon(index, SkapaFordon(typ, regnr));

        return index;
    }
    static string TypTillText(string typ)
    {
        return typ == Bil ? "Bil" : "MC";
    }

    static bool SökFordon(string regnr, out int platsIndex)
    {
        for (int index = 0; index < parkeringshus.Length; index++)
        {
            foreach (string fordon in DelaUppPlats(parkeringshus[index]))
            {
                if (HämtaRegnr(fordon) == regnr)
                {
                    platsIndex = index;
                    return true;
                }
            }
        }

        platsIndex = -1;

        return false;
    }

    static int HittaLedigPlats(string typ)
    {
        if (typ == MC)
        {
            int index = HittaEnsamMC();

            if (index != -1)
            {
                return index;
            }
        }

        return HittaTomPlats();
    }

    static int HittaEnsamMC()
    {
        for (int index = 0; index < parkeringshus.Length; index++)
        {
            if (ÄrEnsamMC(index))
            {
                return index;
            }
        }

        return -1;
    }

    static int HittaTomPlats()
    {
        for (int index = 0; index < parkeringshus.Length; index++)
        {
            if (ÄrPlatsTom(index))
            {
                return index;
            }
        }

        return -1;
    }

    static void LäggTillFordon(int index, string fordon)
    {
        if (ÄrPlatsTom(index))
        {
            parkeringshus[index] = fordon;
        }
        else
        {
            parkeringshus[index] = string.Join(McSkiljetecken, parkeringshus[index], fordon);
        }
    }


    static bool ÄrPlatsTom(int index)
    {
        return string.IsNullOrEmpty(parkeringshus[index]);
    }

    static bool ÄrEnsamMC(int index)
    {
        string[] fordonLista = DelaUppPlats(parkeringshus[index]);

        if (fordonLista.Length != 1)
        {
            return false;
        }

        return HämtaTyp(fordonLista[0]) == MC;
    }

    static int RäknaTommaPlatser()
    {
        int antal = 0;

        for (int index = 0; index < parkeringshus.Length; index++)
        {
            if (ÄrPlatsTom(index))
            {
                antal++;
            }
        }

        return antal;
    }

    static int RäknaParkeradeFordon()
    {
        int antal = 0;

        for (int index = 0; index < parkeringshus.Length; index++)
        {
            antal += DelaUppPlats(parkeringshus[index]).Length;
        }

        return antal;
    }

    static string SkapaFordon(string typ, string regnr)
    {
        return string.Join(Typskiljetecken, typ, regnr);
    }

    static string HämtaTyp(string fordon)
    {
        string[] delar = fordon.Split(Typskiljetecken);

        return delar[0];
    }

    static string HämtaRegnr(string fordon)
    {
        string[] delar = fordon.Split(Typskiljetecken);

        return delar[1];
    }

    static string[] DelaUppPlats(string innehåll)
    {
        if (string.IsNullOrEmpty(innehåll))
        {
            return new string[0];
        }

        return innehåll.Split(McSkiljetecken);
    }

    static int LäsHeltal(string ledtext, int min, int max)
    {
        while (true)
        {
            string text = LäsText(ledtext);

            if (int.TryParse(text, out int tal) && tal >= min && tal <= max)
            {
                return tal;
            }

            SkrivFel($"Felaktig inmatning. Skriv ett heltal mellan {min} och {max}.");
        }
    }

    static string LäsText(string ledtext)
    {
        Console.Write(ledtext);

        string text = Console.ReadLine() ?? "";

        return text.Trim();
    }

    static string LäsRegnr(string ledtext)
    {
        while (true)
        {
            string regnr = NormaliseraRegnr(LäsText(ledtext));

            if (regnr == "")
            {
                return "";
            }

            if (ÄrGiltigtRegnr(regnr))
            {
                return regnr;
            }

            SkrivFel($"Ogiltigt regnr. Högst {TotalRegLängd} tecken, vänligen försök igen.");
        }
    }

    static string LäsFordonstyp()
    {
        int val = LäsHeltal("Fordonstyp (1 = Bil, 2 = MC, 0 = avbryt): ", 0, 2);

        if (val == 1)
        {
            return Bil;
        }

        if (val == 2)
        {
            return MC;
        }

        return "";
    }

    static string NormaliseraRegnr(string text)
    {
        return TaBortMellanslag(text).ToUpper();
    }

    static string TaBortMellanslag(string text)
    {
        string resultat = "";

        foreach (char tecken in text)
        {
            if (tecken != ' ')
            {
                resultat += tecken;
            }
        }

        return resultat;
    }

    static bool ÄrGiltigtRegnr(string regnr)
    {
        if (string.IsNullOrEmpty(regnr))
        {
            return false;
        }

        if (regnr.Length > TotalRegLängd)
        {
            return false;
        }

        foreach (char tecken in regnr)
        {
            if (!ÄrTillåtetTecken(tecken))
            {
                return false;
            }
        }

        return true;
    }

    static bool ÄrTillåtetTecken(char tecken)
    {
        foreach (char tillåtet in TeckenFörReg)
        {
            if (tecken == tillåtet)
            {
                return true;
            }
        }

        return false;
    }

    static void VisaRubrik(string rubrik)
    {
        RensaSkärmen();

        SkrivFärgad($"===== {rubrik} =====", ConsoleColor.Cyan);

        Console.WriteLine();
    }

    static void RensaSkärmen()
    {
        if (!Console.IsOutputRedirected)
        {
            Console.Clear();
        }
    }

    static void VäntaPåEnter()
    {
        Console.WriteLine();

        LäsText("Tryck Enter för att återgå till menyn...");
    }

    static void SkrivFärgad(string text, ConsoleColor färg)
    {
        SkrivFärgadPåRad(text, färg);

        Console.WriteLine();
    }

    static void SkrivFärgadPåRad(string text, ConsoleColor färg)
    {
        Console.ForegroundColor = färg;

        Console.Write(text);

        Console.ResetColor();
    }

    static void SkrivFel(string meddelande)
    {
        SkrivFärgad(meddelande, ConsoleColor.Red);
    }

    static void SkrivOK(string meddelande)
    {
        SkrivFärgad(meddelande, ConsoleColor.Green);
    }

    static void SkrivInfo(string meddelande)
    {
        SkrivFärgad(meddelande, ConsoleColor.Yellow);
    }
}
