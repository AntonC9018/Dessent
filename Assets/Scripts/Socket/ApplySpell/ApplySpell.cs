
using UnityEngine;

public class ApplySpellRequest : Request
{
    public SpellName name;
    public Vector2Int coord;

    public ApplySpellRequest()
    {
        headerName = HeaderName.ApplySpell;
    }
}


public class ApplySpellResponse : Response
{
    public Ack ack;
    public SpellName name;
    public Vector2Int coord;

    public ApplySpellResponse()
    {
        headerName = HeaderName.ApplySpell;
    }
}


public class ApplySpellPacket : Packet
{
    public SpellName name;
    public Vector2Int coord;

    public ApplySpellPacket()
    {
        headerName = HeaderName.ApplySpell;
    }
}

