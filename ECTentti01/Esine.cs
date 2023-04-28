using System;
using System.Collections.Generic;
using System.Text;

namespace ECTentti01
{
    class Esine : INimi, IComparable<Esine>
    {
        //Kenntämuuttuja johonka taletetaan saatu paino
        private double _paino;

        //Ominaisuudet. PainoG ominaisuuteen asetetaan saatu pyöristetty paino
        public string Nimi { get; set; }
        public double PainoG { get => _paino; }

        //Asetetaan Esineen paino ja jos annettu paino on negatiivinen lähetetään siitä virhe
        public double Paino
        {
            get => _paino;

            set
            {
                if (value < 0)
                {
                    throw new Exception("Et voi tallattaa negatiivista painoa!");
                }
                _paino = Math.Round(value, 3);
            }
        }

        //Verrataan esine olioiden painoa
        public int CompareTo(Esine esine)
        {
            return ToString().CompareTo(esine.ToString());
        }

        //Palautetaan arvot merkkijonona
        public override string ToString()
        {
            return $"{Nimi} {PainoG} kg";
        }
    }
}
