using System;
using System.Collections.Generic;
using System.Text;

namespace ECTentti01
{
    class Laatikko : INimi
    {
        //Kenntämuuttuja johonka taletetaan maximipaino
        private int _maximipaino;

        //Ominaisuudet
        public string Nimi { get; set; }
        public double Paino { get; }
        public double Painavin { get; }

        //konstruktori jossa alustetaan maksimipaino ja esinelistaan tyhjä Esine‐olioiden lista.
        public Laatikko(int maksimipaino = 100)
        {
            List<Esine> esineet = new List<Esine>();
            _maximipaino = maksimipaino;
        }

        //Metodi jossa lisätään esineitä Laatikko‐olioon jos ylittää maximi painot lähetetään poikkeus
        public void LisaaEsine(Esine esine)
        {
            if (esine.PainoG > _maximipaino)
            {
                throw new Exception($"Laatikkoon {Nimi} ei voitu lisätä, koska maksimipaino olisi ylittynyt.");
            }
        }

        //Palautetaan arvot merkkijonona
        public override string ToString()
        {
            return $"{Nimi} {Paino} kg";
        }
    }
}
