using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firestorm : Spell
{

    public override int manacost { get; set; } = 4;
    public override SpellName spellName { get; } = SpellName.Firestorm;

    public int damage = 2;



    public override void Request(Cell cell, StateManager sm)
    {
        var req = new ApplyFirestormSpellRequest
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
        // damage the building
        if (cell.building != null)
        {
            cell.building.TakeHit(new DamageSource(damage));
        }

        var resp = new ApplyFirestormSpellResponse
        {
            name = spellName,
            coord = cell.gridPos,
            ack = cell.building != null ? Ack.Success : Ack.Failure
        };

        var illumSpell = sm.GetIlluminateSpell();
        var illuminates = new List<ApplyIlluminateSpellResponse>();

        // Illuminate target tile
        illuminates.Add((ApplyIlluminateSpellResponse)illumSpell.ApplyEffect(cell, sm));

        // Illuminate adjacent tiles
        var cells = cell.GetAdjacent();
        foreach (Cell c in cells)
        {
            illuminates.Add((ApplyIlluminateSpellResponse)illumSpell.ApplyEffect(c, sm));
        }

        resp.illuminates = illuminates;

        return resp;
    }


    public override void RealizeResponse(Response response, StateManager sm, bool animate)
    {
        var res = (ApplyFirestormSpellResponse)response;
        var illuminate = sm.GetIlluminateSpell();

        foreach (var illum in res.illuminates)
        {
            Cell cell = sm.privateGrid.GetCellAt(illum.coord);
            illuminate.RealizeResponse(illum, sm, false);
        }

        // TODO: start animation 

    }
}
