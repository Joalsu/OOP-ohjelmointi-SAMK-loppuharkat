using System;
using System.Collections.Generic;
using System.Text;

namespace Tentti03
{
    class Henkilo : IOsallistuja
    {
        public int Id { get; set; }
        public string Etunimi { get; set; }
        public string Sukunimi { get; set; }

        public string Nimi
        {
            //Palauttaa ominaisuudet Sukunimi ja Etunimi arvot välilyönnillä eroteltuna
            get
            {
                return $"{Sukunimi} {Etunimi}"; 
            }

            //Sijoitetaan saatu Nimi-yhdiste merkkijonotauluun nimet
            set
            {
                string[] nimiet = value.Split();

                //Tarkistetaan onko alkioiden määrä 2, jos on
                //sijoitetaan eka ominaisuuteen Sukunimi ja toka Etunimeen.
                if (nimiet.Length == 2)
                {
                    Sukunimi = nimiet[0];
                    Etunimi = nimiet[1];
                }

                //Jos ei ole 2 alkiota merkkijonotaulukossa nimet, lähetetään virhe viesti.
                else
                {
                    throw new Exception("Henkilön nimi on oltava muodossa sukunimi etunimi.");
                }
            }
        }
    }
}
