using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlluminateBuff : BuffSpell
{
    public override int manacost { get; set; } = 0;
    public override SpellName spellName { get; } = SpellName.Illuminate;


    // send a request to the opponent's or not grid
    public override void Request(Cell cell, StateManager sm)
    {
        var req = new ApplyIlluminateBuffRequest
        {
            coord = cell.gridPos
        };
        sm.Request(req);
    }

    public override void RealizeResponse(Response response, StateManager sm, bool animate)
    {
        var res = (ApplyIlluminateBuffResponse)response;
        Cell cell = sm.publicGrid.GetCellAt(res.coord);

        int diffCount = res.bonuses.Count - cell.bonuses.Count;
        
        // TODO: refactor, enhance
        foreach (var bonus in res.bonuses)
        {
            sm.instantiator.InstantiateBonusOnCell(cell);
        }

        if (diffCount > 0)
        {
            // TODO: discover bonuses
            Debug.Log("There are bonuses");
        }
    }

    public override ApplyBuffResponse GenerateResponse
        (ApplyBuffRequest req, StateManager from, StateManager opponent)
    {
        var cell = opponent.privateGrid.GetCellAt(req.coord);

        // generate a response with the necessary data
        var res = new ApplyIlluminateBuffResponse
        {
            // respond with the list of bonuses that the tile has
            bonuses = Bonus.ConvertToStructs(cell.bonuses),
            coord = req.coord,
        };

        return res;
    }

    public override ApplyBuffPacket GeneratePacket
        (ApplyBuffRequest req, StateManager from, StateManager opponent)
    {
        return null;
    }
}
