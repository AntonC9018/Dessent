using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hut : Building
{
    public override BuildingName type
    {
        get { return BuildingName.Hut; }
    }
    public override GroundName allowedGroundType
    {
        get { return GroundName.Grassland; }
    }

    private static int manaPerLevel = 1;
    private static int baseManaProduction = 0;

    public override int manaProduction
    {
        get {
            int result = level * manaPerLevel + baseManaProduction;
            // TODO: increase if manaProduction amplification
            return result;
        }
    }


}