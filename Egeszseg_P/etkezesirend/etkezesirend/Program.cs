using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace etkezesirend
{
    public  class Program
    {
        public static List<Etelek> list_Etelek = new List<Etelek>();
        public static List<Italok> list_Italok = new List<Italok>();
        public static List<Desszertek> list_Desszertek = new List<Desszertek>();
        public static List<Szemelyek> list_Szemelyek = new List<Szemelyek>();
        public static List<Etkezesek> list_Etkezesek = new List<Etkezesek>();

        public static string connection = "server=localhost;user=root;database=Etrend;password=;";
        static void Main(string[] args)
        {
            Etelek_beolv();
            Italok_beolv();
            Desszertek_beolv();
            Szemelyek_beolv();
            Etkezesek_beolv();
            

            ///  melyik étel/ital az amit a legtöbbször rendeltek 
            ///  melyik az emberek egészségi rend szerint csökkenő sorrendben (BMI - mozgás- kaja )
            ///  minden ember étkezésenként mennyi kaloriát vitt be / és menneyire tartotta be a napi kaloria limitet
            ///  csoportonként / embewrek milyen ételt esznek 
            ///  mennyire térnek el az emberek az átleg BMI től és merre kelle mozogni a helyes legyen 
          

            Console.WriteLine("=== 1. Legtöbbször választott étel és ital és desszert===");
            Leggyakoribb();

            Console.WriteLine("\n=== 2. Napi kalória bevitel és limit betartása ===");
            NapiKaloria();

            Console.WriteLine("\n=== 3. Csoportonként fogyasztott ételek ===");
            CsoportEtelek();

            Console.WriteLine("\n=== 4. Eltérés az átlag BMI-től ===");
            BmiElteres();

            Console.WriteLine("\n=== 5. Személyek egészségi sorrend szerint (csökkenő) ===");
            EgeszsegRendszer();
        }

        public static void Leggyakoribb()
        {
            var leg_etel = list_Etkezesek.Where(n => n.etel_id != 1).GroupBy(n => n.etel_id).OrderByDescending(n=> n.Count()).First().Key;
            var nev_etel = list_Etelek.FirstOrDefault(n => n.Id == leg_etel).nev;

            var leg_ital = list_Etkezesek.Where(n=> n.ital_id != 1).GroupBy(n => n.ital_id).OrderByDescending(n => n.Count()).First().Key;
            var nev_ital = list_Italok.FirstOrDefault(n => n.Id == leg_ital).nev;

            var leg_dessz = list_Etkezesek.Where(n=> n.desszert_id != 1).GroupBy(n => n.desszert_id).OrderByDescending(n => n.Count()).First().Key;
            var nev_dessz = list_Desszertek.FirstOrDefault(n => n.Id == leg_dessz).nev;
            Console.WriteLine("");
            Console.WriteLine($"A legtöbbször {nev_etel} választották az ételek közül");
            Console.WriteLine($"A legtöbbször {nev_ital} választották az italok közül");
            Console.WriteLine($"A legtöbbször {nev_dessz} választották az deszzertek közül");
        }

       
 
       public static void CsoportEtelek()
        {
            var csoportok = list_Szemelyek
                .GroupBy(sz =>   sz.mozgas );

            foreach (var csoport in csoportok)
            {
                Console.WriteLine($"\nCsoport: {csoport.Key }");

                var etelGyakorisag = csoport
                    .SelectMany(sz => list_Etkezesek.Where(e => e.szemely_id == sz.Id && e.etel_id != 1))
                    .GroupBy(e => e.etel_id)
                    .Select(g => new
                    {
                        Nev = list_Etelek.FirstOrDefault(et => et.Id == g.Key).nev,
                        Db = g.Count()
                    });
                    

                Console.WriteLine($"  {"Étel",-30} {"Rendelések db",12}");
                Console.WriteLine("  " + new string('-', 50));
                foreach (var e in etelGyakorisag)
                    Console.WriteLine($"  {e.Nev,-30} {e.Db,12}");
            }
        }
        
        public static void BmiElteres()
        {
            double normal_bmi_min = 18.5;
            double normal_bmi_max = 24.9;


            Console.WriteLine($"\nAz átlagos BMI tartomány:{normal_bmi_min} - {normal_bmi_max} között van");
            Console.WriteLine($"{"Név",-18}  {"mozog",-10}  {"BMI",-10}  {"Tanacs"}");
            Console.WriteLine(new string('-', 50));

            foreach (var i in list_Szemelyek)
            {
                double bmi =Math.Round( i.suly / ((i.magassag / 100) * (i.magassag/100)),2);
                string ertek = "";
                if (bmi < normal_bmi_min) ertek = "BMI alacsony, javasolt töblet bevitel.";
                else if (bmi >= normal_bmi_min && bmi <= normal_bmi_max) ertek = "BMI normális";
                else if (bmi <= 27.9) ertek = "BMI kicsit túlmegy a normálon";
                else ertek = "BMI tulmutat a normálon javasolt alacsonyabb kalória bevitel/ fogyás";

                Console.WriteLine($"{i.nev,-18}  {i.mozgas,-10}  {bmi,-10}  {ertek}");

            }
        }

        public static void NapiKaloria()
        {
            foreach (var i in list_Szemelyek)
            {
                
                var limit=Normal_kaloria(i) * MozgasPont(i.mozgas); ;
                var szemely_etkezes= list_Etkezesek.Where(n => n.szemely_id == i.Id).GroupBy(n=> n.nap);

                Console.WriteLine($"{i.nev,-15}| Napi limit:{limit}\n");
                foreach (var j in szemely_etkezes)
               {
                    Console.WriteLine($"{"Dátum:",10} - {j.Key:yyyy-MM-dd}\n");

                    var etkezes_kal =j.Select(n =>
                    {
                        var mennyiseg = (list_Etelek.FirstOrDefault(p => p.Id == n.etel_id).kaloria) +
                        (list_Italok.FirstOrDefault(p => p.Id == n.ital_id).kaloria) +
                        (list_Desszertek.FirstOrDefault(p => p.Id == n.desszert_id).kaloria);

                        return new
                        {
                            napszak = n.napszak,
                            mennyiseg = mennyiseg,
                        };

                    }).ToList();

                    etkezes_kal.ForEach(k => Console.WriteLine($"  {k.napszak,-15} {k.mennyiseg}/kal"));
                    var ossz_kal = etkezes_kal.Sum(n=> n.mennyiseg);
                    var elteres = limit - ossz_kal;
                    var ertekeles = (elteres<-150)? "A normál szint fölé mentél" : (elteres <= 150&& elteres>=-150)?"A szint normális": "A normál szint alá mentél";

                    
                    Console.WriteLine($"\nAmenyit bevitt:{ossz_kal,10}");
                    Console.WriteLine($"Az eltérés: {elteres,13}");
                    Console.WriteLine($"Az értékelés:{ertekeles}\n");
                    Console.WriteLine(new string('-', 50));


                }
            }
        }

        public static void EgeszsegRendszer()
        {
            var rangsor = list_Szemelyek.Select(n =>
            {
                double bmi = Math.Round(n.suly / ((n.magassag / 100) * (n.magassag / 100)), 2);
                double limit = Normal_kaloria(n) * MozgasPont(n.mozgas);
                double atlagBevitel = KaloriaSZ(n);

                double bmiPont = Math.Max(0, 100 - (Math.Abs(bmi - 21.7) / 18.3 * 100));
                double kalPont = Math.Max(0, 100 - (Math.Abs(atlagBevitel - limit) / limit * 200));

                double egeszsegiSzam = Math.Round((bmiPont + kalPont) / 2, 1);

                string ertekeles;
                if (egeszsegiSzam >= 80) ertekeles = "Kiváló";
                else if (egeszsegiSzam >= 60) ertekeles = "Jó";
                else if (egeszsegiSzam >= 40) ertekeles = "Átlagos";
                else ertekeles = "Problémás";

                return new
                {
                    Nev = n.nev,
                    BMI = bmi,
                    Mozgas = n.mozgas,
                    EgeszsegiSzam = egeszsegiSzam,
                    Ertekeles = ertekeles
                };
            })
            .OrderByDescending(n => n.EgeszsegiSzam);

            Console.WriteLine($"\n{"Név",-20} {"BMI",-7} {"Mozgás",-12} {"Pont",6}  {"Értékelés"}");
            Console.WriteLine(new string('-', 60));
            foreach (var r in rangsor)
                Console.WriteLine($"{r.Nev,-20} {r.BMI,-7} {r.Mozgas,-12} {r.EgeszsegiSzam,6}  {r.Ertekeles}");
        }
        
        public static double Normal_kaloria(Szemelyek sz)
        {

            var nemi_ertek = sz.nem == "férfi" ? 5 : -161;
            var limit = ((10 * sz.suly) + (6.25 * sz.magassag) - (5 * sz.kor) + nemi_ertek);


            return limit;
        }

        public static double KaloriaSZ(Szemelyek sz)  
        {

            var napi_kaloria_atlag = list_Etkezesek.Where(n => n.szemely_id == sz.Id).GroupBy(n => n.nap)
                .Select(n => n.Sum(k =>

                (list_Etelek.FirstOrDefault(p => p.Id == k.etel_id).kaloria) +
                (list_Italok.FirstOrDefault(p => p.Id == k.ital_id).kaloria) +
                (list_Desszertek.FirstOrDefault(p => p.Id == k.desszert_id).kaloria)
                )).Average();


            return napi_kaloria_atlag;
        }
        public static double MozgasPont(string moz)
        {
            if (moz == "sportol") return 1.55;
            else if (moz == "normál") return 1.375;
            else return 1.14;

        }

        public static void Etelek_beolv()
        {
           

            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string sql = "SELECT * FROM Etelek";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list_Etelek.Add(new Etelek(
                        reader.GetInt32("id"),
                        reader.GetString("nev"),
                        reader.GetDouble("kaloria"),
                        reader.GetDouble("ar"),
                        reader.GetBoolean("vegan")
                    ));
                }
            }

           
        }
        public static void Italok_beolv()
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string sql = "SELECT * FROM Italok";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list_Italok.Add(new Italok(
                        reader.GetInt32("id"),
                        reader.GetString("nev"),
                        reader.GetDouble("kaloria"),
                        reader.GetDouble("ar"),
                        reader.GetBoolean("vegan")
                    ));
                }
            }
        }
        public static void Desszertek_beolv()
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string sql = "SELECT * FROM Desszertek";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list_Desszertek.Add(new Desszertek(
                        reader.GetInt32("id"),
                        reader.GetString("nev"),
                        reader.GetDouble("kaloria"),
                        reader.GetDouble("ar"),
                        reader.GetBoolean("vegan")
                    ));
                }
            }
        }
        public static void Szemelyek_beolv()
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string sql = "SELECT * FROM Szemelyek";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list_Szemelyek.Add(new Szemelyek(
                        reader.GetInt32("id"),
                        reader.GetString("nev"),
                        reader.GetInt32("kor"),
                        reader.GetString("nem"),
                        reader.GetBoolean("vegan"),
                        reader.GetDouble("suly"),
                        reader.GetDouble("magassag"),
                        reader.GetString("mozgas")
                    ));
                }
            }
        }
        public static void Etkezesek_beolv()
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string sql = "SELECT * FROM  Etkezesek";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list_Etkezesek.Add(new Etkezesek(
                        reader.GetInt32("id"),
                        reader.GetInt32("szemely_id"),
                        reader.GetInt32("etel_id"),
                        reader.GetInt32("ital_id"),
                        reader.GetInt32("desszert_id"),
                        reader.GetDateTime("nap"),
                        reader.GetString("napszak")
                    ));
                }
            }
        }

    }
}
