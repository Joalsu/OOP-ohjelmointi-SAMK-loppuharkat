using System;
using static System.Console;

namespace Tentti02
{
    class Program
    {
        static void Main()
        {
            try
            {
                //Mennään suorittamaan Luokaan Sovellus staattista metodia Aja
                Sovellus.Aja();
            }
            //Jos tapahtuu virhe palataan tänne ja tulostetaan saatu virhe viesti.
            catch (Exception e)
            {
                WriteLine("Ohjelman suoritus päättyi virheeseen.");
                WriteLine($"Virhe: {e.Message}");
            }
        }
    }
}
