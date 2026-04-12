using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etkezesirend
{
    public class Desszertek
    {
        public int Id { get; set; }
        public string nev { get; set; }
        public double kaloria { get; set; }
        public double ar { get; set; }
        public bool vegan { get; set; }


        public Desszertek(int uid, string unev,double ukaloria, double uar,bool uvegan)
        {
            Id = uid;
            nev = unev;
            kaloria = ukaloria;
            ar = uar;
            vegan = uvegan;

        }
    }
}
