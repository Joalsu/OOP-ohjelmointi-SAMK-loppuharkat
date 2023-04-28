using System;
using static System.Console;
using System.Collections.Generic;
using System.Text;

namespace Tentti02
{
    class Peli
    {
        // -- Automaattiset ominaisuudet --

        public Pelaaja Pelaaja1 { get; set; }
        public Pelaaja Pelaaja2 { get; set; }
        public int KierrosLkm { get; set; }
        public int VoitonPisteRaja { get; }

        // -- Ominaisuudet --

        public Pelaaja Voittaja
        {
            get
            {
                //Palauttaa Pelaaja1 arvon, jos pistemäärä on vähintään voittamiseen tarvittava pistemäärä
                if (Pelaaja1.Pisteet >= VoitonPisteRaja)
                {
                    return Pelaaja1;
                }

                //Palauttaa Pelaaja2 arvon, jos pistemäärä on vähintään voittamiseen tarvittava pistemäärä
                if (Pelaaja2.Pisteet >= VoitonPisteRaja)
                {
                    return Pelaaja2;
                }

                //Palautetaan null, eli ei mitään, jos kumpikaan edellisistä vaihtoehdoista ei toteudu
                return null;
            }
        }

        // -- Konstruktorit --

        public Peli(Pelaaja p1, Pelaaja p2, int voittoRaja)
        {
            //Asetetaan parametrien arvot vastaaviin ominaisuuksiin ja nollataan KierrosLkm arvo
            Pelaaja1 = p1;
            Pelaaja2 = p2;
            VoitonPisteRaja = voittoRaja;
            KierrosLkm = 0;
        }

        // -- Metodit --

        private int NoppienHeitto(Pelaaja pelaaja)
        {
            //Kutsutaan pelaajan noppa olion heitä metodia joka on luokassa Noppa, joka sitten palauttaa tänne lukema arvon
            pelaaja.Noppa.Heita();
            //Asetetaan saatu eka lukema
            int eka = pelaaja.Noppa.Lukema;

            pelaaja.Noppa.Heita();
            int toka = pelaaja.Noppa.Lukema;

            //Laketaan saadut luvut yhteen kahdesta nopan heitosta
            int summa = eka + toka;

            WriteLine($"{pelaaja.Nimi}: {eka} + {toka} = {summa}");

            //Palautetaan saatu summa alla olevaan metodiin PelaaKierros
            return summa;
        }

        public void PelaaKierros()
        {
            WriteLine($"Heittokierros {KierrosLkm}");

            //Mennään suorittamaan yllä olevaan NoppienHeitto metodiin josta saadaan sitten pelaajille summa
            int pelaaja1 = NoppienHeitto(Pelaaja1);
            int pelaaja2 = NoppienHeitto(Pelaaja2);

            //Jos pelaajan1 summa on suurempi, kuin pelaajan 2, kasvsatetaan sen arvoa yhdellä ja nollataan toisen pisteet
            if (pelaaja1 > pelaaja2)
            {
                Pelaaja1.Pisteet++;
                Pelaaja2.Pisteet = 0;
            }

            //Jos pelaajan2 summa on suurempi, kuin pelaajan 1, kasvsatetaan sen arvoa yhdellä ja nollataan toisen pisteet
            if (pelaaja2 > pelaaja1)
            {
                Pelaaja2.Pisteet++;
                Pelaaja1.Pisteet = 0;
            }

            //Jos kumpikaan yllä oleva ehto ei todeodu ei tapahdu mitään muutosta
        }

    }
}
