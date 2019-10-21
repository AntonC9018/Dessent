using System.Collections.Generic;

public class ApplyIlluminateBuffRequest : ApplyBuffRequest
{
    public ApplyIlluminateBuffRequest()
    {
        headerName = HeaderName.ApplyBuff;
        name = SpellName.Illuminate;
    }
}


public class ApplyIlluminateBuffResponse : ApplyBuffResponse
{
    public List<BonusStruct> bonuses;

    public ApplyIlluminateBuffResponse()
    {
        headerName = HeaderName.ApplyBuff;
        name = SpellName.Illuminate;
    }
}