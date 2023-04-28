using System;
using System.Collections.Generic;
using System.Text;

namespace Tentti02
{
    class Pelaaja : INimi
    {
        // -- Automaattiset ominaisuudet --

        public string Nimi { get; }
        public int Pisteet { get; set; }
        public Noppa Noppa { get; set; }

        // -- Konstruktorit --

        public Pelaaja(string pelaajaNimi)
        {
            //Asetetaan pelaajalle nimi parametrimuuttujasta
            Nimi = pelaajaNimi;

            //Nollataan pelaajan pistemäärä
            Pisteet = 0;

            //Luodaan pelaajalle uusi noppa-olio
            Noppa = new Noppa();
        }
    }
}
