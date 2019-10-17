public class TwinSpellTile : SelectableActionTile
{
    public TwinSpell twinSpell;

    public override void ApplyAction(Cell cell)
    {
        if (stateManager.mana.currentMana >= twinSpell.manacost)
        {
            twinSpell.Request(cell, stateManager);
        }
        else
        {
            // TODO: Display to the user some message with an error
            // BTW, onButtonClick is overridable too, so
            // you could add some manarelated error checking there
            // to not even let the user select a spell when mana is low
        }
    }

    public override void CancelAction()
    {
        throw new System.NotImplementedException();
    }
}