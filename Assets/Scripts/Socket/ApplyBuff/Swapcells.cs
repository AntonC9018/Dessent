using System.Collections.Generic;
using UnityEngine;

public class ApplySwapcellsBuffRequest : ApplyBuffRequest
{
    public Vector2 coordTo;
    public ApplySwapcellsBuffRequest()
    {
        headerName = HeaderName.ApplyBuff;
        name = SpellName.Swapcells;
    }
}


public class ApplySwapcellsBuffResponse : ApplyBuffResponse
{
    public Vector2Int coordTo;
    public ApplySwapcellsBuffResponse()
    {
        headerName = HeaderName.ApplyBuff;
        name = SpellName.Swapcells;
    }
}


// if someone called this, it means that
// swapcells was called while one of the players
// had vision on one or more of the swapped cells
public class ApplySwapcellsBuffPacket : ApplyBuffPacket
{
    public Vector2Int coordTo;

    public ApplySwapcellsBuffPacket()
    {
        headerName = HeaderName.ApplyBuff;
        name = SpellName.Swapcells;
    }
}