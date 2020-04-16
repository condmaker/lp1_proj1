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
            string mainInput = "";

            Board ola = new Board(8);
            ola.ShowBoard();

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
            Console.WriteLine("|                            |");
            Console.WriteLine("|As shown on the boards above|");
            Console.WriteLine("|each line will have a number|");
            Console.WriteLine("|and each column a letter    |");
            Console.WriteLine("|assigned to them. By using  |");
            Console.WriteLine("|the move <column><line>     |");
            Console.WriteLine("|command, you will be able to|");
            Console.WriteLine("|move a specific character to|");
            Console.WriteLine("|a desired tile, like this:  |\n");
            Console.WriteLine(
                "move B3\n");
            Console.WriteLine("|If the character can move to|");
            Console.WriteLine("|That place, the game will   |");
            Console.WriteLine("|perform the action and end  |");
            Console.WriteLine("|your turn. If the game does |");
            Console.WriteLine("|not recognize the tile      |");
            Console.WriteLine("|inputted, it will show      |");
            Console.WriteLine("|the following error message:|\n");
            Console.WriteLine("Unknown tile, please input again!\n");
            Console.WriteLine("|...And if you cannot move   |");
            Console.WriteLine("|to your selected tile:      |\n");
            Console.WriteLine(
                "You cannot move to this tile, please input again!\n");
            Console.WriteLine("|If you're playing as the    |");
            Console.WriteLine("|sheep, you need to do the   |");
            Console.WriteLine("|select <column><line>       |");
            Console.WriteLine("|command in order to choose  |");
            Console.WriteLine("|the sheep that will move:   |\n");
            Console.WriteLine(
                "select C2\n");
            Console.WriteLine("|The tile selected must have |");
            Console.WriteLine("|a sheep on it!              |");
            Console.WriteLine("|As for the wolf, since he is|");
            Console.WriteLine("|by himself, he only needs to|");
            Console.WriteLine("|perform the 'move' command. |");
            Console.WriteLine("|One more thing before we    |");
            Console.WriteLine("|wrap up: don't forget that  |");
            Console.WriteLine("|in-game you can input the   |");
            Console.WriteLine("|'q' key and enter as a      |");
            Console.WriteLine("|command any time to quit the|");
            Console.WriteLine("|game.                       |");
            Console.WriteLine("------------------------------");
            Console.WriteLine("|    You have returned to    |");
            Console.WriteLine("|       the main menu.       |");
            Console.WriteLine("------------------------------");

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
