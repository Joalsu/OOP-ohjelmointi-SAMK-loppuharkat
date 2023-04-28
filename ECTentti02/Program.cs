using System;
using static System.Console;

namespace ECTentti02
{
    class Program
    {
        static void Main()
        {
            try
            {
                Application.Run();
            }
            catch (Exception e)
            {
                Write($"{e.Message}");
                ReadLine();
            }
        }
    }
}
