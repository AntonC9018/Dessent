using UnityEngine;
using System.Collections.Generic;

public class ApplyShieldBuffResponse : ApplyBuffResponse
{
    public ApplyShieldBuffResponse()
    {
        headerName = HeaderName.ApplyBuff;
        name = SpellName.Shield;

    }
}


public class ApplyShieldBuffRequest : ApplyBuffRequest
{
    public ApplyShieldBuffRequest()
    {
        headerName = HeaderName.ApplyBuff;
        name = SpellName.Shield;
    }
}