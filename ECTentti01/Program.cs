using System;
using System.Collections.Generic;
using static System.Console;

namespace ECTentti01
{
    class Program
    {
        static void Main()
        {
            try
            {
                Algoritmi();
            }
            catch (Exception e)
            {
                WriteLine($"Virhe: {e.Message}");
            }
        }

        static void Algoritmi()
        {
            //Tehdään uusi laatikko lista
            List<Laatikko> LaatikkoLista = new List<Laatikko>();

            Laatikko laatikko = new Laatikko();
            int kaikki = 0;

            WriteLine("Tehdään laatikot");

            //Kysytään niin kauvan kunnes annetaan tyhjä
            while (true)
            {
                Write("Anna laatikon nimi (tyhjä lopettaa): ");
                string nimi = ReadLine();

                if (nimi.Length == 0)
                {
                    break;
                }

                //Tehdään uusi laatikko olio
                laatikko = new Laatikko()
                {
                    Nimi = nimi
                };

                //Lisätään laatikko oio listaan
                LaatikkoLista.Add(laatikko);

                //Mennään ominaisuuteen kysymään tiedot esineistä
                kaikki = Esinelisays();
            }

            //Tulostetaan laatikon tiedot
            WriteLine();
            WriteLine($"Laatikko {laatikko.Nimi}");
            WriteLine($"Esineitä {kaikki} kpl.");
            WriteLine("Painavin esine");
        }

        static int Esinelisays()
        {
            WriteLine("Lisätään laatikkooon esineitä");

            string nimi; double paino;
            int maara; int maaraYhteensä = 0;

            //Kysytään niin kauvan kunnes annetaan tyhjä
            //ja kysytään tuotteen nimi, paino ja määrä ja tehdään niiden arvoilla uusi esine olio 

            while (true)
            {
                Write("Anna esineen nimi (tyhjä lopettaa): ");
                nimi = ReadLine();

                if (nimi.Length == 0)
                {
                    break;
                }

                Write("Anna esineen paino (kg): ");
                paino = double.Parse(ReadLine());

                Write("Lisättävä määrä: ");
                maara = int.Parse(ReadLine());

                paino = paino * maara;

                Esine esine = new Esine()
                {
                    Paino = paino,
                    Nimi = nimi
                };

                WriteLine($"Esinettä {esine.Nimi} lisättiin {maara} kpl.");

                maaraYhteensä += maara;
            }

            //Palautetaan esine kpl määrä
            return maaraYhteensä;
        }
    }
}
