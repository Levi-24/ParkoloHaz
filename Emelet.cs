using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkoloHaz
{
    internal class Emelet
    {
        public int sorszam { get; set; }
        public string emeletNev { get; set; }
        public List<double> szektorAdatok { get; set; }

        public Emelet(string r, int sorszam)
        {
            this.sorszam = sorszam;
            var adatok = r.Split(';');
            this.emeletNev = adatok[0].PadRight(10);
            this.szektorAdatok = new List<double>();
            for (int i = 1; i < adatok.Length; i++) szektorAdatok.Add(double.Parse(adatok[i]));
        }

        public override string ToString()
        {
            return $"{sorszam}. {emeletNev} \t {string.Join("\t\t", szektorAdatok)}";
        }
    }
}
