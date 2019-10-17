//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class GridCreation : MonoBehaviour
//{

//    public Image prefab;
//    private Image temp;
//    public float offset;
//    public float yOverlap;

//    public SinglePlayerGameManager singleManager;
//    public int id;
//    public GameObject otherGrid;

//    private void Start()
//    {
//        GetCells();
//    }

//    [ContextMenu("CreateCells")]
//    public void Grid4X4()
//    {
//        for (var i = transform.childCount; i-- > 0;)
//            UnityEditor.EditorApplication.delayCall += () =>
//            {
//                DestroyImmediate(transform.GetChild(0).gameObject);
//            };


//        var rect = prefab.rectTransform;
//        float heightMod = (rect.rect.height + yOverlap) * rect.localScale.y * 3 / 4 + offset / Mathf.Cos(Mathf.PI / 3);
//        //float widthMod = rect.rect.width * rect.localScale.x / 2 + offset;

//        //float heightMod = ((hardcodeHeight + yOverlap) * rect.localScale.y * 3) / 4 + offset / Mathf.Cos(Mathf.PI / 3); //localscale rect.localScale.y *
//        //float widthMod = hardcodeWidth * rect.localScale.x / 2 + offset; //localscale * rect.localScale.x

//        HexGrid hexGrid = GetComponentInParent<HexGrid>();
//        hexGrid.cells = new List<Cell>();

//        for (int y = 3; y >= -3; y--)
//            for (int x = -6 + Mathf.Abs(y); x <= 6 - Mathf.Abs(y); x += 2)
//            {
//                temp = Instantiate(prefab, transform.position + new Vector3(x * widthMod, y * heightMod, 0f), Quaternion.identity, transform);
//                Cell cell = temp.GetComponent<Cell>();
//                cell.gridPos = new Vector2Int(x, y);
//                hexGrid.cells.Add(cell);
//            }

//    }

//    public void GetCells()
//    {
//        HexGrid hexGrid = GetComponentInParent<HexGrid>();
//        hexGrid.cells = new List<Cell>();

//        foreach (Cell cell in GetComponentsInChildren<Cell>())
//        {
//            hexGrid.cells.Add(cell);
//        }
//    }
//}
