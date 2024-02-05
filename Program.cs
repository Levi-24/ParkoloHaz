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