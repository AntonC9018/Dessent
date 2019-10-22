
public class IlluminateTwinSpell : TwinSpell
{
    public override int manacost { get; set; } = 0;
    public override SpellName spellName { get; } = SpellName.Illuminate;
    public override Spell spell { get; } = new IlluminateSpell();
    public override BuffSpell buffSpell { get; } = new IlluminateBuff();

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
        spell.Request(privateCell, sm);
        buffSpell.Request(publicCell, sm);
    }
}