using UnityEngine;
public class BuildingTile : SelectableActionTile
{
    //public BuffSpell buff;
    public Building building;
    public GameObject ghost;

    public override void ApplyAction(Cell cell)
    {


        if (cell.building)
        {
            Debug.Log("Building already exists");
            // Building already exists
            // TODO: Display an error to the player
        }

        else if (building.allowedGroundType != cell.ground.altitude)
        {
            Debug.Log($"Not right type of ground. Expected {building.allowedGroundType.ToString()}, got {cell.ground.altitude.ToString()}");
            // Ground type not allowed check
            // TODO: Display an error
        }

        else if (stateManager.mana.currentMana < building.GetBuildManaCost())
        {
            Debug.Log("Not enough mana");
            // Low mana
            // TODO: Display an error
        }
        else {
            // Can build
            var req = new ConstructBuildingRequest
            {
                coord = cell.gridPos,
                type = building.type,
            };

            stateManager.Request(req);
            ghost.SetActive(false);
        }

        print($"Building has level: {cell.building.level}");
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