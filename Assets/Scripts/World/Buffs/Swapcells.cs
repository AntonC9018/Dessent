using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapcells : BuffSpell
{

    public override int manacost { get; set; } = 1;
    public override SpellName spellName { get; } = SpellName.Swapcells;

    public override int requiredNumberPhases {
        get;
    } = 1;


    public override void Request(Cell cell, StateManager sm)
    {
        var coord1 = cell.gridPos;
        var coord2 = sm.selectedAction.cells[0].gridPos;
        if (coord1 == coord2)
        {
            Debug.Log("Use must choose two distinct cells");
            return;
        }

        var req = new ApplySwapcellsBuffRequest
        {
            coordTo = coord1,
            coord = coord2,
        };

        sm.Request(req);
    }

    public override void RealizeResponse(Response response, StateManager sm, bool animate)
    {
        var res = (ApplySwapcellsBuffResponse)response;
        Cell cell1 = sm.publicGrid.GetCellAt(res.coord);
        Cell cell2 = sm.publicGrid.GetCellAt(res.coordTo);
        Cell.ExchangeCells(cell1, cell2);
    }

    // if someone called this, it means that
    // swapcells was called while one of the players
    // had vision on one or more of the swapped cells
    public override void RealizePacket(Packet packet, StateManager sm)
    {
        var pack = (ApplySwapcellsBuffPacket)packet;
        Cell cell1 = sm.privateGrid.GetCellAt(pack.coord);
        Cell cell2 = sm.privateGrid.GetCellAt(pack.coordTo);
        Cell.ExchangeCells(cell1, cell2);
    }


    public override ApplyBuffResponse GenerateResponse
        (ApplyBuffRequest req, StateManager from, StateManager opponent)
    {
        var r = (ApplySwapcellsBuffRequest)req;

        return new ApplySwapcellsBuffResponse
        {
            coord = r.coord,
            coordTo = r.coordTo,
        };
    }


    public override ApplyBuffPacket GeneratePacket(ApplyBuffRequest req, StateManager from, StateManager opponent)
    {
        var request = (ApplySwapcellsBuffRequest)req;

        return new ApplySwapcellsBuffPacket
        {
            coord = request.coord,
            coordTo = request.coordTo,
        };
    }


}