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
    public bool exists;
}


public abstract class Building : MonoBehaviour
{

    public abstract BuildingName type
    {
        get;
    }

    // Health 
    public int hp = 1;
    public int lastDamage = 0;

    // Religion
    // TODO: Implement religion logic. Right now it is completely ignored
    public int religion = 4;
    public bool owner = true;

    // Building
    public static int initialBuildCost = 1;
    public static int buildIncrement = 1;
    public abstract GroundName allowedGroundType
    {
        get;
    }

    public int GetBuildManaCost()
    {
        return initialBuildCost + buildIncrement * stateManager.buildingCount[type];
    }


    // Upgrading
    public static int initialUpgradeCost = 1;
    public static int upgradeIncrement = 1;
    public static int minLevel = 1;
    public static int maxLevel = 4;
    public int level = minLevel;

    public int GetUpgradeManaCost()
    {
        return upgradeIncrement * level + initialUpgradeCost;
    }

    public bool activeState = true;

    public Cell parentCell;
    public StateManager stateManager;


    // Mana + Spells amplification
    public virtual int manaCapacity
    {
        get;
    } = 0;
    public virtual int manaProduction
    {
        get;
    } = 0;
    public virtual int spellCapacity
    {
        get;
    } = 0;

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

    public virtual void Special() {}
    public virtual void OnTurnStart() {}
    public virtual void OnTurnEnd() {}

    public int ProduceMana()
    {
        var mana = manaProduction;
        // TODO: remove buffs
        return mana;
    }

    public virtual void Deactivate(int length)
    {
        // TODO: check for shield too
        activeState = false;
    }

}
