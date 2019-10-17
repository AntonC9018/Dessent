
using UnityEngine;

public class ApplyBuffRequest : Request
{
    public BuffSpellName name;
    public Vector2Int coord;

    public ApplyBuffRequest()
    {
        headerName = HeaderName.ApplyBuff;
    }
}


public class ApplyBuffResponse : Response
{
    public Ack ack;
    public BuffSpellName name;
    public Vector2Int coord;

    public ApplyBuffResponse()
    {
        headerName = HeaderName.ApplyBuff;
    }
}



public class ApplyBuffPacket : Packet
{
    public BuffSpellName name;
    public Vector2Int coord;

    public ApplyBuffPacket()
    {
        headerName = HeaderName.ApplyBuff;
    }
}