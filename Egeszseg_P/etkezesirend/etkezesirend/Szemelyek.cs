using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etkezesirend
{
    public class Szemelyek
    {


        public int Id { get; set; }
        public string nev { get; set; }
        public int kor { get; set; }
        public string nem { get; set; }
        public bool vegan { get; set; }
        public double suly { get; set; }
        public double magassag { get; set; }
        public string mozgas { get; set; }


        public Szemelyek(int uid, string unev, int ukor, string unem, bool uvegan, double usuly, double umagas,string umozog)
        {
            Id = uid;
            nev = unev;
            kor = ukor;
            nem = unem;
            vegan = uvegan;
            suly = usuly;
            magassag = umagas;
            mozgas = umozog;
            
        }




    }
}
