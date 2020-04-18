using System;

namespace WolfandSheep
{

    /// <summary>
    /// Class that makes and manages everything related to the board, with the
    /// exception of the Tiles, which has its own class
    /// </summary>
    class Board
    {
        // Number of 'sides' in the board. Only supports 8x8
        int sideNumb = 8;

        // The array of tiles that make the board. While internally it is 
        // considered as a single array, the player tackles it as a multi-
        // dimensional one
        public Tile[] darkTiles = new Tile[]{null,null,null,null,null,null,null
        ,null,null,null,null,null,null,null,null,null};

        // The basic class constructor. Will construct the board
        public Board()
        {     
            this.CreateBoard(this.sideNumb);
        }

        /// <summary>
        /// With the coordinates of a tile on the board, it will obtain a Tile
        /// class object with those same coordinates from the current board.
        /// </summary>
        /// <param name="x">The horizontal 'x' coordinate</param>
        /// <param name="y">The vertical 'y' coordinate</param>
        /// <returns>Tile class object</returns>
        public Tile GetTile(int x, int y)
        {
            // Creates the tile, empty
            Tile wantedTile = null;
            // Converts the coords. to an array
            int wantedTileNumb = ConvertToArrayNumb(x,y);
            // With the array, applies the correspondent tile on the board
            // to wantedTile, and then returns it
            wantedTile = this.darkTiles[wantedTileNumb];
            return wantedTile;
        }

        /// <summary>
        ///  Creates the list of all dark tiles based on the chosen size 
        ///  of the board  
        /// </summary>
        /// <param name="sideNumb">Board size int</param>
        public void CreateBoard(int sideNumb)
        {           
            // Will multiply the number by the power of two to obtain a 
            // squared board (like 8x8, for example)
            int totalDarkTiles = (int)(MathF.Pow(sideNumb/2,2));
            // Variable that will increment the tile list
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
                    this.darkTiles[tilenumb] = 
                    new Tile(x, y, state, tilenumb, inicialPos);

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

            
            foreach(Tile t in this.darkTiles)
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
        /// With a chosen tile and type of neighbour and a Tile
        /// object it will get one of it's neighbours.
        /// </summary>
        /// <param name="typeOfNeighbour">Array of ints</param>
        /// <param name="currentTile">Tile object</param>
        public void GetNeighbour(int[] typeOfNeighbour, Tile currentTile)
        {
            // Variable to store the coordinates of the neighbour
            int tempNumb;            
            
            // Variables that will change depending on the neighbour type
            int xInteraction = 0;
            int yInteraction = 0;

            // Depending on the type of neighbour (if its odd or even and then
            // if it is the first -> fourth neighbour) will assign different 
            // values to xInteraction and yInteraction, that will match 
            // the wanted neighbour's coordinates correctly
            if(typeOfNeighbour[0] == 0)
            {
                switch(typeOfNeighbour[1])
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
        
                switch(typeOfNeighbour[1])
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

            // Assigns the current neighbour's coordinates do tempX and tempY
            int tempX = currentTile.x + xInteraction;
            int tempY = currentTile.y + yInteraction;
            
            
            // Verifies if neighbour is not impossible (if it does not go off 
            // the map)
            if(tempY >= 0 && tempY < sideNumb/2 
            && tempX >= 0 && tempX < sideNumb/2)
            {
                // Converts the neighbour's coordinates to an int array and
                // applies it to the selected tile's neighbour list
                tempNumb = ConvertToArrayNumb(tempX, tempY);           
                currentTile.neighbours[typeOfNeighbour[1]] = 
                this.darkTiles[tempNumb];                     
            }
        }


        /// <summary>
        /// Converts the coordinates of a 2 dimensional array to
        /// the index of an 1 dimensional array. 
        /// Only works with certain arrays 
        /// </summary>
        /// <param name="x">Horizontal 'x' coordinate</param>
        /// <param name="y">Vertical 'y' coordinate</param>
        /// <returns>The int index of the given array coords.</returns>
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
            Console.WriteLine("    1    2    3    4    5    6    7    8");
            Console.WriteLine("  -----------------------------------------");
            Console.Write("A |");
            foreach(Tile t in darkTiles)
            {
                //First tile in a line therefor start a new line 
                if(t.x == 0 && t.y != 0)
                {
                    Console.WriteLine("");
                    switch (t.y)
                    {
                        case 1: 
                            Console.WriteLine("B |\t\t\t\t\t  |");
                            Console.Write("C"); 
                            break;
                        case 2: 
                            Console.WriteLine("D |\t\t\t\t\t  |");
                            Console.Write("E"); 
                            break;
                        case 3: 
                            Console.WriteLine("F |\t\t\t\t\t  |"); 
                            Console.Write("G"); 
                            break;
                    }
                    
                    Console.Write(" |");
                }
                
                if(t.y % 2 == 1)
                {            
                    t.PrintTileImage();
                    Console.Write("    |");
                }
                if(t.y % 2 == 0)
                {
                    Console.Write("    |");
                    t.PrintTileImage();
                }         
      
            }

            Console.WriteLine(
                "\n  -----------------------------------------\n");
            
        }

    }

