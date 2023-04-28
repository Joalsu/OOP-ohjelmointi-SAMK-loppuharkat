using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static System.Console;

namespace ECTentti03
{
    public static class Application
    {
        public static List<MenuItem> Menu = new List<MenuItem>();

        public static void Run()
        {
            Intialize();
            int valinta;

            //Kysytään loputtomasti vaihtoehtoa ja lopetetaan kun annetaan 0, kun annetaan
            //kelvollinen kutsutaan vastaavaa tapahtumaa MenuItem‐olion metodilla Select

            while (true)
            {
                valinta = ReadFromMenu();

                if (valinta == 0)
                {
                    break;
                }

                valinta--;

                Menu[valinta].Select();
            }

        }

        public static void Intialize()
        {
            Data.GenerateData();
            IntializeMenu();
        }

        public static void IntializeMenu()
        {
            //Populoidaan Staattiseen muuttujaan uusia MenuItem olioilla
            Menu.Add(new MenuItem() { Id = 1, Name = "50-vuotiaat työntekijät" });
            Menu.Add(new MenuItem() { Id = 2, Name = "Osastot yli 50 henkilöä" });
            Menu.Add(new MenuItem() { Id = 3, Name = "Sukunimen työntekijät" });
            Menu.Add(new MenuItem() { Id = 4, Name = "Osastojen isoimmat palkat" });
            Menu.Add(new MenuItem() { Id = 5, Name = "Viisi yleisintä sukunimeä" });
            Menu.Add(new MenuItem() { Id = 6, Name = "Osastojen ikäjakaumat" });

            //Tehdään jokaiselle LINQ kyselyt halutulle datalle jonka yhdeydessä luodaan
            //ItemSelected käsittelijä metodi tapahtumalle ja tulostetaan data
            //metodissa kyselystä WriteResult saaduilla data arvoilla.

            //50-vuotiaat työntekijät
            Menu[0].ItemSelected += (obj, a) =>
            {
                var result = Data.Employees.Where(emp => emp.Age == 50)
                                           .Select(emp => new 
                                           { 
                                               Nimi = emp.Name,
                                               Ikä = emp.Age,
                                               Osasto = emp.Department.Name
                                           });

                WriteResult(a.ItemId, result.ToList());
            };

            //Osastot yli 50 henkilöä
            Menu[1].ItemSelected += (obj, a) =>
            {
                var result = Data.Departments.Where(dep => dep.EmployeeCount > 50)
                                             .Select(dep => new 
                                             { 
                                                 Id = dep.Id,
                                                 Nimi = dep.Name,
                                                 Vahvuus = dep.EmployeeCount 
                                             })
                                             .OrderByDescending(dep => dep.Vahvuus);

                WriteResult(a.ItemId, result.ToList());
            };

            //Sukunimen mukaan työntekijät
            Menu[2].ItemSelected += (obj, a) =>
            {
                WriteLine();
                Write("Anna sukunimi: ");
                string sukunimi = ReadLine();

                var result = Data.Employees.Where(emp => emp.LastName == sukunimi)
                                           .Select(emp => new { Id = emp.Id, Name = emp.Name });

                WriteResult(a.ItemId, result.ToList());
            };

            //Osastojen isoimmat palkat
            //(Ei ihan oikein, en oikeen osannut vaikka mitä pyörittelin)
            Menu[3].ItemSelected += (obj, a) =>
            {
                var result = Data.Departments.GroupBy(max => new { max.Name, max.Employees })
                                             .SelectMany(d => d.Key.Employees,
                                             (d, e) => new
                                             {
                                                 Osasto = d.Key.Name,
                                                 Maksimipalkka = e.Salary
                                             });

                WriteResult(a.ItemId, result.ToList());
            };

            //Viisi yleisintä sukunimeä
            Menu[4].ItemSelected += (obj, a) =>
            {
                var result = Data.Employees.Where(emp => emp.LastName.Length > 0)
                                           .GroupBy(emp => emp.LastName)
                                           .Select(joukot => new
                                           {
                                               Sukunimi = joukot.Key,
                                               Lkm = joukot.Count()
                                           })
                                           .OrderByDescending(tulos => tulos.Lkm)
                                           .Take(5);

                WriteResult(a.ItemId, result.ToList());
            };

            //Osastojen ikäjakaumat
            Menu[5].ItemSelected += (obj, a) =>
            {

                //Tehdään jokaselle halutulle datalle omat kyselyt

                var hekilotAlle = Data.Employees.Where(alle => alle.Age < 30)
                                                .GroupBy(alle => alle.Department.Name)
                                                .Select(emp => new
                                                {
                                                    Nimi = emp.Key,
                                                    Alle30v= emp.Count()
                                                });

                var henkilotValilla = Data.Employees.Where(vali => vali.Age >= 30 && vali.Age <= 50)
                                                    .GroupBy(alle => alle.Department.Name)
                                                    .Select(emp => new
                                                    {
                                                        Nimi = emp.Key,
                                                        Välillä30_50v = emp.Count()
                                                    });

                var henkilotYli = Data.Employees.Where(yli => yli.Age > 50)
                                                .GroupBy(yli => yli.Department.Name)
                                                .Select(emp => new
                                                {
                                                    Nimi = emp.Key,
                                                    Yli50v = emp.Count()
                                                });

                //Yhdistetään kaikki yllä tehdyt kyselyt yhteen

                var yhdiste = from eka in hekilotAlle
                              from toka in henkilotValilla
                              from kol in henkilotYli
                              where (eka.Nimi == toka.Nimi && eka.Nimi == kol.Nimi)
                              select new
                              {
                                  Nimi = eka.Nimi,
                                  Alle30v = eka.Alle30v,
                                  Välillä30_50v = toka.Välillä30_50v,
                                  Yli50v = kol.Yli50v
                              };

                WriteResult(a.ItemId, yhdiste.ToList());
            };
        }

