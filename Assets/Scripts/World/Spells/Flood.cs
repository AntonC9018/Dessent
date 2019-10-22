using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : Spell
{

    public override int manacost { get; set; } = 2;
    public override SpellName spellName { get; } = SpellName.Flood;

    public int deactivationLength = 1;


    public override void Request(Cell cell, StateManager sm)
    {
        var req = new ApplyFloodSpellRequest
        {
            coord = cell.gridPos,
        };
        sm.Request(req);
    }


    public override void Apply(Cell cell, StateManager sm)
    {
        base.Apply(cell, sm);
    }

    public override Response ApplyEffect(Cell cell, StateManager sm)
    {
        if (cell.building != null)
        {
            // deactivate the building at location
            cell.building.Deactivate(deactivationLength);
        }

        // illuminate the cell
        var illumSpell = sm.GetIlluminateSpell();

        var resp = new ApplyFloodSpellResponse
        {
            coord = cell.gridPos,
            ack = cell.building ? Ack.Success : Ack.Failure,
            illuminate = (ApplyIlluminateSpellResponse)illumSpell.ApplyEffect(cell, sm),
        };

        return resp;
    }

    // TODO: implement
    public override void RealizeResponse(Response response, StateManager sm, bool animate)
    {

        if (!animate) return;
    }
}
