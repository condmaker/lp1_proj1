using System;

namespace WolfandSheep
{

    class Board
    {
        int sideNumb;
        //Just for testing. I don't know if I can use Lists
        public Tile[] darkTiles = new Tile[]{null,null,null,null,null,null,null
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
            //Total number of dark tiles in this board
            int totalDarkTiles = (int)(MathF.Pow(sideNumb/2,2));
            int tilenumb = 0;


            //
            for(int i = 0; i < (sideNumb/2); i++)
            {
                bool inicialPos = false;
                //Empty, Wolf or Sheep maybe change to enum
                int state = 0;

                for(int j = 0; j < (sideNumb/2); j++)
                {        
                    //Sheep spawn in the first line 
                    if(i == (sideNumb/2 - 1))
                    {
                        inicialPos = true;
                        state = 1;
                    }

                    int x = j;//sideNumb/2 - (j+1);
                    int y = i; 

                    //Create new tile and add it to the list of dark tiles
                    this.darkTiles[tilenumb] = new Tile(x, y, state, inicialPos);

                    //increment tile numb
                    tilenumb++;
                }
            }

            this.AssignNeighbours();
        }


        /// <summary>
        /// 
        /// </summary>
        public void AssignNeighbours()
        {           
            foreach(Tile t in darkTiles)
            {
                int tempX = 0;
                int tempY = 0;
                int tempNumb = 0;

                int tileX = t.x;
                int tileY = t.y; 
                

                if(tileY % 2 == 1)
                {
                    //Get left/next tile
                    tempX = t.x + 1;
                    tempY = t.y - 1;
                    if(tempY >= 0 && tempY < sideNumb 
                    && tempX >= 0 && tempX < sideNumb)
                    {
                      
                        tempNumb = tempX + (tempY * sideNumb/2);
                        t.neighbours[0] = this.darkTiles[tempNumb];
                        
                    }

                    //Get right/next tile

                    //Get left/previous tile

                    //Get right/previous tile
                }
                else
                {                    
                    //Get left/next tile
                    tempX = t.x + 1;
                    tempY = t.y;
                    if(tempY >= 0 && tempY > sideNumb 
                    && tempX >= 0 && tempX > sideNumb){
                        Console.WriteLine(t.x + " , " + t.y);
                        tempNumb = tempX + (tempY * sideNumb/2);
                        t.neighbours[0] = darkTiles[tempNumb];
                        //Console.WriteLine("> " + t.neighbours[0].x + " , " + t.neighbours[0].y);
                    }

                    //Get right/next tile

                    //Get left/previous tile

                    //Get right/previous tile

                }


            }
        }

        





        /// <summary>
        /// Constructs the visual game board in accordance with wolf, sheep, and
        /// gameBoard variables (SUBJECT TO CHANGE)
        /// </summary>
        public void ShowBoard()
        {

            foreach(Tile x in darkTiles)
            {
                
                //First tile in a line therefor start a new line 
                if(x.x == 0)
                {
                     Console.WriteLine(""); 
                     Console.WriteLine("");
                     Console.WriteLine("");
                }
                
                if(x.y % 2 == 1)
                {                 
                    x.PrintTileImage();
                    Console.Write("    |");
                }
                if(x.y % 2 == 0)
                {
                    Console.Write("    |");
                    x.PrintTileImage();
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
        public Tile[] neighbours = new Tile[]{null,null,null,null};
        
        public Tile(int x, int y, int state, bool isInitialPos = false)
        {     
            this.x = x;
            this.y = y;
            this.tileState = state;
            this.isInitialPos = isInitialPos;
        }
        
        public void PrintTileImage()
        {
            Console.Write(x + "  " + y);
            return;
            /*switch(this.tileState)
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
            }*/
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

            Console.WriteLine(">"  + ola.darkTiles[14].neighbours[0]);
            Console.WriteLine(ola.darkTiles[14].neighbours[0]);

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