    class Tile
    {
        bool isInitialPos;

        //0 - empty / 1 - sheep / 2 - wolf 
        public int tileState;
        public int x, y;
        public int index;
        public Tile[] neighbours = new Tile[]{null,null,null,null};
        
        public Tile(int x, int y, int state, int index,
         bool isInitialPos = false)
        {    
            this.index = index;
            this.x = x;
            this.y = y;
            this.tileState = state;
            this.isInitialPos = isInitialPos;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void PrintTileImage()
        {
            
            /*Console.Write("  " + index + "  ");
            return;*/

            /*Console.Write(x + " , " + y);
            return;*/

            
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
    	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetTile"></param>
        /// <returns></returns>
        public bool CheckTileAvailability(Tile targetTile)
        {
            bool available = false;

            if(targetTile.tileState == 0){available = true;}
            
            foreach(Tile n in this.neighbours)
            {

                if(n == null){ continue; }

                if(n.index == targetTile.index)
                {
                    available = true;
                }
            }

            return available;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CheckIfSurrounded()
        {
            bool isSurrounded = true;

            //Check if any neighbour is empty
            foreach(Tile n in this.neighbours)
            {
                //Ignore if null
                if(n == null){ continue; }
                    
                Console.WriteLine(n.tileState);
                if(n.tileState == 0)
                {

                    isSurrounded = false;
                }
            }
            return isSurrounded;
        }

    }               


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
                mainInput = Console.ReadLine().ToLower();

                // The command list with all the functions
                switch (mainInput)
                {
                    case "t":
                        GameTutorial();
                        break;

                    case "s":
                        GameStart();
                        mainInput = "q";
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
        /// 
        /// </summary>
        private static void GameStart()
        {
            // Creates the game board and a string that will store the input of
            // both players
            Board gameBoard = new Board();
            string playersInput = null; 

            // Variable that will represent the number of turns when the game
            // begins
            int turnCount = 1;
            // Variable that will store the player's commands with an array of
            // strings
            string[] gameCommand;
            // Declaration of the array of coordinates that will be made by
            // the player's input
            int[] tileNumArray;
            // Declaration of the variable that will store the index of
            // the coordinates
            int tileNum;

            // Shows the board to the player
            gameBoard.ShowBoard();
            
            // Asks one of the players to select the wolf's position
            Console.WriteLine(
                "PLAYER1, please select the position for the WOLF, "  +
                "the character you will play as. You can only choose" +
                "tiles from the A row. Input the command as:\n\n"     +
                "choose <vertical>\n\n"+
                "If you choose an invalid number on that row, the"    +
                "will convert it to the highest possible number on "  +
                "that row.\n");

            while (true)
            {
                // Will store the player's input on the aforementioned variable,
                // and then will split it in order to read each command easily
                playersInput = Console.ReadLine();
                string[] firstCommand = playersInput.Split(" ");
                
                // Leaves the function (needs work to leave program)
                if (playersInput == "q") break;

                // Observes the length and naming of the player's input, and
                // deems it valid or not
                if (firstCommand.Length == 1)
                {
                    Console.WriteLine("Not enough commands.");
                    continue;
                }
                
                if (firstCommand[0].ToLower() != "choose")
                {
                    Console.WriteLine(
                        "Command not recognized. Input command as:\n" +
                        "choose <tile>");
                    continue;
                }

                // Transforms the player input to an array of ints
                tileNumArray = CoordToInt('A', firstCommand[1][0]);

                // Observes if the vertical tile number inputted by the player
                // is valid on the situation
                if (tileNumArray[1] > 8)
                {
                    Console.WriteLine(
                        "Invalid Coordinate. Please input a valid tile.");
                    continue;
                }

                // Converts the int array to an index, so it can be given to
                // the game board
                tileNum = gameBoard.ConvertToArrayNumb(
                    tileNumArray[0], tileNumArray[1]);

                // Observes if tile is out of range
                try
                {
                    gameBoard.darkTiles[tileNum].tileState = 2;
                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine(
                        "Invalid Coordinate. Please look at the board and " +
                        "input again.");
                    continue;
                }

                // Reconstructs the Game Board and shows it to the player
                gameBoard.ShowBoard(); 
            }

            // Starts the game on a loop that breaks if the input is equal to
            // 'q'.
            while (playersInput != "q")
            {
                if (turnCount % 2 != 0)
                {
                    Console.WriteLine("PLAYER1, make your move.");
                    Console.WriteLine("|MOVE COMMAND: <move> <tile>|");

                    playersInput = Console.ReadLine();
                    gameCommand = playersInput.Split(" ");

                    if (gameCommand[0] != "move")
                    {
                        Console.WriteLine("Unknown Input. Please input again.");
                        continue;
                    } 

                    tileNumArray = CoordToInt(
                        gameCommand[1][0], gameCommand[1][1]);

                    tileNum = gameBoard.ConvertToArrayNumb(
                        tileNumArray[0], tileNumArray[1]);

                }
                else
                {
                    Console.WriteLine("PLAYER2, make your move.");
                    Console.WriteLine(
                        "|MOVE COMMAND: <move> <tile>|    "+
                        "|CHOOSE SHEEP COMMAND: <choose> <tile>|");

                    playersInput = Console.ReadLine();
                    gameCommand = playersInput.Split(" ");
                }
                
                // Increases the turn count and goes back to the beginning
                turnCount++;
            }

            return;
            
        }

        /// <summary>
        /// Converts two chars into an array of ints. The first char needs to be
        /// a number from A-to-G, and the second one a number.
        /// </summary>
        /// <param name="a">A-to-G char</param>
        /// <param name="b">A number in the form of a char</param>
        /// <returns>Array of ints</returns>
        private static int[] CoordToInt(char a, char b)
        {
            // Declaration of the used variables
            int xValue;
            int yValue;

            // Converts each char to their respective numbers in board 
            // coordinates
            xValue = (int)(MathF.Ceiling((float)(char.GetNumericValue(b)/ 2) - 1));
            yValue = (int)(char.ToUpper(a) - 65);

            // Returns the int array with the coordinates
            return new int[] {xValue, yValue};

        }

        /// <summary>
        /// Shows to the player how to play the game with a concise and interac-
        /// tive tutorial.
        /// </summary>
        private static void GameTutorial()
        {
            Board tutorialBoard = new Board();
            while (true)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("|       Introduction         |");
                Console.WriteLine("------------------------------");
                Console.WriteLine("|Welcome to WOLF AND SHEEP!  |");
                Console.WriteLine("|                            |");
                Console.WriteLine("|This is a multiplayer puzzle|");
                Console.WriteLine("|game, where one player con- |");
                Console.WriteLine("|trols 4 sheep, and the other|");
                Console.WriteLine("|controls a wolf. Here is an |");
                Console.WriteLine("|board example:              |\n");

                tutorialBoard.ShowBoard();

                Console.WriteLine("|Sheep are represented with  |");
                Console.WriteLine("|MEEH on the board, while the|");
                Console.WriteLine("|wolf is represented with    |");
                Console.WriteLine("|WOOF.                       |");
                Console.WriteLine("|You can only move your      |");
                Console.WriteLine("|pieces to the tiles with    |");
                Console.WriteLine("|indents, like this: |----|  |");
                Console.WriteLine("|                            |");
                Console.WriteLine("|Horizontal lines are        |");
                Console.WriteLine("|represented with numbers    |");
                Console.WriteLine("|(1-8) and vertical lines    |");
                Console.WriteLine("|with letters (A-G). In order|");
                Console.WriteLine("|to reference a tile in a    |");
                Console.WriteLine("|command, you put first the  |");
                Console.WriteLine("|letter, then the number,    |");
                Console.WriteLine("|for example:                |\n");
                Console.WriteLine("D4\n");


                if (!ContinueText()) break;

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
                Console.WriteLine("|4 sheep are on the bottom.  |");
                Console.WriteLine("|                            |");
                Console.WriteLine("|Much like those other board |");
                Console.WriteLine("|games, there are turns      |");
                Console.WriteLine("|between the wolf and sheep  |");
                Console.WriteLine("|player.                     |");
                Console.WriteLine("|The wolf will always play   |");
                Console.WriteLine("|first at the start of the   |");
                Console.WriteLine("|game, and furthermore, on   |");
                Console.WriteLine("|the sheep player turn, he   |");
                Console.WriteLine("|will only be able to move   |");
                Console.WriteLine("|one sheep. Which one is up  |");
                Console.WriteLine("|to the player.              |");
                
                if (!ContinueText()) break;
                
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

                if (!ContinueText()) break;

                Console.WriteLine("------------------------------");
                Console.WriteLine("|     Winning Conditions     |");
                Console.WriteLine("------------------------------");
                Console.WriteLine("|In order for the sheep to   |");
                Console.WriteLine("|win the game, they need to  |");
                Console.WriteLine("|trap the wolf so that he    |");
                Console.WriteLine("|cannot move anymore, like   |");
                Console.WriteLine("|this:                       |");

                // Show an example Game Board of an trapped wolf also with the 
                // class

                Console.WriteLine("|...And in order for the wolf|");
                Console.WriteLine("|to win, all he needs is to  |");
                Console.WriteLine("|reach one of the starting   |");
                Console.WriteLine("|positions of the sheep, like|");
                Console.WriteLine("|this:                       |");

                // Show an example Game Board of the wolf winning.

                if (!ContinueText()) break;

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
                break;

            }
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
            Console.WriteLine("|or anything else to go back.|");
            Console.WriteLine("-----------------------------");

            // Read the user's command and store it on the string
            input = Console.ReadLine();

            // Verifies if the input is equal to 'c', and if so returns true,
            // on the contrary returning false
            if (input == "c") return true;
            else return false;

        }

    }
}