        public static int ReadFromMenu()
        {
            //Tyhjennetään konsoli ja tulostetaan
            //kaikki vaihtoehdot

            Clear();
            WriteLine("Vaihtoehdot");

            foreach (var vaihtoehto in Menu)
            {
                WriteLine($"{vaihtoehto}");
            }

            string syote;
            int valittuId;

            //Kysytään lukua niin kauvan kunnes anetaan kelollinen luku tai 0
            //ja palautetaan annettu arvo, jos virheellinen syöte lähetetään virhe

            while (true)
            {
                Write("Valitse (0 = lopetus):");
                syote = ReadLine();

                if (syote == "0")
                {
                    break;
                }

                try
                {
                    valittuId = int.Parse(syote);

                    if (valittuId < 1 || valittuId > Menu.Count)
                    {
                        throw new ApplicationException();
                    }

                    else
                    {
                        return valittuId;
                    }
                }

                catch
                {
                    Write("Vastaus ei ole kelvollinen.");
                    ReadLine();
                }
            }

            return int.Parse(syote);
        }

        //Valmiiksi annettu metodi jossa tulostetaan tulostaulu
        public static void WriteResult<T>(int itemid, List<T> result)
        {
            string row;

            //otsikkorivit
            WriteLine();
            WriteLine(Menu.Where(mi => mi.Id == itemid).First().Name.ToUpper());
            WriteLine("‐".PadRight(18 * result[0].GetType().GetProperties().Length + 2, '‐'));

            //sarakeotsikkorivit
            if (result.Count > 0)
            {
                row = "";
                foreach (PropertyInfo property in result[0].GetType().GetProperties())
                {
                    row += $"{property.Name}".PadRight(16) + " | ";
                }
                WriteLine(row);
            }
            WriteLine("‐".PadRight(18 * result[0].GetType().GetProperties().Length + 2, '‐'));

            //datarivit
            foreach (object item in result)
            {
                row = "";
                foreach (PropertyInfo property in item.GetType().GetProperties())
                {
                    row += $"{property.GetValue(item, null).ToString()}".PadRight(16) + " | ";
                }
                WriteLine(row);
            }

            WriteLine("‐".PadRight(18 * result[0].GetType().GetProperties().Length + 2, '‐'));
            WriteLine();
            Write("Paina Enter jatkaaksesi.");
            ReadLine();
        }
    }
}
