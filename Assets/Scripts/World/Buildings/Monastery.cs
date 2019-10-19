using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monastery : Building
{
    public override BuildingName type
    {
        get { return BuildingName.Monastery; }
    }
    public override GroundName allowedGroundType
    {
        get { return GroundName.Mountain; }
    }

    private static int maxSpellsPerLevel = 1;

    public override int spellCapacity
    {
        get { return maxSpellsPerLevel * level; }
    }


}