using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convert : Spell
{
    public new int manacost = 3;

    public Convert()
    {
        type = SpellName.Convert;
    }


    public override void Request(Cell cell, StateManager sm)
    {
        var req = new ApplyConvertSpellRequest
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

        if (cell.building)
        {
            cell.building.InfluenceReligion(
                new ReligionSource
                {
                    //...
                });

            // TODO: Add some more complex logic for checking
            // whether the cell changed its grid
            // and respond correspondingly
        }

        var res = new ApplyConvertSpellResponse
        {
            coord = cell.gridPos,
            ack = cell.building ? Ack.Success : Ack.Failure,
        };

        if (cell.building)
        {
            var illuminate = sm.FindSpell(SpellName.Illuminate);
            var illum = illuminate.ApplyEffect(cell, sm);
            res.illuminate = (ApplyIlluminateSpellResponse)illum;
        }

        return res;
    }

    public override void RealizeResponse(Response response, StateManager sm, bool animate)
    {
        var res = (ApplyConvertSpellResponse)response;
        if (res.illuminate != null)
        {
            var illuminate = sm.FindSpell(SpellName.Illuminate);
            illuminate.RealizeResponse(res.illuminate, sm, false);

            // TODO: play some success animation
        }
        else
        {
            // TODO: play some fail animation
        }
    }
}