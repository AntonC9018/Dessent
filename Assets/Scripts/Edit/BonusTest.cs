using UnityEngine;

public class BonusTest : SelectableActionTile
{
    public override void ApplyAction(Cell cell)
    {
        Debug.Log("BonusTest");

        if (!cell.parentGrid.IsPublic())
        {
            stateManager.instantiator.InstantiateBonusOnCell(cell);
        }
    }
}