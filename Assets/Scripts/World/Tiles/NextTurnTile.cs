public class NextTurnTile : SelectableActionTile
{
    public override void OnMouseButtonUp()
    {
        stateManager.Request(new EndTurnRequest());
        stateManager.ResetSelectedAction();
    }
}