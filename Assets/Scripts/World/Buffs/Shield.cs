using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BuffSpell
{

    public override int manacost { get; set; } = 3;
    public override SpellName spellName { get; } = SpellName.Shield;


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
    public override ApplyBuffPacket GeneratePacket
        (ApplyBuffRequest req, StateManager from, StateManager opponent)
    {
        return null;
    }


    public override ApplyBuffResponse GenerateResponse
        (ApplyBuffRequest req, StateManager from, StateManager opponent)
    {
        return new ApplyShieldBuffResponse
        {
            ack = Ack.Success,
            coord = req.coord,
        };
    }
}


public class ShieldBuff : Buff
{

}