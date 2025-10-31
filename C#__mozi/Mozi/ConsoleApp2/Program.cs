using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Program
    {
        public static List<Mozi> mozik = new List<Mozi>();

        //egy nap hány filmet lehet leadni  ha 9-22 ig adja a filmeket minimum és maximum
        public static List<int> Napi_Film()
        {
            List<int> eredmenyek = new List<int>();
            List<int> film_ido = new List<int>(); //percben
            foreach (Mozi mozi in mozik)
            {               // az 1,53 az 1-óra 53-perc ként van 
                int ora = (int)mozi.Vetitesi_ido;
                int perc = (int)(mozi.Vetitesi_ido * 100) - 40 * ora;
                film_ido.Add(perc);
            }

            film_ido.Sort();
            double ido = 780; // 780 perc 13*60
            int max_film = 0;
            int min_film = 0;

            foreach (var i in film_ido)
            {
                int szunet_db = (int)i / 40;
                int szunet = 5 * szunet_db;
                int takaritas = 15;
                if (ido - i - szunet - takaritas >= 0) { ido -= (i + szunet + takaritas); max_film++; }
                else break;
            }

            ido = 780;
            for (int i = film_ido.Count - 1; 0 <= i; i--)
            {
                int szunet_db = (int)film_ido[i] / 40;
                int szunet = 5 * szunet_db;
                int takaritas = 15;

                if (ido - film_ido[i] - szunet - takaritas >= 0) { ido -= (film_ido[i] + szunet + takaritas); min_film++; }
                else break;
            }

            eredmenyek.Add(min_film);
            eredmenyek.Add(max_film);



            return eredmenyek;
        }

        public static List<string> Legeredemenyesebb_filmek()
        {
            List<string> filmek = new List<string>();

            var ertekeles_atlag = mozik.Average(n => n.Ertekeles);
            var nezok_atlag = mozik.Average(n => n.Nezok_szama);


            foreach (var n in mozik)
            {
                if (n.Ertekeles > ertekeles_atlag && n.Nezok_szama > nezok_atlag) filmek.Add(n.Film_cim);
            }
            return filmek;
        }



        public static bool Ertekeles()
        {
            var nezetseg_min = mozik.Min(n => n.Nezok_szama);
            var nezetseg_max = mozik.Max(n => n.Nezok_szama);

            var ertekeles_min = mozik.Min(n => n.Ertekeles);
            var ertekeles_max = mozik.Max(n => n.Ertekeles);

            var nez_ind_min = mozik.FindIndex(n => n.Nezok_szama == nezetseg_min);
            var nez_ind_max = mozik.FindIndex(n => n.Nezok_szama == nezetseg_max);


            if (mozik[nez_ind_min].Ertekeles == ertekeles_min && mozik[nez_ind_max].Ertekeles == ertekeles_max) return true;
            else return false;
        }
        public static List<int> kis_adag = new List<int>() {300,25 };
        public static List<int> kozepes_adag = new List<int>() { 600, 40 };
        public static List<int> nagy_adag = new List<int>() { 800, 60 };

        public static Dictionary<string, List<int>> popcorn_cola=new Dictionary<string, List<int>>()
        {
            {"nagy adag",nagy_adag},    // 60 p
            {"közepes adag",kozepes_adag}, // 40 p
            {"kis adag",kis_adag},     // 25 p
            
            

        };
       

        public static string Vasarlas_opcio(int keret, Mozi film)
        {
            string szoveg = "";
            // csak szünetben lehet venni és csak akkor ha elfogyott az előző adat
            List<int> idok=new List<int>() {};
            int ora = (int)film.Vetitesi_ido;
            int perc = (int)(film.Vetitesi_ido * 100) - 40 * ora;

            while (true)
            {
                if (perc - 40 > 0)
                {
                    perc -= 40;
                    idok.Add(40);
                }
                else
                {
                    idok.Add(perc);
                    break;
                }
            }
            for (int i = 0; i < idok.Count; i++)
            {                
            
                foreach (var k in popcorn_cola)
                {
                    if (idok[i] == 0) break;
                    else if (idok.Count>i+1 && keret - k.Value[0] > 0)
                    {
                        if (idok[i] + idok[i + 1] - k.Value[1]< 40)
                        {
                            idok[i] = 0;
                            idok[i+1] = 0;
                            keret-=k.Value[0];
                            szoveg += k.Key+";";

                        }
                    }
                    else if (idok[i] - k.Value[1] >= -15 && keret >= k.Value[0])
                    {
                        idok[i] = 0;
                        keret -= k.Value[0];
                        szoveg += k.Key+";";
                    }
                    
                }
            }


            szoveg+="megmaradt pénz:"+keret.ToString();




            return szoveg;
        }

        public static Dictionary<string,double> rangsor = new Dictionary<string,double>();
        public static string Rangsor()
        {
            string legjobb_film = "";

            foreach( var i in mozik)
            {
                int ora = (int)i.Vetitesi_ido;
                int perc = (int)(i.Vetitesi_ido * 100) - 40 * ora;

                var pont =Math.Round((i.Nezok_szama * i.Ertekeles) / perc,2);

                rangsor.Add(i.Film_cim, pont);
            }

            
            foreach( var i in rangsor)
            {
                if (i.Value == rangsor.Values.Max()) { legjobb_film = i.Key;break; }
            }



            return legjobb_film;

        }



        static void Main(string[] args)
        {

            //max férőhely 500
            // minden film 40-perc után tart egy 5-perces szünetet
            // minen film után van egy 15-perc takaritási idő 
            /// Vannak popcornok kis-300FT és 25-perc alatt fogy el
            ///                  kozepes- 600 FT 40-percig tart 
            ///                  nagy 800 ft 1-óráig tart 
            ///                  


            Mozi film1 = new Mozi("Nagyfiúk", 1.42, 6.0, 122);
            Mozi film2 =new Mozi("A Holnap határa", 1.53, 7.9, 437);
            Mozi film3=new Mozi("Venom", 1.50, 6.0, 222);
            Mozi film4=new Mozi("Socic 3", 1.50, 6.9, 259);
            Mozi film5=new Mozi("Avatar: A víz útja", 3.12, 7.6, 391);
            Mozi film6=new Mozi("A Karib-tenger kalózai - A Fekete Gyöngy átka", 2.23, 8.1, 466);
            Mozi film7=new Mozi("Láthatatlan Sue", 1.30, 4.6, 80);
            Mozi film8=new Mozi("Boszorkányvadászat", 1.53, 5.4, 90);
            Mozi film9=new Mozi("Tarzan", 1.28, 7.3, 317);
            Mozi film10=new Mozi("Pókember: Nincs hazaút", 2.28, 8.2, 500);

            mozik.Add(film1);
            mozik.Add(film2);
            mozik.Add(film3);
            mozik.Add(film4);
            mozik.Add(film5);
            mozik.Add(film6);
            mozik.Add(film7);
            mozik.Add(film8);
            mozik.Add(film9);
            mozik.Add(film10);



            Console.WriteLine("Ennyi filmet lehet leadni maximum: {0}", Napi_Film()[1]);
            Console.WriteLine("Ennyi filmet lehet leadni minimum: {0}", Napi_Film()[0]);

            Console.WriteLine("\nEzeket a filmeket érdemes megnézni:\n");
            foreach (var i in Legeredemenyesebb_filmek())
            {
                Console.WriteLine("{0}",i);
            }

            Console.WriteLine("\nAz állitás: ");
            if(Ertekeles()) Console.WriteLine("Igaz hogy a legjobb értékelést kapott filmre mennek a legtöbben");
            else Console.WriteLine("Nem igaz hogy a legjobb értékelést kapott filmre mennek a legtöbben");

            Console.WriteLine("\nAz ajánlat:{0}", Vasarlas_opcio(2500, film1));

            Console.WriteLine("\nA számitá alapján a legjobb film:{0}", Rangsor());


            Console.ReadKey();
        }
    }


    public class Mozi
    {
        private string film_cim;
        private double vetitesi_ido;
        private double ertekeles;
        private int nezok_szama;

        public string Film_cim
        {
            get=> film_cim;
            set=> film_cim = (value.Length>1)?value:throw new Exception("A név legalább 2-karakter legyen");
        }

        public double Vetitesi_ido
        {
            get => vetitesi_ido;
            set => vetitesi_ido = (value > 0) ? value : throw new Exception("A vetitési idő legalább egy 10 perc legyen");
        }

        public double Ertekeles
        {
            get => ertekeles;
            set => ertekeles = (value>0 && value<=10) ? value : throw new Exception("Az értékelés 1-10 közötti lehet");
        }
        public int Nezok_szama
        {
            get => nezok_szama;
            set => nezok_szama = (value >0 && value <= 500) ? value : throw new Exception("Az nézők száma 1-500 közötti lehet");
        }


        public Mozi(string film_cim, double vetitesi_ido, double ertekeles, int anezok_szama)
        {
            Film_cim = film_cim;
            Vetitesi_ido = vetitesi_ido;
            Ertekeles = ertekeles;
            Nezok_szama = anezok_szama;
        }





    }
}
