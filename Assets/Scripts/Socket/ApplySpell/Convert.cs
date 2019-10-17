
public class ApplyConvertSpellResponse : ApplySpellResponse
{
    public ApplyIlluminateSpellResponse illuminate;
    //// change to just religion level?
    //public BuildingStruct convertedBuilding;

    public ApplyConvertSpellResponse()
    {
        headerName = HeaderName.ApplySpell;
        name = SpellName.Convert;
    }
}


public class ApplyConvertSpellRequest : ApplySpellRequest
{
    public ApplyConvertSpellRequest()
    {
        headerName = HeaderName.ApplySpell;
        name = SpellName.Convert;
    }
}