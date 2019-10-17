using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlluminateBuff : BuffSpell
{
    public new int manacost = 0;

    public IlluminateBuff()
    {
        type = BuffSpellName.Illuminate;
    }

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
        cell.bonuses = res.bonuses;

        if (diffCount > 0)
        {
            // TODO: discover bonuses
        }
    } 

}
