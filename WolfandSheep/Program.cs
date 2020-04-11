using System;

namespace WolfandSheep
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prints the main menu and starts the 'input' string
            MainScreen();
            string input = "";

            // Starts a loop that will continuously observe the player's inputs
            // and will end when 'q' is selected.
            while(input != "q")
            {
                input = Console.ReadLine();

                // The command list with all the functions
                switch (input)
                {
                    case "t":
                        GameTutorial();
                        break;

                    case "s":
                        break;

                    case "m":
                        MainScreen();
                        break;

                    case "q":
                        break;

                    default:
                        Console.WriteLine(
                            "Unknown Option. Press m to print the menu again.");
                        break;
                }
            }

            // Program says bye to player before ending
            Console.WriteLine("Bye now!");
        }

        /// <summary>
        /// Prints the game's main menu.
        /// </summary>
        private static void MainScreen()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("|ＷＯＬＦ　ＡＮＤ　ＳＨＥＥＰ|");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("|t - Game tutorial           |");
            Console.WriteLine("|s - Start the game          |");
            Console.WriteLine("|m - Print this menu         |");
            Console.WriteLine("|q - Quit the game           |");
            Console.WriteLine("-----------------------------");
        }

        /// <summary>
        /// Shows to the player how to play the game.
        /// </summary>
        private static void GameTutorial()
        {

        }

        /// <summary>
        /// Constructs the visual game board in accordance with wolf, sheep, and
        /// gameBoard variables (SUBJECT TO CHANGE)
        /// </summary>
        private static void ShowBoard()
        {

        }
    }
}
