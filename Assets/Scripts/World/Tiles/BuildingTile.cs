using UnityEngine;
public class BuildingTile : SelectableActionTile
{
    //public BuffSpell buff;
    public Building building;
    public GameObject ghost;


    public override void ApplyAction(Cell cell)
    {
        // TODO: Add construct building request
        // for now, just create the building
        // TODO: figure manacosts
        if (cell.building)
        {
            // TODO: Display an error to the player
        }
        else if (stateManager.mana.currentMana >= 0 // cell.GetBuildingCost(type)
            && Building.IsAllowedBuildingOnCell(building.type, cell))
        {
            var req = new ConstructBuildingRequest
            {
                coord = cell.gridPos,
                type = building.type,
            };

            stateManager.Request(req);

            CancelAction();
        }
        else
        {
            // TODO: Display to the user some message with an error
            // BTW, onButtonClick is overridable too, so
            // you could add some manarelated error checking there
            // to not even let the user select a spell when mana is low
        }
    }

    public override void OnMouseEnterOnCell(Cell cell)
    {
        if (cell.building == null)
        {
            stateManager.instantiator.TeleportObjectToCell(cell, ghost);
        }
    }

    public override void OnMouseExitOnCell(Cell cell)
    {
        ghost.SetActive(false);
    }

    public override void OnMouseButtonDownOnCell(Cell cell)
    {
        //ghost.SetActive(false);
        ApplyAction(cell);
    }

    public override void CancelAction()
    {
        stateManager.ResetSelectedAction();
        ghost.SetActive(false);
    }
}