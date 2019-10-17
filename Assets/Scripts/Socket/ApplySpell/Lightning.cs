using UnityEngine;
using System.Collections.Generic;

public class ApplyLightningSpellResponse : ApplySpellResponse
{
    public ApplyIlluminateSpellResponse illuminate;

    public ApplyLightningSpellResponse()
    {
        headerName = HeaderName.ApplySpell;
        name = SpellName.Lightning;
    }
}


public class ApplyLightningSpellRequest : ApplySpellRequest
{
    public ApplyLightningSpellRequest()
    {
        headerName = HeaderName.ApplySpell;
        name = SpellName.Lightning;
    }
}