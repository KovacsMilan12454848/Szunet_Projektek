using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etkezesirend
{
    public class Etkezesek
    {
        public int Id { get; set; }
        public int szemely_id { get; set; }
        public int etel_id { get; set; }
        public int ital_id { get; set; }
        public int desszert_id { get; set; }
        public DateTime nap { get; set; }
        public string napszak { get; set; }


        public Etkezesek(int uid, int uszemely, int uetel, int uital, int udesszert,DateTime unap, string unapszak)
        {
            Id = uid;
            szemely_id = uszemely;
            etel_id = uetel;
            ital_id = uital;
            desszert_id = udesszert;
            nap = unap;
            napszak = unapszak;
            

        }


    }
}
