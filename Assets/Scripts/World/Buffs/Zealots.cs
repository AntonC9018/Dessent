using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zealots : BuffSpell
{

    public override int manacost { get; set; } = 2;
    public override SpellName spellName { get; } = SpellName.Zealots;


    public override void Request(Cell cell, StateManager sm)
    {
        var req = new ApplyZealotsBuffRequest
        {
            coord = cell.gridPos,
        };
        sm.Request(req);
    }

    public override void RealizeResponse(Response response, StateManager sm, bool animate)
    {
        var res = (ApplyZealotsBuffResponse)response;
        Cell cell = sm.publicGrid.GetCellAt(res.coord);
        cell.AddBuff(new ZealotsBuff());
        // TODO: start some animation
    }


    // this may be called when the ooponent applies this
    // while having 2 or less level of religion on the cell
    public override void RealizePacket(Packet packet, StateManager sm)
    {
        var pack = (ApplyZealotsBuffPacket)packet;
        var illuminate = sm.GetIlluminateSpell();
        illuminate.RealizeResponse(pack.illuminate, sm, false);
        // TODO: start some animation
    }


    public override ApplyBuffResponse GenerateResponse
        (ApplyBuffRequest req, StateManager from, StateManager opponent)
    {
        var cell = from.publicGrid.GetCellAt(req.coord);
        int religionLevel = cell.building.religion - 1;
        Ack ack;

        // opponent gained control
        if (religionLevel == 0)
        {
            // By this time the packet has been sent, so ignore
            ack = Ack.Failure;
        }
        // opponent gets vision
        else if (religionLevel == 1)
        {
            ack = Ack.Warning;
        }
        else
        {
            ack = Ack.Success;
        }

        return new ApplyZealotsBuffResponse
        {
            coord = req.coord,
            religionLevel = religionLevel,
            ack = ack,
        };
    }
}


public class ZealotsBuff : Buff
{

}