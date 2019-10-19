using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TwinSpell : SpellBase
{
    public override SpellType spellType
    {
        get { return SpellType.TwinSpell; }
    }

    public override void RealizeResponse
        (Response response, StateManager sm, bool animate)
    {}
}

