public class TwinSpellTile : SelectableActionTile
{
    public TwinSpell twinSpell;

    public override void ApplyAction(Cell cell)
    {
        //if (stateManager.mana.currentMana >= twinSpell.manacost)
        //{
        //    if (twinSpell.requiredNumberPhases >= stateManager.selectedAction.phase)
        //    {
        //        twinSpell.Request(cell, stateManager);
        //    }
        //}
        //else
        //{
        //    print("Low Mana");
        //}
    }
}