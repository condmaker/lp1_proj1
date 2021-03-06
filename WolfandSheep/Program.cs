﻿using System;

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
        ,null,null,null,null,null,null,null,null,null,null,null,null,null,null
        ,null,null,null,null,null,null,null,null,null,null,null};

        // The basic class constructor. Will construct the board after called
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

            // A double for cicle, that will account vertical and horizontal
            // tiles in the construction
            for(int i = 0; i < (sideNumb); i++)
            {
                bool inicialPos = false;
                // The tile state. 0 = Empty Tile, 1 = Sheep Tile, 2 = Wolf Tile
                int state = 0;

                for(int j = 0; j < (sideNumb/2); j++)
                {        
                    // Spawning the sheep always at the first line from the 
                    // bottom
                    if(i == (sideNumb - 1))
                    {
                        inicialPos = true;
                        state = 1;
                    }

                    int x = j;
                    int y = i; 

                    // Create new tile and add it to the list of dark tiles
                    this.darkTiles[tilenumb] = 
                    new Tile(x, y, state, tilenumb, inicialPos);

                    // Increment tile numb
                    tilenumb++;
                }
            }

            // Assigns the neighbours for each tile at the board, for the rest
            // of the game
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
            if(tempY >= 0 && tempY < sideNumb 
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
        /// gameBoard variables
        /// </summary>
        public void ShowBoard()
        {
            Console.WriteLine("    1    2    3    4    5    6    7    8");
            Console.WriteLine("  -----------------------------------------");
            Console.Write("A |");
            foreach(Tile t in darkTiles)
            {
                if(t.x == 0 && t.y != 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("  |\t\t\t\t\t  |");
                    switch (t.y)
                    {
                        case 1:          
                            Console.Write("B"); 
                            break;
                        case 2: 
                            Console.Write("C"); 
                            break;
                        case 3: 
                            Console.Write("D"); 
                            break;
                        case 4: 
                            Console.Write("E"); 
                            break;
                        case 5: 
                            Console.Write("F"); 
                            break;
                        case 6: 
                            Console.Write("G"); 
                            break;
                        case 7: 
                            Console.Write("H"); 
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

    /// <summary>
    /// Class that manages individual tiles, their state, coordinates (in an 
    /// index value), and their own neighbours.
    /// </summary>
    class Tile
    {
        // A tile that verifies if its the initial position of a sheep. It is
        // there in order for the wolf to win
        public bool isInitialPos;
        // The state of the tile, if it is empty, or has a sheep/wolf.
        // 0 - Empty | 1 - Sheep | 2 - Wolf
        public int tileState;
        // The tile's coordinates on a numerical system
        public int x, y;
        // The index coordinate of the tile, to access the array
        public int index;
        // The tile's neighbours
        public Tile[] neighbours = new Tile[]{null,null,null,null};
        
        // The basic constructor of the tile
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
        /// Prints an 'image' of the tile based on its state.
        /// </summary>
        public void PrintTileImage()
        {
            // Verifies the tile state and prints the 'image' accordingly
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
                    Console.Write("Error.");
                    break;
            }
            
        }
    	
        /// <summary>
        /// Compares one tile to another, and observes if the player can move 
        /// to this another tile.
        /// </summary>
        /// <param name="targetTile">The new Tile class object</param>
        /// <returns>Bool that says if it cans or not</returns>
        public bool CheckTileAvailability(Tile targetTile)
        {
            bool available = false;
            
            foreach(Tile n in this.neighbours)
            {

                if(n == null){ continue; }

                if((n.index == targetTile.index) && (targetTile.tileState == 0))
                {
                    available = true;
                }
            }

            // Verifies if inputted tile is the same as before
            if (targetTile == this) available = true;

            return available;
        }

        /// <summary>
        /// Observes if the wolf is surrounded in his tile
        /// </summary>
        /// <returns>Boolean</returns>
        public bool CheckIfSurrounded()
        {
            // Starts with the assumption that is true, verifies if its false
            // in case any of the triggers are true
            bool isSurrounded = true;

            // Check if any neighbour is empty
            foreach(Tile n in this.neighbours)
            {
                // Ignore if null
                if(n == null){ continue; }

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
        private static void Main(string[] args)
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
        /// Method that starts the game.
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
            // Stores the final game state-- if the wolf or sheep won.
            // Sheep wins is 0, Wolf loses is 1
            ushort gameState = 3;
            // Variable that will store the player's commands with an array of
            // strings
            string[] gameCommand;
            // Declaration of the array of coordinates that will be made by
            // the player's input
            int[] tileNumArray;
            // Declaration of the variable that will store the index of
            // the coordinates
            int tileNum = 0;

            // Shows the board to the player
            gameBoard.ShowBoard();
            
            // Asks one of the players to select the wolf's position
            Console.WriteLine(
                "PLAYER1, please select the position for the WOLF, "  +
                "the character you will play as. You can only choose " +
                "tiles from the A row. Input the command as:\n\n"     +
                "choose <horizontal number>\n\n"+
                "If you choose an invalid number on that row, the game "    +
                "will convert it to the highest possible number on "  +
                "that row. Keep in mind that this is also valid for the " + 
                "rest of the in-game commands. If you input a unavailable " +
                "tile, it may round it to the nearest tile.\n");

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

                break;
            }

            // Starts the game on a loop that breaks if the input is equal to
            // 'q'.
            while (playersInput != "q")
            {
                // Stores the number of the anterior tile to see if the player
                // can move in wanted direction
                int oldNum = 0; 
                int wolfNum = 0;

                // A for cicle to discover where is the wolf, then storing the
                // location index on the wolfNum variable
                for (int i = 0; i <= gameBoard.darkTiles.Length; i++)
                {
                    if (gameBoard.darkTiles[i].tileState == 2)
                    {
                        wolfNum = i;
                        break;
                    } 
                }

                // Checks if wolf has won
                if (gameBoard.darkTiles[wolfNum].isInitialPos == true)
                {
                    gameState = 1;
                    break;
                }

                // Checks if the wolf is surrounded
                if (gameBoard.darkTiles[wolfNum].CheckIfSurrounded())
                {
                    gameState = 0;
                    break;
                }

                // Shows the current turn to the player
                Console.WriteLine($"\nTURN {turnCount}");
                // Reconstructs the Game Board and shows it to the player
                gameBoard.ShowBoard();   

                // Observes which player makes their move if turnCount is odd
                // or even
                if (turnCount % 2 != 0)
                {
                    Console.WriteLine("PLAYER1 (Wolf), make your move.");
                    Console.WriteLine("|MOVE COMMAND: move <tile>|");

                    // Saves the wolf location on oldNum
                    oldNum = wolfNum;
                }
                else
                {
                    Console.WriteLine("PLAYER2 (Sheep), choose your sheep.");
                    Console.WriteLine(
                        "|CHOOSE SHEEP COMMAND: choose <tile>|");

                    // Reads the player input
                    playersInput = Console.ReadLine();

                    // Observes if said input is equal to q, and if so it 
                    // changes the gameState variable to 3, in order to leave
                    // the program immediately when leaving the loop
                    if (playersInput == "q") 
                    {
                        gameState = 3;
                        continue;
                    }

                    // Splits the player command in order to analyze it better
                    gameCommand = playersInput.Split(" ");

                    // Observes if player command, is different from choose,
                    // returning to the beggining if that is the case
                    if (gameCommand[0] != "choose")
                    {
                        Console.WriteLine(
                            "Please choose a tile with your desired sheep to " +
                            "move.");
                        continue;
                    }

                    // Converts the desired tile to an array of ints with the
                    // correct coordinates
                    tileNumArray = CoordToInt(
                        gameCommand[1][0], gameCommand[1][1]);

                    // Obtains the index from the inputted coordinates
                    tileNum = gameBoard.ConvertToArrayNumb(
                        tileNumArray[0], tileNumArray[1]);

                    // Checks if inputted tile has a sheep on it (MAY NEED DBG)
                    if (gameBoard.darkTiles[tileNum].tileState != 1)
                    {
                        Console.WriteLine(
                            "There is no sheep on this tile. Please input " +
                            "again.");
                        continue;
                    }

                    // Saves the chosen sheep location on oldNum
                    oldNum = tileNum;

                    Console.WriteLine("Sheep Chosen!\n");
                    Console.WriteLine("PLAYER2 (Sheep), make your move.");
                    Console.WriteLine("|MOVE COMMAND: move <tile>|");

                }

                // Reads the player input for movement of the piece
                playersInput = Console.ReadLine();

                if (playersInput == "q") 
                {
                    gameState = 3;
                    continue;
                }

                gameCommand = playersInput.Split(" ");

                // Checks if the command is different from move, and if so, 
                // returns to the beginning
                if (gameCommand[0] != "move")
                {
                    Console.WriteLine("Unknown Input. Please input again.");
                    continue;
                } 

                // Converts the desired tile to an array of ints with the
                // correct coordinates, if it is not able to do that, the 
                // program detects an error where the tile is out of range, 
                // and returns to the beginning of the loop
                try
                {
                    tileNumArray = CoordToInt(
                    gameCommand[1][0], gameCommand[1][1]);
                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine("Tile out of range. Input again.");
                    continue;
                }


                
                // Converts the gameCommand coords to an array of ints
                tileNumArray = CoordToInt(
                    gameCommand[1][0], gameCommand[1][1]);
                
                // Obtains the index from the inputted coordinates
                tileNum = gameBoard.ConvertToArrayNumb(
                    tileNumArray[0], tileNumArray[1]);

                // With the present player tile, checks if he can go to next
                // tile
                if (!gameBoard.darkTiles[oldNum].CheckTileAvailability(
                    gameBoard.darkTiles[tileNum]))
                {
                     Console.WriteLine("You cannot go to this tile. " +
                                       "please input again.");
                     continue;
                }

                 // Makes sure that sheep can't go backwards
                if ( (gameBoard.darkTiles[oldNum].y 
                < gameBoard.darkTiles[tileNum].y) && turnCount % 2 == 0)
                {
                    Console.WriteLine(
                        "You cannot go back as sheep. Please input again.");
                    continue;
                }

                // Assigns tile state depending if the wolf or sheep made the
                // play, and resets old tile state
                if (turnCount % 2 != 0)
                {
                    gameBoard.darkTiles[oldNum].tileState = 0;
                    gameBoard.darkTiles[tileNum].tileState = 2;             
                }
                else
                {
                    gameBoard.darkTiles[oldNum].tileState = 0;
                    gameBoard.darkTiles[tileNum].tileState = 1;
                }       

                // Increases the turn count and goes back to the beginning
                turnCount++;
            }

            // Observes the game state and prints the correct text-- if the 
            // sheep or wolf won, or if the player has quit the game.
            switch(gameState)
            {
                case 0:
                    gameBoard.ShowBoard();
                    Console.WriteLine("PLAYER2, the sheep, wins!");
                    break;
                case 1: 
                    gameBoard.ShowBoard();
                    Console.WriteLine("PLAYER1, the Wolf, wins!");
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine("Error.");
                    break;
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
            xValue = (int)(
                MathF.Ceiling((float)(char.GetNumericValue(b)/ 2) - 1));
            yValue = (int)(char.ToUpper(a) - 65);

            // Returns the int array with the coordinates
            return new int[] {xValue, yValue};

        }

        /// <summary>
        /// Shows to the player how to play the game with a concise and 
        /// interactive tutorial.
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
                Console.WriteLine("|cannot move anymore, in any |");
                Console.WriteLine("|direction.                  |");
                Console.WriteLine("|...And in order for the wolf|");
                Console.WriteLine("|to win, all he needs is to  |");
                Console.WriteLine("|reach one of the starting   |");
                Console.WriteLine("|positions of the sheep,     |");
                Console.WriteLine("|which is any tile on the H  |");
                Console.WriteLine("|row.                        |");

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
                Console.WriteLine("|the move <tile>             |");
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
                Console.WriteLine("|inputted, or if you cannot  |");
                Console.WriteLine("|move to the selected tile,  |");
                Console.WriteLine("|the game will show you the  |");
                Console.WriteLine("|following error message:    |\n");
                Console.WriteLine(
                    "You cannot move to this tile, please input again!\n");
                Console.WriteLine("|If you're playing as the    |");
                Console.WriteLine("|sheep, you need to do the   |\n");
                Console.WriteLine("|choose <tile>               |\n");
                Console.WriteLine("|command ALWAYS FIRST in     |");
                Console.WriteLine("|order to choose correctly   |");
                Console.WriteLine("|the sheep that will move:   |\n");
                Console.WriteLine(
                    "choose C2\n");
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

            return;
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
