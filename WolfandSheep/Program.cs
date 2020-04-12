using System;

namespace WolfandSheep
{

    class Board
    {
        int sideNumb;
        //Just for testing. I don't know if I can use Lists
        Tile[] darkTiles = new Tile[]{null,null,null,null,null,null,null
        ,null,null,null,null,null,null,null,null,null,};



        public Board(int sideNumb)
        {     
            if(sideNumb % 2 == 0)
            {
                this.sideNumb = sideNumb;
                this.CreateBoard(sideNumb);
            }
        }


        /// <summary>
        ///  Creates the list of all dark tiles based on the chosen size 
        ///  of the board  
        /// </summary>
        public void CreateBoard(int sideNumb)
        {           
            int totalDarkTiles = (int)(MathF.Pow(sideNumb/2,2));
            int tilenumb = 1;
            for(int i = 0; i < (sideNumb/2); i++)
            {
                bool inicialPos = false;
                int state = 0;

                for(int j = 0; j < (sideNumb/2); j++)
                {        
                    
                    if(i == 0)
                    {
                        inicialPos = true;
                        state = 1;
                    }


                    int x = sideNumb/2 - (j+1);
                    int y = i; 

                    this.darkTiles[ totalDarkTiles - tilenumb] = new Tile(x, y, state, inicialPos);
                    tilenumb++;
                }
            }

            //this.AssignNeighbours();
        }


        /// <summary>
        /// 
        /// </summary>
        public void AssignNeighbours()
        {           

        }

        /// <summary>
        /// Constructs the visual game board in accordance with wolf, sheep, and
        /// gameBoard variables (SUBJECT TO CHANGE)
        /// </summary>
        public void ShowBoard()
        {
            
            foreach(Tile x in darkTiles)
            {
                x.PrintTileImage();

                int finalTile = (sideNumb/2) - 1;

                if(x.x == finalTile && x.y % 2 == 1)
                {
                     Console.WriteLine(""); 
                     Console.WriteLine("");
                     Console.WriteLine("");
                }

                

                if(x.x != finalTile || x.y % 2 != 0)
                {
                    Console.Write("    |");
                }
                else
                {
                    Console.WriteLine(""); 
                    Console.WriteLine("");
                    Console.WriteLine(""); 
                }
            }

            Console.WriteLine(""); 
            
        }

    }



    class Tile
    {
      
        bool isInitialPos;

        //0 - empty / 1 - sheep / 2 - wolf 
        int tileState;
        public int x, y;
        Tile[] neighbours;
        
        public Tile(int x, int y, int state, bool isInitialPos = false)
        {     
            this.x = x;
            this.y = y;
            this.tileState = state;
            this.isInitialPos = isInitialPos;
        }

 
        
        public void PrintTileImage()
        {
            switch(this.tileState)
            {
                case 0:
                    Console.Write("----|");
                    break;
                case 1:
                    Console.Write("MEEH|");
                    break;
                case 2:
                    Console.Write("WOOF|");
                    break;
                default:
                    Console.Write("If u are seeing this I did a bad job");
                    break;
            }
        }


    }



    class Program
    {

        private static Tile[] darkTiles;
        private static Board defaultBoard;


        static void Main(string[] args)
        {
            // Prints the main menu and starts the 'input' string
            MainScreen();
            string input = "";

            Board ola = new Board(8);
            ola.ShowBoard();

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
