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
        else if (building.allowedGroundType == cell.ground.altitude)
        {
            // TODO: display an error
        }
        else if (stateManager.mana.currentMana < building.GetBuildManaCost())
        {
            // TODO: Display an error
        }
        // Can build
        else {
            var req = new ConstructBuildingRequest
            {
                coord = cell.gridPos,
                type = building.type,
            };

            stateManager.Request(req);

            CancelAction();
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