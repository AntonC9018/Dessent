using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purge : BuffSpell
{
    public override int manacost { get; set; } = 2;
    public override SpellName spellName { get; } = SpellName.Purge;


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
        var res = (ApplyPurgeBuffResponse)response;
        Cell cell = sm.publicGrid.GetCellAt(res.coord);
        cell.building.religion = res.religionLevel;
    }

    // this will be called only if a player loses control of a cell
    // after the other one uses the buff on it
    // or if the religion level is so low the opponent has vision
    public override void RealizePacket(Packet packet, StateManager sm)
    {
        var pack = (ApplyPurgeBuffPacket)packet;
        Cell cell = sm.privateGrid.GetCellAt(pack.coord);
        // make sure the building is there
        if (cell.building)
        {
            cell.building.religion = pack.religionLevel;
        }
        if (pack.lostControl)
        {
            cell.Convert();
        }
    }
}


public class PurgeBuff : Buff
{

}