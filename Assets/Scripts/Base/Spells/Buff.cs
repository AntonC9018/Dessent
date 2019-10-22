using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffSpell : SpellBase
{
    public override SpellType spellType {
        get { return SpellType.BuffSpell;  }
    }


    public virtual void RealizePacket(Packet packet, StateManager sm) {}

    public abstract ApplyBuffResponse GenerateResponse
        (ApplyBuffRequest req, StateManager from, StateManager opponent);

    public virtual ApplyBuffPacket GeneratePacket
        (ApplyBuffRequest req, StateManager from, StateManager opponent)
    {
        var cell = opponent.privateGrid.GetCellAt(req.coord);
        // opponent has vision
        if (cell.building != null && cell.building.religion <= 2)
        {
            var illumTwinSpell = (TwinSpell)opponent.FindSpell(SpellName.Illuminate);
            var illumSpell     = illumTwinSpell.spell;
            var publicCell     = from.publicGrid.GetCellAt(req.coord);
            var res            = illumSpell.ApplyEffect(publicCell, from);
            var response       = (ApplyIlluminateSpellResponse)res;

            return new ApplyBuffVisionPacket
            {
                illuminate = response,
            };
        }
        return null;
    }
}


// this is the buff's effect
public class Buff
{
    public GameObject buffIcon;
}