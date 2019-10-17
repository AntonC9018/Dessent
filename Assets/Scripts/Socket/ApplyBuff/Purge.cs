using UnityEngine;
using System.Collections.Generic;

public class ApplyPurgeBuffResponse : ApplyBuffResponse
{
    public int religionLevel;

    public ApplyPurgeBuffResponse()
    {
        headerName = HeaderName.ApplyBuff;
        name = BuffSpellName.Purge;
    }
}


public class ApplyPurgeBuffRequest : ApplyBuffRequest
{
    public ApplyPurgeBuffRequest()
    {
        headerName = HeaderName.ApplyBuff;
        name = BuffSpellName.Purge;
    }
}


public class ApplyPurgeBuffPacket : ApplyBuffPacket
{

    public bool lostControl;
    public int religionLevel;

    public ApplyPurgeBuffPacket()
    {
        headerName = HeaderName.ApplyBuff;
        name = BuffSpellName.Purge;

    }
}