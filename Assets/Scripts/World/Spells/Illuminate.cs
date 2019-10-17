using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// not to be confused with IlluminateBuff!!
public class IlluminateSpell : Spell
{
    public new int manacost = 0;

    public IlluminateSpell()
    {
        type = SpellName.Illuminate;
    }

    // send a request to the opponent's or not grid
    public override void Request(Cell cell, StateManager sm)
    {
        var req = new ApplyIlluminateSpellRequest
        {
            coord = cell.gridPos
        };
        sm.Request(req);
    }

    public override void Apply(Cell cell, StateManager sm)
    {
        base.Apply(cell, sm);
    }

    public override Response ApplyEffect(Cell cell, StateManager sm)
    {
        BuildingStruct building = new BuildingStruct();

        // include info about the illuminated cell
        if (cell.building != null)
        {
            building.type = cell.building.type;
            building.religion = cell.building.religion;
            building.activeState = cell.building.activeState;
            building.level = cell.building.level;
        }

        var res = new ApplyIlluminateSpellResponse
        {
            coord = cell.gridPos,
            building = building,
            buffs = cell.buffs,
            altitude = cell.ground.altitude,
        };

        return res;
    }

    public override void RealizeResponse(Response response, StateManager sm, bool animate) {


        var res = (ApplyIlluminateSpellResponse)response;
        Cell cell = sm.privateGrid.GetCellAt(res.coord);
        if (cell.building)
        {
            // TODO: Destroy the building as gameObject
            // cell.building.gameObject.Destroy();
        }
        cell.CreateAndSetBuilding(res.building);

        if (!animate) return;

        var parent = cell.gameObject;

        // istantiate prefab bla bla
        // this is temporary btw
        var illuminate = (GameObject)Resources.Load("Prefab/Spells_anim/Illuminate");

        var animationObject = GameObject.Instantiate(
            illuminate,
            parent.transform.position,
            Quaternion.identity,
            parent.transform.parent);
    }


    //public void RealizePacket(Packet packet, StateManager sm, bool animate)
    //{

    //}
}
