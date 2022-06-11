namespace GameObjects;

/// <summary>
/// The <c>Game</c> class holds all the logic to start the game and rerun if an individual wants to play again.
/// </summary>
public class Game
{
    private int BoardSize { get; set; } = 9;
    private bool Winner { get; set; } = false;
    private Player[] Players { get; set; } = new Player[2];
    private int CurrentPlayer { get; set; } = 0;
    private char EmptyLocation { get; set; } = '-';
    private char[] Board { get; set; } = new char[9];

    /// <summary>
    /// Initializes the game, no arguments are required. Creating the <c>Game</c> class does not run the game. 
    /// </summary>
    public Game()
    {
        for (int i = 0; i < 9; i++)
        {
            Board[i] = EmptyLocation;
        }

        Players[0] = new Player('X', "Player 1");
        Players[1] = new Player('O', "Player 2");

    }

    /// <summary>
    /// The function will display the game board to the console.
    /// </summary>
    private void PrintBoard()   
    {
        int gameBoardIndex = 0;
        
        for (var i = 0; i < 3; i++)
        {
            char[] row = new char[5];
            var columnIndex = 0;
            var boardSymbolToggle = 1;

            var j = 0;
            while (j < 3)
            {
                if (boardSymbolToggle == 1)
                {
                    row[columnIndex] = Board[gameBoardIndex];
                    j++;
                    columnIndex++;
                    gameBoardIndex++;
                    boardSymbolToggle = 0;
                }
                else
                {
                    row[columnIndex] = '|';
                    columnIndex++;
                    boardSymbolToggle = 1;
                }
            }
            Console.WriteLine(row);
        }
    }

    /// <summary>
    /// Validates that the choice used is within the Game Board Parameters 
    /// </summary>
    /// <param name="choice">Must be a valid int between 1 and 9</param>
    /// <returns>If the number is valid will return true, else false.</returns>
    private bool CheckInputRange(int choice)
    {
        return choice is > 0 and <= 9;
    }
    
    /// <summary>
    /// Gets the player input converts it to a string and returns an int.
    /// </summary>
    /// <returns>Returns a valid int for the game to process on the game board.</returns>
    private int GetPlayerInput()
    {
        int numberInput = new int();
        bool isCorrect = false;

        while (!isCorrect)
        {
            Console.WriteLine(Players[CurrentPlayer].PlayerName + " enter your choice: ");
            var userInput = Console.ReadLine()!;

            try
            {
                numberInput = int.Parse(userInput);
            }
            catch
            {
                Console.WriteLine("You did not enter a number please try again later");
                continue;
            }

            isCorrect = CheckInputRange(numberInput);
            if (!isCorrect)
            {
                Console.WriteLine("You did not enter a correct number try again!");
            }
        }

        return numberInput;
    }

    /// <summary>
    /// Toggles the current player index in the current class.
    /// </summary>
    private void ToggleCurrentPlayer()
    {
        if (CurrentPlayer == 1)
        {
            CurrentPlayer = 0;
            return;
        }

        CurrentPlayer = 1;
    }

    /// <summary>
    /// Intakes a value and confirms the spot on the board is empty and places it.
    /// </summary>
    /// <param name="choice">Validated integer and the position on the board. Function takes care of subtracting one for index.</param>
    private void PlacePlayerChoice(int choice)
    {
        if (Board[choice - 1] == EmptyLocation)
        {
            Board[choice - 1] = Players[CurrentPlayer].PlayerSymbol;
            ToggleCurrentPlayer();
        }
        else
        {
            Console.WriteLine("Location is taken try again!");
        }
    }

    /// <summary>
    /// Validates if a <c>Player</c> has won by checking their piece against a row of three.
    /// </summary>
    /// <param name="a">First location</param>
    /// <param name="b">Second location</param>
    /// <param name="c">Third location</param>
    /// <param name="playerSymbol">Which player that is being checked.</param>
    /// <returns></returns>
    private bool CheckRow(char a, char b, char c, char playerSymbol)
    {
        return a == playerSymbol && b == playerSymbol && c == playerSymbol;
    }

    /// <summary>
    /// Uses CheckRow to see if a Player has won.
    /// </summary>
    /// <param name="playerSymbol">The player's symbol to check if they won.</param>
    /// <returns>Returns turn if a Player won, else false.</returns>
    /// <seealso cref="CheckRow"/>
    private bool CheckResults(char playerSymbol)
    {
        char topLeft = Board[0];
        char topMid = Board[1];
        char topRight = Board[2];
        char midLeft = Board[3];
        char midMid = Board[4];
        char midRight = Board[5];
        char botLeft = Board[6];
        char botMid = Board[7];
        char botRight = Board[8];
        
        bool[] results = {
            CheckRow(topLeft, topMid, topRight, playerSymbol),
            CheckRow(midLeft, midMid, midRight, playerSymbol),
            CheckRow(botLeft, botMid, botRight, playerSymbol),
            CheckRow(topLeft, midLeft, botLeft, playerSymbol),
            CheckRow(topMid, midMid, botMid, playerSymbol),
            CheckRow(topRight, midRight, botRight, playerSymbol),
            CheckRow(topLeft, midMid, botRight, playerSymbol),
            CheckRow(botLeft, midMid, topRight, playerSymbol)
        };

        return results.Any(t => t);
    }

    /// <summary>
    /// Will check if a player wins, if they have will return true.
    /// </summary>
    /// <returns>Returns true on either player winning, else false.</returns>
    /// <seealso cref="CheckResults"/>
    private bool CheckForWinner()
    {
        if (CheckResults(Players[0].PlayerSymbol))
        {
            Console.WriteLine(Players[0].PlayerName + " wins!");
            return true;
        } else if (CheckResults(Players[1].PlayerSymbol))
        {
            Console.WriteLine(Players[1].PlayerName + " wins!");
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if all places are taken.
    /// </summary>
    /// <returns>Returns false if location is open, else true.</returns>
    private bool CheckForTie()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            if (Board[i] == EmptyLocation)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Checks if the user wants to play the game again.
    /// </summary>
    /// <returns>Returns true if they answer 'y' or 'Y', else false</returns>
    private bool PlayAgain()
    {
        Console.WriteLine("Play again? ");
        var playAgain = Console.ReadLine()!;
        if (playAgain is "y" or "Y")
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Runs the game loop
    /// </summary>
    /// <param name="game">Provides a reference, if the user wants to play again, the game object will be recreated.</param>
    public void GameLoop(ref Game game)
    {
        PrintBoard();
        var gameComplete = false;

        while (!gameComplete)
        {
            var choice = GetPlayerInput();
            PlacePlayerChoice(choice);
            PrintBoard();
            if (CheckForWinner() || CheckForTie())
            {
                gameComplete = true;
            }
        }

        if (PlayAgain())
        {
            game = new Game();
            game.GameLoop(ref game);
        }   
    }
    
}