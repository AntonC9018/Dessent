public class NextTurnTile : SelectableActionTile
{
    //public BuffSpell buff;


    // TODO: Add EndTurn request instead
    public override void ApplyAction(Cell cell)
    {
        //.Request(cell, stateManager);
        stateManager.Request(new EndTurnRequest());
    }

    public override void CancelAction()
    {
        throw new System.NotImplementedException();
    }
}