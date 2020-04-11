using System;

namespace WolfandSheep
{
    class Program
    {
        static void Main(string[] args)
        {
            MainScreen();
            string input = "";

            while(input != "q")
            {

            }
        }

        private static void MainScreen()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("| ＷＯＬＦ　ＡＮＤ　ＳＨＥＥＰ |");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("|t - Game tutorial           |");
            Console.WriteLine("|s - Start the game          |");
            Console.WriteLine("|q - Quit the game           |");
            Console.WriteLine("-----------------------------");
        }
    }
}
