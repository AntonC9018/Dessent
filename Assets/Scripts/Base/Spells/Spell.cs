using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : SpellBase
{
    public override SpellType spellType
    {
        get { return SpellType.Spell; }
    }

    // Apply the effect of the spell and send a response to gm
    public virtual void Apply(Cell cell, StateManager sm)
    {
        sm.Respond(ApplyEffect(cell, sm));
    }

    // Apply the effects of the spell on a given cell, without 
    // reporting the response to gm
    public abstract Response ApplyEffect(Cell cell, StateManager sm);

}

