using ParkoloHaz;

List<Emelet> parkoloHaz = new List<Emelet>();
int sorszam = 1;
using (var sr = new StreamReader("../../../src/parkolohaz.txt"))
{
    while (!sr.EndOfStream)
    {
        parkoloHaz.Add(new Emelet(sr.ReadLine(), sorszam));
        sorszam++;
    }
}

Console.WriteLine("Szint neve\t Szektor1\tSzektor2\tSzektor3\tSzektor4\tSzektor5\tSzektor6");
foreach (var emelet in parkoloHaz) Console.WriteLine(emelet);

Console.WriteLine();
Console.WriteLine("Feladat 8.");
var legkevesebbAutoEmelet = parkoloHaz.OrderBy(x => x.szektorAdatok.Sum()).First();
Console.WriteLine(legkevesebbAutoEmelet.emeletNev);

Console.WriteLine();
Console.WriteLine("Feladat 9.");
var automentesSzektor = parkoloHaz.SelectMany(emelet => emelet.szektorAdatok
    .Select((szektor, index) => new { Emelet = emelet.sorszam, Szektor = szektor, SzektorIndex = index + 1 }))
    .FirstOrDefault(item => item.Szektor == 0);

if (automentesSzektor != null) Console.WriteLine($"Emelet sorszáma: {automentesSzektor.Emelet}, Szektor sorszáma: {automentesSzektor.SzektorIndex}");
else Console.WriteLine("Nincs autómentes szektor az emeleteken!");

Console.WriteLine();
Console.WriteLine("Feladat 10.");
var atlagAutoMenny = Math.Round(parkoloHaz.Average(x => x.szektorAdatok.Average()), 2);
Console.WriteLine(atlagAutoMenny);

var atlagAutoKeres = parkoloHaz.SelectMany(emelet => emelet.szektorAdatok)
    .Count(szektorAutok => szektorAutok == atlagAutoMenny);
Console.WriteLine(atlagAutoKeres);

var kisAtlagAutoKeres = parkoloHaz.SelectMany(emelet => emelet.szektorAdatok)
    .Count(szektorAutok => szektorAutok < atlagAutoMenny);
Console.WriteLine(kisAtlagAutoKeres);

var nagyAtlagAutoKeres = parkoloHaz.SelectMany(emelet => emelet.szektorAdatok)
    .Count(szektorAutok => szektorAutok > atlagAutoMenny);
Console.WriteLine(nagyAtlagAutoKeres);

Console.WriteLine();
Console.WriteLine("Feladat 11.");
Console.WriteLine("Fájlba irás");
var egyAutoSzektorok = parkoloHaz
    .SelectMany(emelet => emelet.szektorAdatok.Select((szektor, index) => new { Emelet = emelet.sorszam, Szektor = index + 1, AutoSzam = szektor }))
    .Where(szektor => szektor.AutoSzam == 1);

using StreamWriter sw = new StreamWriter("../../../src/1-1.txt");
foreach (var szektor in egyAutoSzektorok) sw.WriteLine($"{szektor.Emelet} - {szektor.Szektor}");

Console.WriteLine();
Console.WriteLine("Feladat 12.");
var legtobbAutoEmelet = parkoloHaz.OrderByDescending(emelet => emelet.szektorAdatok.Sum()).First();

if (legtobbAutoEmelet.sorszam == 1) Console.WriteLine("Igaz, hogy a legfelső emeleten van a legtöbb autó.");
else Console.WriteLine($"A legtöbb autó a {legtobbAutoEmelet.sorszam}. {legtobbAutoEmelet.emeletNev} emeleten van, {legtobbAutoEmelet.szektorAdatok.Sum()}db autó van az emeleten.");

Console.WriteLine();
Console.WriteLine("Feladat 13.");
Console.WriteLine("Fájlba irás");
var szabadHelyek = parkoloHaz.Select(x => 90 - x.szektorAdatok.Sum()).ToList();

sw.WriteLine("13. Feladat:");
foreach (var hely in szabadHelyek) sw.WriteLine(hely);

Console.WriteLine();
Console.WriteLine("Feladat 14.");
double szabadParkoloHaz = szabadHelyek.Sum();
Console.WriteLine(szabadParkoloHaz);