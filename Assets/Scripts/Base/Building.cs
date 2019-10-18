using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public struct BuildingStruct
{
    public int hp;
    public BuildingName type;
    public int religion;

    // TODO: Reconsider in the future
    public bool activeState;

    public int level;
}


public abstract class Building : MonoBehaviour
{

    public BuildingName type;
    public int hp = 1;
    public int lastDamage = 0;
    public int religion = 4;
    public bool owner = true;

    public static int buyCost = 1;
    public static int upgradeCost = 1;

    // upgradeIncrement * level + upgradeCost
    public static int upgradeIncrement = 1; 

    public bool activeState = true;

    public static int minLevel = 1;
    public int level = minLevel;
    public static int maxLevel = 4;

    public GroundName allowedGroundType;

    public Cell parentCell;

    public bool GetBuildManaCost()
    {
        return 0;
    }

    public bool CanUpgrade()
    {
        if (level < maxLevel)
        {
            return true;
        }
        return false;
    }

    public virtual void Upgrade()
    {
        level += 1;
    }


    public virtual void TakeDamage(int damage) {
        hp -= damage;
        lastDamage = damage;
    }

    public virtual void TakeHit(DamageSource source) {
        int damage = source.damage;
        TakeDamage(damage);


        foreach(Effect eff in source.effects)
            {
                eff.Apply(this, source);
            }
        
        if (hp <= 0) {
            // TODO: destroy the building
        }
    }

    public virtual void InfluenceReligion(ReligionSource source) {}


    // the inherited class that is going to produce mana
    // should override these functions to a one that sould take 
    // in accound who is the owner of the building
    public virtual int GetMPS() 
    {
        return 0;
    }
    public virtual int GetMana()
    {
        return 0;
    }

    // this is for the buildings that produce mana
    public virtual int GetManaCapacity() 
    {
        return 0;
    }

    public virtual int GetMaxSpells()
    {
        return 0;
    }

    public virtual void OnTurnPhaseChange(TurnPhase phase)
    {
        
    }

    public virtual void Deactivate(int length)
    {
        // TODO: check for shield too
        activeState = false;
    }

    public int GetUpgradeManaCost()
    {
        //var grid = parentCell.parentGrid;
        //var count = grid.GetBuildingCountOfType(type);
        return upgradeIncrement * level + upgradeCost;
    }

    public static bool IsAllowedBuildingOnCell(BuildingName type, Cell cell)
    {
        if (type == BuildingName.Hut || type == BuildingName.Stable)
        {
            if (cell.ground.altitude == GroundName.Grassland) return true;
        }
        else if (type == BuildingName.Beacon)
        {
            if (cell.ground.altitude == GroundName.Sea) return true;
        }
        else if (type == BuildingName.Monastery)
        {
            if (cell.ground.altitude == GroundName.Mountain) return true;
        }
        return false;
    }
}
