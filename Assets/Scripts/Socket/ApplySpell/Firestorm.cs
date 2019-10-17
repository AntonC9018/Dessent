using UnityEngine;
using System.Collections.Generic;

public class ApplyFirestormSpellResponse : ApplySpellResponse
{
    public List<ApplyIlluminateSpellResponse> illuminates;
    public BuildingStruct damagedBuilding;

    public ApplyFirestormSpellResponse()
    {
        headerName = HeaderName.ApplySpell;
        name = SpellName.Firestorm;
    }
}


public class ApplyFirestormSpellRequest : ApplySpellRequest
{
    public ApplyFirestormSpellRequest()
    {
        headerName = HeaderName.ApplySpell;
        name = SpellName.Firestorm;
    }
}