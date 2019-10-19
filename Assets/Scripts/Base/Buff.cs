using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffSpell
{
    public int manacost;
    public BuffSpellName type;
    public virtual int requiredNumberPhases
    {
        get;
    } = 1;

    public abstract void Request(Cell cell, StateManager sm);

    public abstract void RealizeResponse(Response response, StateManager sm, bool animate);


    public virtual void RealizePacket(Packet packet, StateManager sm) {}
}


// this is the buff's effect
public abstract class Buff
{

}