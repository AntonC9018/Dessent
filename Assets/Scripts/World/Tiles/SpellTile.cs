public class SpellTile : SelectableActionTile
{
    public SpellBase spell;

    private bool IsCellTargetable(Cell cell)
    {
        if (spell.spellType == SpellType.TwinSpell) return true;

        bool isPublic = cell.parentGrid.IsPublic();
        if (spell.spellType == SpellType.Spell)
        {
            return isPublic;
        }
        if (spell.spellType == SpellType.BuffSpell)
        {
            return !isPublic;
        }
        return false;
    }


    public override void ApplyAction(Cell cell)
    {
        if (stateManager.mana.currentMana < spell.manacost)
        {
            print("Low Mana");
        }
        else if (IsCellTargetable(cell))
        {
            print("Cell not targetable");
        }
        else if (spell.requiredNumberPhases == stateManager.selectedAction.phase)
        {
            spell.Request(cell, stateManager);
            stateManager.ResetSelectedAction();
        }
        else
        {
            spell.OnActionPhaseChange(cell, stateManager.selectedAction.phase);
            stateManager.ProgressSelectedActionPhase(cell);
        }
    }
}