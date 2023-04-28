using System;
using System.Collections.Generic;
using static System.Console;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ECTehtava4
{
    static class Application
    {
        //staattiset muuttujat

        public static int LastOperationId = 0;
        public static List<Operation> Operations = new List<Operation>();
        public static Random r = new Random();
        public static int MaxBreaks = 10;
        public static int MinTimeInSeconds = 5;
        public static int MaxTimeInSeconds = 10;


        //Metodit

        private static void PrintOperations(List<Operation> operaatio)
        {
            //Tulostetaan kaikki listan arvot PrintEnded ominaisuuksella

            foreach (var opr in operaatio)
            {
                opr.PrintEnded();
            }
        }

        private static async Task KaynnistaOperaatioAsync(Operation operaatio)
        {
            await Task.Run(() =>
            {
                //Odotetaan eli pysäytetään konsoli saadun arvon verran

                double odotus = (operaatio.TotalTimeInSeconds * 1.0 / operaatio.Breaks) * 1000;
                int odotusaika = Convert.ToInt32(odotus);

                Thread.Sleep(odotusaika);

                double kulunut = (operaatio.TotalTimeInSeconds * LastOperationId * 1.0) / operaatio.Breaks;
                int kulunutaika = Convert.ToInt32(kulunut);

                operaatio.SpendTimeInSeconds = kulunutaika;

                operaatio.Print();
            }
            );
            //Lisätään aikaleima operation päätyttyä
            operaatio.Ended = DateTime.Now;
        }

        public static void Run()
        {
            int cursori = CursorTop;
            string vastaus;

            while (true)
            {
                for (int i = 0; i < 60; i++)
                {
                    SetCursorPosition(0, i);
                    Write(" ");
                }

                SetCursorPosition(0, cursori);

                Write("Käynnistä uusi operaatio = K, Lopeta ohjelma = L:");
                vastaus = ReadLine();

                //Jos totta tulostetaan ensiksi 60 välilyöntiä ja tulostetaan tulokset
                //Enter painalluksen jälkeen lopetetaan suoritus
                if (vastaus == "L" || vastaus == "l" )
                {
                    for (int i = 0; i < 60; i++)
                    {
                        SetCursorPosition(0, i);
                        Write(" ");
                    }

                    SetCursorPosition(0, cursori);

                    Write("Paina Enter, kun kaikki operaatiot on suoritettu");

                    PrintOperations(Operations);

                    ReadLine();
                    break;
                }

                //Jos totta tehdään uusi operaatio olio ja käynnistetään asynkroninen suoritus sen arvoilla
                if (vastaus == "K" || vastaus == "k")
                {
                    LastOperationId++;

                    Operation operaatio = new Operation
                    {
                        Id = LastOperationId,
                        Breaks = r.Next(1, MaxBreaks),
                        TotalTimeInSeconds = r.Next(MinTimeInSeconds, MaxTimeInSeconds)
                    };

                    Operations.Add(operaatio);

                    Task tehtava = KaynnistaOperaatioAsync(operaatio);
                }
            }
        }

    }
}
