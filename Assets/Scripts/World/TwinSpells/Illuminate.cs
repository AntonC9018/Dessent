public abstract class TwinSpell
{
    public int manacost;
    // do both requests : a spell request and a buff request
    public abstract void Request(Cell cell, StateManager sm);
}

public class IlluminateTwinSpell : TwinSpell
{
    public new int manacost = 0;
    public override void Request(Cell cell, StateManager sm)
    {

        // TODO: add more complex logic to when both cells
        // are actually both public or both private
        Cell publicCell;
        Cell privateCell;
        if (cell.parentGrid.IsPublic())
        {
            publicCell = cell;
            privateCell = sm.privateGrid.GetCellAt(cell.gridPos);
        }
        else
        {
            privateCell = cell;
            publicCell = sm.publicGrid.GetCellAt(cell.gridPos);
        }
        var ispell = sm.FindSpell(SpellName.Illuminate);
        ispell.Request(privateCell, sm);
        var ibuff = sm.FindBuff(BuffSpellName.Illuminate);
        ibuff.Request(publicCell, sm);
    }
}