public enum TurnPhase
{
    Start,      // Translation Opponent -> You
    You,        // The actual turn (one can use spells etc)
    End,        // Translation You -> Opponent
    Opponent,   // The opponent's You
}

public enum GamePhase
{
    Terraforming,
    Building,
    Apocalypse,
}


public enum CellEventName
{
    ButtonPressed,
    ButtonReleased,
    DraggedIn,
    DraggedOut,
    Enter,
    Exit,
    Cancel
}