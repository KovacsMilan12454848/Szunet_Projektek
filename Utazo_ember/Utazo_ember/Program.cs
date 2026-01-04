using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Utazo_ember
{
    public class Program
    {
        public static List<string> varos_ut = new List<string>() {};
        public static List<Utazasi_iroda> Utazas = new List<Utazasi_iroda>();
        public static Dictionary<string, int> Utvonalak = new Dictionary<string, int>();
        public static List<string> Varosok = new List<string>();

        public static void Utvonal(string aktualis, string veg, int ido)
        {

            
            if (aktualis==veg)
            {            
                return;
            }

            foreach (var i in Utazas)
            {
                          
                if (aktualis == i.Varos1 && i.Varos2 == veg)
                {
                    ido += i.Varos_tav;
                    varos_ut.Add(veg);
                    Utvonalak.Add(string.Join("-", varos_ut), ido);
                    varos_ut.Remove(varos_ut.Last());
                }

                if (aktualis == i.Varos1 && i.Varos2 != veg && !varos_ut.Contains(i.Varos2))
                {
                    
                    ido += i.Varos_tav;
                    varos_ut.Add(i.Varos2);
                  
                    Utvonal(i.Varos2, veg, ido);

                    varos_ut.Remove(varos_ut.Last());
                    ido -= i.Varos_tav;
                  

                }
                 

            }

        }
        

        public static void Menetrend()
        {
           
            try
            {
                StreamReader sr = new StreamReader("menetrend.txt");
                while (!sr.EndOfStream)
                {              
                   var i = sr.ReadLine().Split('-');
                   Utazas.Add(new Utazasi_iroda(i[0], i[1],int.Parse (i[2])));
                    if (!Varosok.Contains(i[0]))Varosok.Add(i[0]);

                }
                sr.Close();
            }
            catch (FileNotFoundException) { Console.WriteLine("A fálj nem található"); }
            catch (IOException) { Console.WriteLine("Hiba a fál olvasása közben"); }
        }

       

        public static void Varos_vizsgalat()
        {
           
           string indulas = "";
           string vegalommas = "";
           string varos = "";

            Console.WriteLine("Üdvözöljük az utazási irodán!");
            while (true)
            {
                Console.Write("Kérem mondja meg honnan indul: ");
                varos = Console.ReadLine().ToUpper();

                if (Varosok.Contains(varos))
                {
                    Console.WriteLine("A város megjelölve.");
                    indulas= varos;
                    break;
                }
                else
                {
                    Console.WriteLine("NIncs ilyen város kérem modjon egy másikat!");
                }
            }

            while (true)
            {
                Console.Write("Kérem mondja meg hová akar utazni: ");
                varos = Console.ReadLine().ToUpper();
                if (varos != indulas && Varosok.Contains(varos))
                {       
                    Console.WriteLine("A város megjelölve.");
                    vegalommas = varos;
                    break;
                }
                else if(varos == indulas)
                {
                    Console.WriteLine("Az induló és érkező város nem lehet ugyanaz");
                }
                else
                {
                    Console.WriteLine("Nincs ilyen város kérem modjon egy másikat!");
                }
            }
           


            varos_ut.Add(indulas);
            Utvonal(indulas,vegalommas,0);

        }

        public static void Legrovidebb_ut()
        {
           int min= Utvonalak.Values.Min();

            foreach(var i in Utvonalak)
            {
                if (i.Value == min)
                {
                    Console.WriteLine("\nA legrövidebb ut:{0}; az ut hossza {1} perc", i.Key, min);
                    break;
                }
            }

        }


        static void Main(string[] args)
        {

            Menetrend();
            Console.WriteLine("Az utatazható városok.");
            foreach (string v in Varosok){ Console.WriteLine("-{0}",v);}

            Varos_vizsgalat();

            Legrovidebb_ut();


            Console.ReadKey();

        }
    }

    public class Utazasi_iroda
    {
        private string indulo_v;
        private string erkezo_v;
        private int varos_tav;

        public string Varos1
        {
            get => indulo_v;
            set => indulo_v = (value.Length>0)?value:throw new Exception("A név tul rövid");
        }
        public string Varos2
        {
            get => erkezo_v;
            set => erkezo_v = (value.Length > 0) ? value : throw new Exception("A név tul rövid");
        }
        public int Varos_tav
        {
            get => varos_tav;
            set => varos_tav = (value> 0) ? value : throw new Exception("A ut csak 1-től nagyobb lehet.");
        }
        public Utazasi_iroda(string v1, string v2, int ut)
        {
            Varos1 = v1;
            Varos2 = v2;
            Varos_tav = ut;
        }


    }
}
