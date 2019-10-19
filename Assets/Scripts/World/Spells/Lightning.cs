using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : Spell
{
    public override int manacost { get; set; } = 4;
    public override SpellName spellName { get; } = SpellName.Lightning;

    public int damage = 1;    


    public override void Request(Cell cell, StateManager sm)
    {
        var req = new ApplyLightningSpellRequest
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
            cell.building.TakeHit(new DamageSource(damage));
        }
        var illuminate = (Spell)sm.FindSpell(SpellName.Illuminate);
        var resp = new ApplyLightningSpellResponse
        {
            name = spellName,
            coord = cell.gridPos,
            ack = cell.building ? Ack.Success : Ack.Failure,
            illuminate = (ApplyIlluminateSpellResponse)illuminate.ApplyEffect(cell, sm),
        };
        return resp;
    }

    public override void RealizeResponse(Response response, StateManager sm, bool animate)
    {
        var res = (ApplyLightningSpellResponse)response;
        // illuminate a cell, do not animate
        var illuminate = sm.FindSpell(SpellName.Illuminate);
        illuminate.RealizeResponse(res.illuminate, sm, false);

        // TODO: play animation
        if (!animate) return;
    }
}