using System;
using static System.Console;
using System.Collections.Generic;
using System.Text;

namespace Tentti03
{
    static class Sovellus
    {
        //staattinen muuttuja, joka saa arvoksi Random-olion
        static Random randomi = new Random();

        static void TeeYksiloKilpailu(double minimi, double maksimi)
        {
            Write("Anna yksikkökilpailun nimi: ");
            string vastaus = ReadLine();

            //Tehdään uusi YksiloKilpailu olio saadusta vastauksesta ja sijoitetaan ominaisuuteen Nimi
            YksiloKilpailu kilpailu = new YksiloKilpailu()
            {
                Nimi = vastaus
            };

            //Kysytään nimiä niin kauan kunnes annetaan tyhjä
            while (true)
            {
                Write("Anna osallistujan nimi muodossa \"sukunimi etunimi\" (tyhjä lopettaa): ");
                string nimi = ReadLine();

                if (nimi.Length == 0)
                {
                    break;
                }

                //Tehdään uusi suoritus olio jonka Osallistuja ominaisuuteen tehdään
                //uusi henkilö olio ja laitetaan saatu nimi siihen
                Suoritus<Henkilo, double> suoritus = new Suoritus<Henkilo, double>()
                {
                    Osallistuja = new Henkilo()
                    {
                        Nimi = nimi
                    }
                };

                //Arvotaan suoritus olion tulos ominaisuuteen randomi luku saaduilla raja-arvoilla pyöristettynä
                suoritus.Tulos = Math.Round(randomi.NextDouble() * (maksimi - minimi) + minimi, 2);

                //Lisätään uuden suoritus olion tiedot Yksilokilpailu olioon
                kilpailu.Suoritukset.Add(suoritus);
            }

            //Tulostetaan keltaisella saatu kilpailun nimi
            WriteLine("");
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine($"Kilpailun {kilpailu.Nimi} tulokset:");

            foreach (var suoritus in kilpailu.Suoritukset)
            {

                WriteLine($"{suoritus.Osallistuja.Nimi}, {suoritus.Tulos}");
            }

            ResetColor();

            //Mennään tulostamaan kaikki saadut arvot YksiloKilpailu olion Suoritukset ominaisuuden listasta

            //TulostaTulokset(kilpailu.Suoritukset);
        }

        static void TeeJoukkueKilpailu(int maksimi)
        {
            Write("Anna joukkuekilpailun nimi: ");
            string vastaus = ReadLine();

            //Tehdään uusi JoukkueKilpailu olio saadusta vastauksesta ja sijoitetaan ominaisuuteen Nimi
            JoukkueKilpailu kilpailu = new JoukkueKilpailu 
            { 
                Nimi = vastaus
            };

            while (true)
            {
                Write("Anna osallistujan nimi (tyhjä lopettaa):");
                string nimi = ReadLine();

                if (nimi.Length == 0)
                {
                    break;
                }

                //Tehdään uusi suoritus olio jonka Osallistuja ominaisuuteen tehdään
                //uusi joukkue olio ja laitetaan saatu nimi siihen
                Suoritus<Joukkue, int> suoritus = new Suoritus<Joukkue, int>
                {
                    Osallistuja = new Joukkue
                    {
                        Nimi = nimi
                    }
                };

                //Arvotaan suoritus olion tulos ominaisuuteen randomi luku väliltä 0 - annettu maksimi
                suoritus.Tulos = randomi.Next(0, maksimi);

                //Lisätään uuden suoritus olion tiedot Joukkuekilpailuolioon olioon
                kilpailu.Suoritukset.Add(suoritus);
            }

            WriteLine("");
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine($"Kilpailun {kilpailu.Nimi} tulokset:");

            foreach (var suoritus in kilpailu.Suoritukset)
            {
                WriteLine($"{suoritus.Osallistuja.Nimi}, {suoritus.Tulos} pistettä");
            }

            ResetColor();

            //TulostaTulokset(kilpailu.Suoritukset);
        }

        //Tulostetaan keltaisella saadut suoritukset lista läpi
        //static void TulostaTulokset<T>(List<T>tulokset)
        //{
        //    ForegroundColor = ConsoleColor.Yellow;

        //    foreach (T alkio in tulokset)
        //    {
        //        WriteLine(alkio.ToString());
        //    }

        //    ResetColor();
        //    WriteLine("");
        //}

        public static void Aja()
        {
            double minimi, maximi;
            int maximiJ;

            while (true)
            {
                Write("Tehdäänkö yksilö- (y) vai joukkuekilpailu (j), tyhjä lopettaa: ");
                string vastaus = ReadLine();

                if (vastaus.Length == 0)
                {
                    break;
                }

                //jos totta kysytään maximi ja minimi tulokset ja tarkistetaan syötteiden kelpoisuus
                //metodissa DesimaalilukuPakottaen ja sen jälkeen siirrytään TeeYksiloKilpailu metodiin annetuilla arvoilla
                if (vastaus == "y")
                {
                    minimi = DesimaalilukuPakottaen("Anna yksilökilpailun minimitulos: ");
                    maximi = DesimaalilukuPakottaen("Anna yksilökilpailun maksimitulos: ");
                    TeeYksiloKilpailu(minimi, maximi);
                }

                //Jos totta tehdään sama kuin yllä, mutta kysytään vain yksi kokonaisluku
                //ja tarkistetaan sen kelpoisuus metodissa KokonaislukuPakottaen.
                if (vastaus == "j")
                {
                    maximiJ = KokonaislukuPakottaen("Anna joukkuekilpailun maksimipistemäärä: ");
                    TeeJoukkueKilpailu(maximiJ);
                }
            }
        }

        static double DesimaalilukuPakottaen(string kehote, int tarkkuus = -1)
        {
            do
            {
                try
                {
                    return Desimaaliluku(kehote, tarkkuus);
                }
                catch (Exception e)
                {
                    WriteLine(e.Message);
                }
            } while (true);
        }

        //Tarkistetaan onko annettu syöte kelvollinen desimaaliluku
        //kokeilamalla muuntaa se desimaaliluvuksi, jos ei onnistu lähetetään virhe

        static double Desimaaliluku(string kehote, int tarkkuus = -1)
        {
            double paluu;
            Write($"{kehote} ");
            if (!double.TryParse(ReadLine(), out paluu))
            {
                throw new ApplicationException("Syöte ei ole kelvollinen desimaaliluku.");
            }
            return tarkkuus >= 0 ? Math.Round(paluu, tarkkuus) : paluu;
        }

        static int KokonaislukuPakottaen(string kehote, int min = int.MinValue, int max = int.MaxValue)
        {
            do
            {
                try
                {
                    return Kokonaisluku(kehote, min, max);
                }
                catch (Exception e)
                {
                    WriteLine(e.Message);
                }
            } while (true);
        }

        //Tarkistetaan onko annettu syöte kelvollinen kokonaisluku
        //kokeilamalla muuntaa se kokonaisluvuksi, jos ei onnistu lähetetään virhe

        static int Kokonaisluku(string kehote, int min = int.MinValue, int max = int.MaxValue)
        {
            int paluu;
            Write($"{kehote} ");
            if (!int.TryParse(ReadLine(), out paluu) || (paluu < min || paluu > max))
            {
                throw new ApplicationException("Syöte ei ole kelvollinen kokonaisluku." +
                    (min > int.MinValue ? ($" Minimi on {min}.") : "") +
                    (max < int.MaxValue ? ($" Maksimi on {max}.") : ""));
            }
            return paluu;
        }
    }
}
