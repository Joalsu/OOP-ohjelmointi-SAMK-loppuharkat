using System;
using static System.Console;

namespace ECTentti03
{
    public class Program
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
