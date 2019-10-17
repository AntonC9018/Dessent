using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HexGrid : MonoBehaviour
{
    public List<Cell> cells = new List<Cell>();
    public List<Cell> externalCells = new List<Cell>();
    public Mana mana;
    public StateManager stateManager;

    abstract public bool IsPublic();

    public Cell GetCellAt(Vector2Int pos)
    {
        foreach (Cell cell in cells)
        {
            if (cell.gridPos == pos)
            {
                return cell;
            }
        }

        return null;
    }

    private void Start()
    {
        stateManager = GetComponentInParent<StateManager>();
    }
}