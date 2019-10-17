using UnityEngine;
using System.Collections.Generic;

public class ApplyShieldBuffResponse : ApplyBuffResponse
{
    public ApplyShieldBuffResponse()
    {
        headerName = HeaderName.ApplyBuff;
        name = BuffSpellName.Shield;

    }
}


public class ApplyShieldBuffRequest : ApplyBuffRequest
{
    public ApplyShieldBuffRequest()
    {
        headerName = HeaderName.ApplyBuff;
        name = BuffSpellName.Shield;
    }
}