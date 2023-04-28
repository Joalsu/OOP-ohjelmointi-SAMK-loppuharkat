using System;
using static System.Console;

namespace Tentti03
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Sovellus.Aja();
            }

            catch (Exception e)
            {
                WriteLine("Ohjelman suoritus päättyi virheeseen.");
                WriteLine($"Virhe: {e.Message}");
            }
        }
    }
}
