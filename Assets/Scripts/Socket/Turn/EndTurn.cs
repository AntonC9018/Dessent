
// Used by a player to end the turn manually
public class EndTurnRequest : Request
{
    public EndTurnRequest()
    {
        headerName = HeaderName.EndTurn;
    }
}

// Responding to the request
public class EndTurnResponse : Response
{
    public int turnCount;

    public EndTurnResponse()
    {
        headerName = HeaderName.EndTurn;
    }
}

// End the turn programmatically, i.e. timer ran out
public class EndTurnPacket : Packet
{

    public int turnCount;

    public EndTurnPacket()
    {
        headerName = HeaderName.EndTurn;
    }
}