using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monastery : Building
{
    // TODO: Add animation / image to this object
    public static int maxSpellsPerLevel = 1;

    void Start()
    {
        type = BuildingName.Monastery;
    }

    public override int GetMaxSpells()
    {
        return maxSpellsPerLevel * level;
    }


}