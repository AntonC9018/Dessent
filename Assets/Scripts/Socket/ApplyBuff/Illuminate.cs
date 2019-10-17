using System.Collections.Generic;

public class ApplyIlluminateBuffRequest : ApplyBuffRequest
{
    public ApplyIlluminateBuffRequest()
    {
        headerName = HeaderName.ApplyBuff;
        name = BuffSpellName.Illuminate;
    }
}


public class ApplyIlluminateBuffResponse : ApplyBuffResponse
{
    public List<Bonus> bonuses;

    public ApplyIlluminateBuffResponse()
    {
        headerName = HeaderName.ApplyBuff;
        name = BuffSpellName.Illuminate;
    }
}