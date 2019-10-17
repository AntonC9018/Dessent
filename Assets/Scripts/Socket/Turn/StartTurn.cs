//public class StartTurnRequest : Request
//{
//    public StartTurnRequest()
//    {
//        headerName = HeaderName.StartTurn;
//    }
//}

//public class StartTurnResponse : Response
//{
//    int turnCount;

//    public StartTurnResponse()
//    {
//        headerName = HeaderName.StartTurn;
//    }
//}

public class StartTurnPacket : Packet
{

    public int turnCount;

    public StartTurnPacket()
    {
        headerName = HeaderName.StartTurn;
    }
}