using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BierenServiceLibrary
{
    public class BierenService : IBierenService
    {
        private static Bier[] bieren = {
                                           new Bier{BierNr=11,Naam="Adler",Alcohol=6.75M},
                                           new Bier{BierNr=41,Naam="Anglo pils",Alcohol=4.8M},
                                           new Bier{BierNr=166,Naam="Boeg pils",Alcohol=5M}
                                     };
        public int GetTotaalAantalBieren()
        {
            return bieren.Count();
        }

        public int GetAantalBierenTussenAlcohol(decimal van, decimal tot)
        {
            return (from bier in bieren
                    where bier.Alcohol >= van && bier.Alcohol <= tot
                    select bier).Count();
        }

        public List<Bier> GetBierenMetWoord(string woord)
        {
            var woordInKleineLetters = woord.ToLower();
            return (from bier in bieren
                    where bier.Naam.ToLower().Contains(woordInKleineLetters)
                    select bier).ToList();
        }


        public List<Bier> GetStrafsteBieren()
        {
            return (from bier in bieren
                    where bier.Alcohol == bieren.Max(eenBier => eenBier.Alcohol)
                    select bier).ToList();
        }
    }
}
