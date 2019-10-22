using System.Collections.Generic;

public class ApplyZealotsBuffRequest : ApplyBuffRequest
{
    public ApplyZealotsBuffRequest()
    {
        headerName = HeaderName.ApplyBuff;
        name = SpellName.Zealots;
    }
}


public class ApplyZealotsBuffResponse : ApplyBuffResponse
{
    public int religionLevel;
    public ApplyZealotsBuffResponse()
    {
        headerName = HeaderName.ApplyBuff;
        name = SpellName.Zealots;
    }
}

public class ApplyZealotsBuffPacket : ApplyBuffPacket
{
    public ApplyIlluminateSpellResponse illuminate;

    public ApplyZealotsBuffPacket()
    {
        headerName = HeaderName.ApplyBuff;
        name = SpellName.Zealots;
    }
}