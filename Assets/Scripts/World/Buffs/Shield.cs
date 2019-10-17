using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BuffSpell
{
    public new int manacost = 3;

    public Shield()
    {
        type = BuffSpellName.Shield;
    }


    public override void Request(Cell cell, StateManager sm)
    {
        var req = new ApplyShieldBuffRequest
        {
            coord = cell.gridPos,
        };
        sm.Request(req);
    }

    public override void RealizeResponse(Response response, StateManager sm, bool animate)
    {
        var res = (ApplyShieldBuffResponse)response;
        Cell cell = sm.publicGrid.GetCellAt(res.coord);
        cell.AddBuff(new ShieldBuff());
        // TODO: play some animation
    }

    // There is no packet, because this buff is hidden
}


public class ShieldBuff : Buff
{

}