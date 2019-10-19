
using UnityEngine;

public class ApplyBuffRequest : Request
{
    public SpellName name;
    public Vector2Int coord;

    public ApplyBuffRequest()
    {
        headerName = HeaderName.ApplyBuff;
    }
}


public class ApplyBuffResponse : Response
{
    public Ack ack;
    public SpellName name;
    public Vector2Int coord;

    public ApplyBuffResponse()
    {
        headerName = HeaderName.ApplyBuff;
    }
}



public class ApplyBuffPacket : Packet
{
    public SpellName name;
    public Vector2Int coord;

    public ApplyBuffPacket()
    {
        headerName = HeaderName.ApplyBuff;
    }
}