using System;
using static System.Console;
using System.Collections.Generic;
using System.Text;

namespace Tentti02
{
    static class Sovellus
    {
        public static void Aja()
        {
            string pelaaja;
            int voittoRaja = 3;

            //Tulostetaan alku teksti keltaisella
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("Noppa-peli");
            ResetColor();

            //Luodaan 2kpl pelaaja olioita kysymällä ensiksi käyttäjältä nimeä.
            //Pelaaja oliot luo pelaajalle uuden noppa olion ja asetaa saadun nimen ja asetaa pistemäärät luokassa Pelaaja

            pelaaja = Merkkijono("Pelaajan 1 nimi:");
            Pelaaja p1 = new Pelaaja(pelaaja);

            pelaaja = Merkkijono("Pelaajan 2 nimi:");
            Pelaaja p2 = new Pelaaja(pelaaja);

            //Tehdään peli olio johonka sijoitetaan yllä olevat pelaaja olioiden arvot ja voittoraja arvo
            Peli peli = new Peli(p1, p2, voittoRaja);

            //Suoritetaan niinkauvan kunnes voittaja selviää, eli kun voittaja saa arvon muun kun null lopetetaan.
            while (peli.Voittaja == null)
            {
                //Kasvatetaan pelattavaa kierrosta aina yhdellä kierroksen alkaessa ja siirrytään Luokan peli metodiin PelaaKierros.
                peli.KierrosLkm++;
                peli.PelaaKierros();
            }

            //kun yllä oleva suoritus loppuu tulostetaan voittaja ja pelatut kierrokset keltaisella
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine($"Pelin voittaja on {peli.Voittaja.Nimi} ja noppia heitettiin {peli.KierrosLkm} kertaa!");
            ResetColor();
        }

        //Kysytään käyttäjältä nimi ja palautetaan sen arvo
        public static string Merkkijono(string kehote)
        {
            Write($"{kehote} ");
            return ReadLine();
        }
    }
}
