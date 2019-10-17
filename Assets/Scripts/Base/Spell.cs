using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell
{
    public int manacost;
    public SpellName type;

    // Apply the effect of the spell and send a response to gm
    public virtual void Apply(Cell cell, StateManager sm)
    {
        sm.Respond(ApplyEffect(cell, sm));
    }

    // Apply the effects of the spell on a given cell, without 
    // reporting the response to gm
    public abstract Response ApplyEffect(Cell cell, StateManager sm);

    // Construct and make a request to gm 
    // This function is put in ApplyActionStruct inside gm
    // as a callback to clicking a cell with this spell selected
    public abstract void Request(Cell cell, StateManager sm);

    // Receive a response after the request and 
    // do something based on the response
    public abstract void RealizeResponse(Response response, StateManager sm, bool animate);

}

