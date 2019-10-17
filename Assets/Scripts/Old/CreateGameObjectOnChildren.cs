using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameObjectOnChildren : MonoBehaviour
{
    
    public GameObject cellParentPrefab;
    
    [ContextMenu("Create gameObjects")] 
    public void CreateGameObj()
    {
        var children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        foreach (var child in children)
        {
            var temp = Instantiate(cellParentPrefab, child.transform.position, Quaternion.identity, transform);
            var ground = child.gameObject.AddComponent<Ground>();
            ground.altitude = GroundName.Void;
            
            var cell = temp.GetComponent<Cell>();
            cell.gridPos = child.GetComponent<Cell>().gridPos;
            cell.parentGrid = GetComponentInParent<HexGrid>();
            cell.parentGrid.cells.Add(cell);
            cell.ground = ground;
            
            child.SetParent(temp.transform, true);
            temp.name = "Cell " + cell.gridPos;
            DestroyImmediate(ground.GetComponent<Cell>());
        }
    }
}
