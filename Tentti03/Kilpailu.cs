using System;
using System.Collections.Generic;
using System.Text;

namespace Tentti03
{
    class Kilpailu<TOsallistuja, TTulos>
    {
        public string Nimi { get; set; }
        public List<Suoritus<TOsallistuja, TTulos>> Suoritukset { get; set; }

        //Sijoitetaan listaan Suoritukset uusi listaolio
        public Kilpailu()
        {
            Suoritukset = new List<Suoritus<TOsallistuja, TTulos>>();
        }
    }
}
