using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffSpell : SpellBase
{
    public override SpellType spellType {
        get { return SpellType.BuffSpell;  }
    }


    public virtual void RealizePacket(Packet packet, StateManager sm) {}
}


// this is the buff's effect
public class Buff
{
    public GameObject buffIcon;
}