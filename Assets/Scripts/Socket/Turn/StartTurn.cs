public class StartTurnPacket : Packet
{

    public int turnCount;

    public StartTurnPacket()
    {
        headerName = HeaderName.StartTurn;
    }
}