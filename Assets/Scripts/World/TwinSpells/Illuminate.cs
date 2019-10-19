
public class IlluminateTwinSpell : TwinSpell
{
    public override int manacost { get; set; } = 0;
    public override SpellName spellName { get; } = SpellName.Illuminate;
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
        var ibuff = sm.FindSpell(SpellName.Illuminate);
        ibuff.Request(publicCell, sm);
    }
}