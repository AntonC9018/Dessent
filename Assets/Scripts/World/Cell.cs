using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Cell : MonoBehaviour
{

    public List<Bonus> bonuses;
    public List<Buff> buffs;
    public Ground ground;
    public Building building;
    public Vector2Int gridPos;
    public HexGrid parentGrid;
    public StateManager stateManager;

    public GameObject upgradeArrowPref;

    public List<GameObject> popups = new List<GameObject>();

    public bool isChanged;
    // iluminated / uniluminated state
    public int illuminated;


    public bool AddBuff(Buff buff)
    {
        buffs.Add(buff);
        return true;
    }


    // use whichever one is more convenient
    public Cell(Vector2Int pos, HexGrid parent)
    {
        gridPos = pos;
        parentGrid = parent;
    } 
    public Cell(Vector3 pos, HexGrid parent)
    {
        gridPos = new Vector2Int((int)pos.x, (int)pos.y);
        parentGrid = parent;
    }

    // the cell should keep buffs but lose bonuses
    // i.e. the two cells should exchange bonuses
    public void MoveTo(Vector2Int destination)
    {
        gridPos = destination;

        // TODO: change world position stateManageroothly through some other logic
    }


    public static void ExchangeCells(Cell lhs, Cell rhs)
    {
        // Exchange bonuses
        List<Bonus> buffer = lhs.bonuses;
        lhs.bonuses = rhs.bonuses;
        rhs.bonuses = buffer;

        Vector2Int coord = lhs.gridPos;
        lhs.MoveTo(rhs.gridPos);
        rhs.MoveTo(coord);

        // if the cells come from different grids, attempt 
        // to convert the enemy's cell into ours
        // and vice-versa
        if (lhs.parentGrid.IsPublic() && !lhs.parentGrid.IsPublic())
        {
            AttemptConversion(lhs, rhs);
        }
        else if (!lhs.parentGrid.IsPublic() && lhs.parentGrid.IsPublic())
        {
            AttemptConversion(rhs, lhs);
        }
    }


    private static void AttemptConversion(Cell publicCell, Cell privateCell)
    {
        // TODO: implement
        // if publicCell has no buildings and sth like that,
        // move it to the privateGrid (give it to the opponent)
        // similarly, do the same if the religion level of 
        // a privateCell being converted to a publicCell 
        // is lower that 3 (i.e. you have more or same influence)
    }


    // TODO: Implement, + mark the cells somehow, oh
    // may be like adding 10 or some other arbitrary number
    // to their coordinate?
    public void Convert()
    {
        if (parentGrid.IsPublic())
        {
            // move the cell to the private grid            
        }
        else
        {
            // move it to the public grid
        }
    }


    public List<Cell> GetAdjacent()
    {

        List<Cell> result = new List<Cell>();


        Vector2Int[] xys = {
            new Vector2Int(-1, -2),
            new Vector2Int( 1, -2),
            new Vector2Int(-2,  0),
            new Vector2Int( 2,  0),
            new Vector2Int(-1,  2),
            new Vector2Int( 1,  2),
        }; 

        foreach(Vector2 xy in xys)
        {
            foreach(Cell cell in parentGrid.cells)
            {
                if ((xy + gridPos).Equals(cell.gridPos))
                {
                    result.Add(cell);
                }
            }
        }

        return result;
    }


    public void ChangeGround(Ground ground)
    {
        this.ground = ground;
        // TODO: call an animation 
        // TODO: update buildings
    }

    public void UpgradeBuilding()
    {
        building.Upgrade();
    }

    private bool mouseOver = false;

    void Update()
    {
        if (mouseOver)
        {
            if (Input.GetMouseButtonUp(0))
            {
                // release left
                stateManager.OnMouseButtonUpOnCell(this);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                stateManager.OnMouseButtonDownOnCell(this);
            }
        }
    }

    public void CreateAndSetBuilding(BuildingStruct b)
    {
        building = Building.Create(b);
    }


    // TODO: Centralize
    public void OnMouseEnter()
    {
        mouseOver = true;
        if (Input.GetMouseButton(0))
        {
            stateManager.OnMouseDraggedOntoCell(this);
        }
        stateManager.OnMouseEnterOnCell(this);
        
    }


    public void OnMouseExit()
    {
        mouseOver = false;
        if (Input.GetMouseButton(0))
        {
            stateManager.OnMouseDraggedOutOfCell(this);
        }
        stateManager.OnMouseExitOnCell(this);
    }

    public void ResetIsChanged()
    {
        isChanged = false;
    }
}
