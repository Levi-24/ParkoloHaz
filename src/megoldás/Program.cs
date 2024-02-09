
using System;
using System.Collections.Generic;
using System.Text;

namespace Parkolohaz
{
    /// <summary>
    /// chatGPT által létrehozott kód, amit javítani kellett több ponton
    /// </summary>
    class Program
    {
        #region metódusok
        //8. feladat
        static string LegkevesebbAuto(List<Szint> szintek)
        {
            var legkevesebb = szintek.Min(szint => szint.Szektorok.Sum());
            var legkevesebbSzint = szintek.Find(szint => szint.Szektorok.Sum() == legkevesebb);
            return legkevesebbSzint.Nev;
        }

        //9. feladat
        static string VanUresSzektor(List<Szint> szintek)
        {
            string eredmeny = String.Empty; 
            foreach (var item in szintek) 
            {
                if (item.Szektorok.Contains(0)) 
                {
                    eredmeny += item.Nev;
                    for (int i = 0; i < item.Szektorok.Count; i++)
                    {
                        if (item.Szektorok[i] == 0)
                        {
                            eredmeny += $" {i + 1}";
                        }
                    }
                    eredmeny += "\n";
                }
            }
            return eredmeny;
        }

        //11. feladat
        static string EgyAutoSzintek(List<Szint> szintek)
        {
            string eredmeny = String.Empty;
            for (int i = 0; i < szintek.Count; i++)
            {   
                if (szintek[i].Szektorok.Contains(1))
                { 
                    eredmeny += szintek[i].Nev;
                    for (int j = 0; j < szintek[i].Szektorok.Count; j++)
                    {
                        if (szintek[i].Szektorok[j] == 1)
                        {
                            eredmeny += $" - {j + 1}"; 
                        }
                    }
                    eredmeny += "\n";
                }                
            }
            return eredmeny;
        }

        //12. feladat
        static (bool, int) LegtobbAutoLegfelsoSzinten(List<Szint> szintek)
        {
            var legtobbAuto = szintek.Max(szint => szint.Szektorok.Sum());
            var legfelsoszint = szintek.Last();
            return (legfelsoszint.Szektorok.Sum() == legtobbAuto, legtobbAuto);
        }

        //13. feladat
        static void SzabadHelyekKiirasa(List<Szint> szintek)
        {
            using (StreamWriter sw = new StreamWriter(path: @"..\..\..\SRC\egyAuto.txt", append: true, encoding: Encoding.UTF8))
            {
                sw.WriteLine("13. feladat:");
                for (int i = 0; i < szintek.Count; i++)
                {
                    int szabadHelyek = 15 * 6 - szintek[i].Szektorok.Sum();
                    sw.WriteLine($"{i + 1}. szint: {szabadHelyek} szabad hely");
                }
            }
        }
        #endregion
        static void Main()
        {
            List<Szint> szintek = new List<Szint>();
            foreach (var sor in File.ReadAllLines(@"..\..\..\SRC\parkolohaz.txt"))
            {
                szintek.Add(new Szint(sor));
            }

            Console.WriteLine("\t\tSzint neve\t1.szektor 2.szektor 3.szektor 4.szektor 5.szektor 6.szektor");
            for (int i = 0; i < szintek.Count; i++)
            {
                Console.WriteLine($"{i + 1}.szint\t\t{szintek[i]}");
            }

            Console.WriteLine($"8. Feladat: Legkevesebb autóval rendelkező szint: {LegkevesebbAuto(szintek)}");

            Console.WriteLine($"9. Feladat: Van-e olyan szektor, ahol nincs autó ");
            var uresSzektorok = VanUresSzektor(szintek);
            if (uresSzektorok != "")
            {
                Console.WriteLine(uresSzektorok);
            }
            else
            {
                Console.WriteLine("Nincs olyan szektor, ahol nincs autó.");                
            }

            double atlagAutok = szintek.SelectMany(szint => szint.Szektorok).Average();
            Console.WriteLine($"10. Feladat: Átlagos autószám: {atlagAutok:F2}");
            Console.WriteLine($"    Átlagos autószám: {szintek.Sum(szint => szint.Szektorok.Count(x => x == atlagAutok))}");
            Console.WriteLine($"    Átlag alatti autószám: {szintek.Sum(szint => szint.Szektorok.Count(x => x < atlagAutok))}");
            Console.WriteLine($"    Átlag feletti autószám: {szintek.Sum(szint => szint.Szektorok.Count(x => x > atlagAutok))}");

            Console.WriteLine($"11. Feladat:fájlbaírás - Szintek és szektorok, ahol csak 1-1 autó van");
            string egyAutoSzintek = EgyAutoSzintek(szintek);
            using (StreamWriter sw = new StreamWriter(@"..\..\..\SRC\egyAuto.txt"))
            {
                sw.Write(egyAutoSzintek);
            }

            (bool allitas, int legtobbAutoLegfelsoSzinten) = LegtobbAutoLegfelsoSzinten(szintek);
            if (allitas)
            {
                Console.WriteLine("12. Feladat: Igen, a legfelső szinten van a legtöbb autó.");
            }
            else
            {
                Console.WriteLine($"12. Feladat: Nem, a legtöbb autó nem a legfelső szinten található, hanem a {legtobbAutoLegfelsoSzinten} szinten.");
            }

            Console.WriteLine("13. Feladat: Szabad helyek számának kiírása fájlba");
            SzabadHelyekKiirasa(szintek);

            int osszesSzabadHely = szintek.Sum(szint => 6 * 15 - szint.Szektorok.Sum());
            Console.WriteLine($"14. Feladat: Jelenleg összesen {osszesSzabadHely} szabad hely van a parkolóházban.");
        }
    }
}


