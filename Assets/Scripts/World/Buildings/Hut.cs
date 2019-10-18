using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hut : Building
{
    // TODO: Add animation / image to this object

    public static int manaPerLevel;

    void Start()
    {
        type = BuildingName.Hut;
        allowedGroundType = GroundName.Grassland;
    }

    public override int GetMana()
    {
        return manaPerLevel * level;
    }

    public override int GetMPS()
    {
        return manaPerLevel * level;
    }

}