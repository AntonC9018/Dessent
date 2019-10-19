using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stable : Building
{
    public override BuildingName type
    {
        get { return BuildingName.Stable; }
    }
    public override GroundName allowedGroundType
    {
        get { return GroundName.Grassland; }
    }

    public static int capacityPerLevel = 1;

    public override int manaCapacity
    {
        get { return capacityPerLevel * level; }
    }

}