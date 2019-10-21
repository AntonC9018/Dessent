using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Cell : MonoBehaviour
{

    public List<Bonus> bonuses = new List<Bonus>();
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

    // the cell should keep buffs but lose bonuses
    // i.e. the two cells should exchange bonuses
    public void MoveTo(Vector2Int pos, Vector2 worldPos)
    {
        gridPos = pos;
        gameObject.transform.localPosition = worldPos;
    }


    public static void ExchangeCells(Cell lhs, Cell rhs)
    {
        // Exchange bonuses
        List<Bonus> buffer = lhs.bonuses;
        lhs.bonuses = rhs.bonuses;
        rhs.bonuses = buffer;

        Vector2Int coord = lhs.gridPos;
        Vector2 worldPos = lhs.gameObject.transform.localPosition;
        lhs.MoveTo(rhs.gridPos, rhs.gameObject.transform.localPosition);
        rhs.MoveTo(coord, worldPos);

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

        foreach(var xy in xys)
        {
            var cell = parentGrid.GetCellAt(gridPos + xy);
            if (cell != null)
            {
                result.Add(cell);
            }
        }

        return result;
    }


    public bool mouseOver = false;

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
