public class SpellTile : SelectableActionTile
{
    public Spell spell;


    public override void ApplyAction(Cell cell)
    {
        if (stateManager.mana.currentMana < spell.manacost)
        {
            print("Low Mana");
        }
        else if (cell.parentGrid.IsPublic())
        {
            print("Choose opponent's tile");
        }
        else if (spell.requiredNumberPhases >= stateManager.selectedAction.phase)
        {
            spell.Request(cell, stateManager);
            stateManager.ResetSelectedAction();
        }
        else
        {
            stateManager.ProgressSelectedActionPhase(cell);
        }
    }
}