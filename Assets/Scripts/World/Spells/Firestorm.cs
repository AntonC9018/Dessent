using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firestorm : Spell
{
    public new int manacost = 4;
    public int damage = 2;

    public Firestorm()
    {
        type = SpellName.Firestorm;
    }


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
        if (cell.building)
        {
            cell.building.TakeHit(new DamageSource(damage));
        }

        var resp = new ApplyFirestormSpellResponse
        {
            name = type,
            coord = cell.gridPos,
            ack = cell.building ? Ack.Success : Ack.Failure
        };

        var illuminate = sm.FindSpell(SpellName.Illuminate);
        var illuminates = new List<ApplyIlluminateSpellResponse>();

        // Illuminate target tile
        illuminates.Add((ApplyIlluminateSpellResponse)illuminate.ApplyEffect(cell, sm));

        // Illuminate adjacent tiles
        var cells = cell.GetAdjacent();
        foreach (Cell c in cells)
        {
            illuminates.Add((ApplyIlluminateSpellResponse)illuminate.ApplyEffect(c, sm));
        }

        resp.illuminates = illuminates;

        return resp;
    }


    public override void RealizeResponse(Response response, StateManager sm, bool animate)
    {
        var res = (ApplyFirestormSpellResponse)response;
        var illuminate = sm.FindSpell(SpellName.Illuminate);

        foreach (var illum in res.illuminates)
        {
            Cell cell = sm.privateGrid.GetCellAt(illum.coord);
            illuminate.RealizeResponse(illum, sm, false);
        }

        // TODO: start animation 

    }
}
