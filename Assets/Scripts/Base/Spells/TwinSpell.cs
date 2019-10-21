using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TwinSpell : SpellBase
{

    public abstract Spell spell { get; }
    public abstract BuffSpell buffSpell { get; }
    public override SpellType spellType
    {
        get { return SpellType.TwinSpell; }
    }

    public override void RealizeResponse
        (Response response, StateManager sm, bool animate)
    {}
}

