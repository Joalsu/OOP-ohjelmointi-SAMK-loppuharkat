using System;
using System.Collections.Generic;
using static System.Console;
using System.Text;

namespace ECTentti02
{
    class ConsoleControl
    {
        //Ominaisuudet
        public List<string> Items { get; set; }

        public int Column { get; set; }
        public int Row { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public ConsoleColor BackColor { get; set;}
        public ConsoleColor TextColor { get; set; }

        //Konstruktori, jossa sijoitetaan saadut arvot ominaisuuksiin
        public ConsoleControl(int col, int row, int width, int height)
        {
            Column = col;
            Row = row;
            Width = width;
            Height = height;

            BackColor = BackgroundColor;
            TextColor = ForegroundColor;

            Items = null;
        }

        //Metodi, jossa tulostetaan välilyöntejä koko alueelle Esiksi
        //ottamalla kursorin sijainti muistiin ja lopuksi paluttaa sen sijainti
        public void Clear()
        {
            int l = CursorLeft;
            int t = CursorTop;

            for (int i = 0; i < l; i++)
            {
                Write(" ");
            }

            for (int i = 0; i < t; i++)
            {
                WriteLine(" ");
            }

            SetCursorPosition(l, t);
        }

        //Metodi jossa tulostetaan konsoli ikkunaan eri väriarvoilla kaikki Items-
        //ominaisuudesta ottamalla ensiksi talteen kurosrin sijainti ja oletus värit.
        public void Draw()
        {
            int l = CursorLeft;
            int t = CursorTop;

            ConsoleColor bc = BackColor;
            ConsoleColor tc = TextColor;

            int row = this.Row; 

            foreach (var item in this.Items)
            {
                SetCursorPosition(this.Column, this.Row);

                BackgroundColor = this.BackColor;
                ForegroundColor = this.TextColor;

                WriteLine($"{item}");

                this.Row++;
            }

            this.Row = row;
            SetCursorPosition(l, t);
            BackgroundColor = bc;
            ForegroundColor = tc;
        }
    }
}
