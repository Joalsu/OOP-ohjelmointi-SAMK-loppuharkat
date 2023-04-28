using System;
using System.Collections.Generic;
using System.Text;

namespace Tentti02
{
    class Noppa
    {
        //staattinen muuttuja, joka saa arvoksi Random-olion
        private static Random NOPPARANDOM = new Random();

        // -- Automaattiset ominaisuudet --

        public int Lukema { get; set; }
        public int HeittoLkm { get; set; }

        // -- Konstruktorit --

        public Noppa()
        {
            //Alustaa heittojen lukumäärän ominaisuuden HeittoLkm nollaksi.
            HeittoLkm = 0;
        }

        // -- Metodit --

        public int Heita()
        {
            //Arpoo kokonaisluvun väliltä 1‐6. Random‐olion avulla ja sijoitetaan se ominaisuuteen lukema
            Lukema = NOPPARANDOM.Next(1, 7);

            //Kasvattaenheittojen lukumäärää yhdellä ja palauttaa ominaisuuden Lukema arvon
            HeittoLkm++;
            return Lukema;
        }
    }
}
