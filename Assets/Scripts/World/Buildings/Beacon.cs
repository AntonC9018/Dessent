using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : Building
{
    // TODO: Add animation / image to this object
    public List<Cell> adjacentCells;
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

    void Start()
    {
        type = BuildingName.Beacon;
    }

    public void PrepareNextVec()
    {
        var privateGrid = parentCell.parentGrid.stateManager.privateGrid;
        Cell cell;
        do
        {
            cell = privateGrid.GetCellAt(parentCell.gridPos + vecOrder[currentVecIndex]);
            currentVecIndex++;
        } while (cell == null);

        currentCell = cell;
    }

    public void IlluminateNext()
    {
        currentVecIndex++;

        var req = new ApplyIlluminateSpellRequest
        { 
            coord = currentCell.gridPos,
        };

        parentCell.parentGrid.stateManager.Request(req);
    }


    public void IlluminateThis()
    {
        var req = new ApplyIlluminateSpellRequest
        {
            coord = parentCell.gridPos,
        };

        parentCell.parentGrid.stateManager.Request(req);
    }

    public override void OnTurnPhaseChange(TurnPhase newPhase)
    {
        if (newPhase == TurnPhase.Start)
        {
            IlluminateNext();
            PrepareNextVec();
            IlluminateThis();
        }
        else if (newPhase == TurnPhase.End)
        {
            IlluminateNext();
            PrepareNextVec();
        }
    }
}