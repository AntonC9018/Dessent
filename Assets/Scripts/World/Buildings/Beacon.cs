using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : Building
{
    public override BuildingName type
    {
        get { return BuildingName.Beacon; }
    }
    public override GroundName allowedGroundType
    {
        get { return GroundName.Sea; }
    }

    public List<Vector2Int> vecOrder = new List<Vector2Int>{
        new Vector2Int(2, 0),
        new Vector2Int(1, 1),
        new Vector2Int(-1, 1),
        new Vector2Int(-2, 0),
        new Vector2Int(-1, -1),
        new Vector2Int(1, -1),
    };
    public int currentVecIndex = 0;
    public Cell currentCell;

    public void PrepareNextVec()
    {
        var privateGrid = stateManager.privateGrid;
        Cell cell;
        do
        {
            cell = privateGrid.GetCellAt(parentCell.gridPos + vecOrder[currentVecIndex]);
            currentVecIndex++;
        } while (cell == null);

        currentCell = cell;
    }

    // TODO: this should be a packet!
    public void IlluminateNext()
    {
        currentVecIndex++;

        var req = new ApplyIlluminateSpellRequest
        { 
            coord = currentCell.gridPos,
        };

        stateManager.Request(req);
    }


    // TODO: This should be a packet!
    public void IlluminateThis()
    {
        var req = new ApplyIlluminateSpellRequest
        {
            coord = parentCell.gridPos,
        };

        stateManager.Request(req);
    }

    public override void OnTurnStart()
    {
        IlluminateNext();
        PrepareNextVec();
        IlluminateThis();
    }

    public override void OnTurnEnd()
    {
        IlluminateNext();
        PrepareNextVec();
    }
}