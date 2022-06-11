namespace GameObjects;

/// <summary>
/// The <c>Player</c> class holds information for the player's name and their symbol.
/// </summary>
public class Player
{
    public char PlayerSymbol { get; set; }
    public string PlayerName { get; set; }

    public Player(char playerSymbol, string playerName)
    {
        this.PlayerSymbol = playerSymbol;
        this.PlayerName = playerName;
    }

}