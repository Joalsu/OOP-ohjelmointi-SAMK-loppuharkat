using System;
using System.Collections.Generic;
using static System.Console;
using System.Text;

namespace ECTehtava4
{
    class Operation
    {
        //ominaisuudet

        public int Id { get; set; }
        public int TotalTimeInSeconds { get; set; }
        public int SpendTimeInSeconds { get; set; }
        public int Breaks { get; set; }

        public DateTime Started { get; set; }
        public DateTime Ended { get; set; }

        //konstruktorit

        public Operation()
        {
            Started = DateTime.Now;
        }

        //metodit

        public void Print()
        {
            //Otetaan talteen kohdistin ja lopuksi palautetaan se

            int cursoriT = CursorTop;
            int cursoriW = CursorLeft;
            SetCursorPosition(1, Id);
            double lasku = (SpendTimeInSeconds * 1.0 / TotalTimeInSeconds * 1.0) * 100;

            Write($"{Id} {Started.ToLongTimeString()} {Math.Round(lasku,2)} %");

            SetCursorPosition(cursoriW, cursoriT);
        }

        public void PrintEnded()
        {
            int cursoriT = CursorTop;
            int cursoriW = CursorLeft;

            SetCursorPosition(0, Id);

            Write($"{Id} {Started.ToLongTimeString()} - {Ended.ToLongTimeString()} = duration {Ended.Second - Started.Second} seconds");

            SetCursorPosition(cursoriW, cursoriT);
        }
    }
}
