﻿using System;

namespace WolfandSheep
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prints the main menu and starts the 'input' string
            MainScreen();
            string mainInput = "";

            // Starts a loop that will continuously observe the player's inputs
            // and will end when 'q' is selected.
            while(mainInput != "q")
            {
                mainInput = Console.ReadLine();

                // The command list with all the functions
                switch (mainInput)
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
            Console.WriteLine("------------------------------");
            Console.WriteLine("|       Wolf and Sheep       |");
            Console.WriteLine("------------------------------");
            Console.WriteLine("|t - Game tutorial           |");
            Console.WriteLine("|s - Start the game          |");
            Console.WriteLine("|m - Print this menu         |");
            Console.WriteLine("|q - Quit the game           |");
            Console.WriteLine("------------------------------");
        }

        /// <summary>
        /// Shows to the player how to play the game with a concise and interac-
        /// tive tutorial.
        /// </summary>
        private static void GameTutorial()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("|       Introduction         |");
            Console.WriteLine("------------------------------");
            Console.WriteLine("|Welcome to WOLF AND SHEEP!  |");
            Console.WriteLine("|                            |");
            Console.WriteLine("|This is a multiplayer puzzle|");
            Console.WriteLine("|game, where one player con- |");
            Console.WriteLine("|trols 4 sheep, and the other|");
            Console.WriteLine("|controls a wolf.            |");

            if (!ContinueText()) return;

            Console.WriteLine("------------------------------");
            Console.WriteLine("|        Basic Rules         |");
            Console.WriteLine("------------------------------");
            Console.WriteLine("|There are only two ways to  |");
            Console.WriteLine("|end the game: The wolf wins,|");
            Console.WriteLine("|or the sheep win. But before|");
            Console.WriteLine("|explaining how to win, you  |");
            Console.WriteLine("|need to know how to play.   |");
            Console.WriteLine("|                            |");
            Console.WriteLine("|This game is played on a    |");
            Console.WriteLine("|board, much like chess and  |");
            Console.WriteLine("|checkers. The wolf is on the|");
            Console.WriteLine("|'top' of the board, and the |");
            Console.WriteLine("|4 sheep are on the bottom:  |");

            // Show the initial Game Board here with the class

            Console.WriteLine("|Much like those games too,  |");
            Console.WriteLine("|there are turns between the |");
            Console.WriteLine("|wolf and sheep player.      |");
            Console.WriteLine("|The wolf will always play   |");
            Console.WriteLine("|first at the start of the   |");
            Console.WriteLine("|game, and furthermore, on   |");
            Console.WriteLine("|the sheep player turn, he   |");
            Console.WriteLine("|will only be able to move   |");
            Console.WriteLine("|one sheep. Which one is up  |");
            Console.WriteLine("|to the player.              |");
            
            if (!ContinueText()) return;
            
            Console.WriteLine("------------------------------");
            Console.WriteLine("|          Movement          |");
            Console.WriteLine("------------------------------");
            Console.WriteLine("|Both the wolf and sheep can |");
            Console.WriteLine("|only move diagonally.       |");
            Console.WriteLine("|The sheep, however, can only|");
            Console.WriteLine("|move forward in relation to |");
            Console.WriteLine("|the board (in the player's  |");
            Console.WriteLine("|perspective, it may be more |");
            Console.WriteLine("|fitting to say they can only|");
            Console.WriteLine("|move 'up'), which means they|");
            Console.WriteLine("|can't go back if the wolf   |");
            Console.WriteLine("|surpasses them.             |");
            Console.WriteLine("|                            |");
            Console.WriteLine("|The wolf, on the other hand,|");
            Console.WriteLine("|can move forwards AND       |");
            Console.WriteLine("|backwards, which means he   |");
            Console.WriteLine("|can back off.               |");

            if (!ContinueText()) return;

            Console.WriteLine("------------------------------");
            Console.WriteLine("|     Winning Conditions     |");
            Console.WriteLine("------------------------------");
            Console.WriteLine("|In order for the sheep to   |");
            Console.WriteLine("|win the game, they need to  |");
            Console.WriteLine("|trap the wolf so that he    |");
            Console.WriteLine("|cannot move anymore, like   |");
            Console.WriteLine("|this:                       |");

            // Show an example Game Board of an trapped wolf also with the class

            Console.WriteLine("|...And in order for the wolf|");
            Console.WriteLine("|to win, all he needs is to  |");
            Console.WriteLine("|reach one of the starting   |");
            Console.WriteLine("|positions of the sheep, like|");
            Console.WriteLine("|this:                       |");

            // Show an example Game Board of the wolf winning.

            if (!ContinueText()) return;

            Console.WriteLine("------------------------------");
            Console.WriteLine("|           Control          |");
            Console.WriteLine("------------------------------");
            Console.WriteLine("|Now that the rules are all  |");
            Console.WriteLine("|explained, the game's       |");
            Console.WriteLine("|controls, for both the      |");
            Console.WriteLine("|wolf and the sheep, will be |");
            Console.WriteLine("|explained.                  |");

        }

        /// <summary>
        /// Simple function that prints repeating text in the GameTutorial() 
        /// function.
        /// </summary>
        private static bool ContinueText()
        {
            // Create a input string
            string input = "";
            
            Console.WriteLine("-----------------------------");
            Console.WriteLine("|   Enter 'c' to continue,   |");
            Console.WriteLine("|  or anything else to quit. |");
            Console.WriteLine("-----------------------------");

            // Read the user's command and store it on the string
            input = Console.ReadLine();

            // Verifies if the input is equal to 'c', and if so returns true,
            // on the contrary returning false
            if (input == "c") return true;
            else return false;

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
