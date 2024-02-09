namespace Parkolohaz
{
    class Szint
    {
        public string Nev { get; set; }
        public List<int> Szektorok { get; set; }

        public Szint(string sor)
        {
            var adatok = sor.Split("; ");
            Nev = adatok[0];
            Szektorok = new List<int>();
            for (int i = 1; i < adatok.Length; i++)
            {
                if (int.TryParse(adatok[i], out int szektor))
                {
                    Szektorok.Add(szektor);
                }
            }
        }

        public override string ToString()
        {
            string szektorAdatok = string.Join("\t", Szektorok);
            return $"{Nev,-11}\t{szektorAdatok}";
        }
    }
}


