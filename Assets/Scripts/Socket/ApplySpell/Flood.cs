using UnityEngine;
using System.Collections.Generic;

public class ApplyFloodSpellResponse : ApplySpellResponse
{
    public ApplyIlluminateSpellResponse illuminate;

    public ApplyFloodSpellResponse()
    {
        headerName = HeaderName.ApplySpell;
        name = SpellName.Flood;
    }
}


public class ApplyFloodSpellRequest : ApplySpellRequest
{
    public ApplyFloodSpellRequest()
    {
        headerName = HeaderName.ApplySpell;
        name = SpellName.Flood;
    }
}