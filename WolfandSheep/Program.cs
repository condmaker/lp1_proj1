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
        /// Assigns the neighbours to every tile 
        /// </summary>
        public void AssignNeighbours()
        {           
            foreach(Tile t in darkTiles)
            {                   
                if(t.y % 2 == 1)
                {
                    //Get left/next tile
                    GetNeighbour(new int[]{1,0}, t);

                    //Get right/next tile
                    GetNeighbour(new int[]{1,1}, t);

                    //Get left/previous tile
                    GetNeighbour(new int[]{1,2}, t);

                    //Get right/previous tile
                    GetNeighbour(new int[]{1,3}, t);
                }
                else
                {           
                    //Get left/next tile
                    GetNeighbour(new int[]{0,0}, t);

                    //Get right/next tile
                    GetNeighbour(new int[]{0,1}, t);

                    //Get left/previous tile
                    GetNeighbour(new int[]{0,2}, t);

                    //Get right/previous tile
                    GetNeighbour(new int[]{0,3}, t);
                }
            }
        }


        /// <summary>
        /// Decides the neighbour that is in the chosen position
        /// </summary>
        public void GetNeighbour(int[] typeOfNeihgbour, Tile currentTile)
        {
            int tempNumb;
            
            
            int xInteraction = 0;
            int yInteraction = 0;

            //Decides how to interact with the coordinates to 
            //get the selected neighbour
            
            if(typeOfNeihgbour[0] == 0)
            {
                switch(typeOfNeihgbour[1])
                {
                    case 0:
                        yInteraction = 1;
                        break;
                    case 1:
                        xInteraction = 1;              
                        yInteraction = 1;
                        break;
                    case 2:
                        yInteraction = -1;
                        break;
                    case 3:
                        xInteraction = 1;
                        yInteraction = -1;
                        break;
                }
            }
            else
            {               
                switch(typeOfNeihgbour[1])
                {
                    case 0:
                        xInteraction = -1;
                        yInteraction = 1;
                        break;
                    case 1:        
                        yInteraction = 1;
                        break;
                    case 2:
                        xInteraction = -1;
                        yInteraction = -1;
                        break;
                    case 3:
                        yInteraction = -1;
                        break;
                }

            }


            int tempX = currentTile.x + xInteraction;
            int tempY = currentTile.y + yInteraction;
            

            if(tempY >= 0 && tempY < sideNumb/2 && tempX >= 0 && tempX < sideNumb/2)
            {
                
                tempNumb = ConvertToArrayNumb(tempX, tempY);
                currentTile.neighbours[typeOfNeihgbour[1]] = darkTiles[tempNumb];                     
            }
        }


        /// <summary>
        /// Converts the coordinates of a 2 dimensional array to
        /// the index of an 1 dimensional array. 
        /// Only works with certain arrays 
        /// </summary>
        public int ConvertToArrayNumb(int x, int y)
        {
            int arrayNumb = x + (y * sideNumb/2);
            return arrayNumb;
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
