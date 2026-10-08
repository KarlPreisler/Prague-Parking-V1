internal class Program
{
    static void Main()
    {
        Console.WriteLine("Prague Parking V1");
    }

    const string Bil = "CAR";

    const string MC = "MC";

    const int TotalaPlatser = 100;

    const int TotalRegLängd = 10;

    const char Typskiljetecken = '#';

    const char McSkiljetecken = '|';

    const string TeckenFörReg = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ÜÇĞŞİČĆĐŠŽØÆ";

    static string[] parkeringshus = new string[TotalaPlatser];

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
}
