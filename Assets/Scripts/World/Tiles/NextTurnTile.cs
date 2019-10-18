public class NextTurnTile : SelectableActionTile
{
    public override void OnMouseButtonUp()
    {
        stateManager.Request(new EndTurnRequest());
        stateManager.ResetSelectedAction();
    }

    public override void CancelAction()
    {
        throw new System.NotImplementedException();
    }
}