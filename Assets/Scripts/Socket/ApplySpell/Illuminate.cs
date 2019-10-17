using UnityEngine;
using System.Collections.Generic;


public class ApplyIlluminateSpellRequest : ApplySpellRequest
{
    public ApplyIlluminateSpellRequest()
    {
        headerName = HeaderName.ApplySpell;
        name = SpellName.Illuminate;
    }
}


public class ApplyIlluminateSpellResponse : ApplySpellResponse
{
    public GroundName altitude;
    public BuildingStruct building;
    public List<Buff> buffs;

    public ApplyIlluminateSpellResponse()
    {
        headerName = HeaderName.ApplySpell;
        name = SpellName.Illuminate;
    }
}