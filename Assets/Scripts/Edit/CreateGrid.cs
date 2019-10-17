using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateGrid : MonoBehaviour
{

    public GameObject cellPref, voidPref, seaPref, earthPref, mountainPref;
    public GameObject goldenPref, redPref, manaPref, bluePref;
    public GameObject
        
        // spells
        illuminatePref,
        floodPref,
        convertPref,
        lightningPref,
        fireStormPref,

        // buffs
        illuminateBuffPref,
        zealotsPref,
        shieldPref,
        purgePref,
        swapCellsPref,

        // buildings
        hutPref,
        stablePref,
        beaconPref,
        monasteryPref;


    public float offsetPercX = 0.05f;
    public float objectOffset = 0.6f;
    public float sizeScale;
    //public float objectShrinkageFactor = 0.8f;

    public struct GridStruct
    {
        public List<GameObject> tiles;
        public GameObject left;
        public GameObject right;
        public GameObject ui;
    };

    private void Start()
    {
        //CalculatePrefSizeScales();
    }

    [ContextMenu("CreateCellsAnton")]
    public void CreateSinglePlayerGameScene()
    {
        DeleteDeleteme();
        CalculatePrefSizeScales();

        var gmobj = new GameObject();
        gmobj.transform.position = new Vector3(0, 0, 0);
        gmobj.transform.localScale = new Vector3(1, 1, 1);
        gmobj.name = "SinglePlayer GameManager";
        gmobj.tag = "DELETEME";        
        var gm = gmobj.AddComponent<SinglePlayerGameManager>();

        for (int i = 0; i < 2; i++)
        {
            var smobj = new GameObject();
            smobj.transform.SetParent(gmobj.transform);
            smobj.transform.localScale = new Vector3(1, 1, 1);
            if (i == 0)
            {
                smobj.transform.localPosition = new Vector3(0, 0, 0);
                smobj.name = "Player 1's Board";
            }
            else
            {
                smobj.transform.localPosition = new Vector3(0, 30, 0);
                smobj.name = "Player 2's Board";
            }
            
            var sm = smobj.AddComponent<StateManager>();
            sm.gameManager = gm;
            sm.createGrid = this;
            gm.stateManagers[i] = sm;
            sm.whichPlayer = (WhichPlayer)i;

            PublicGrid pubg;
            PrivateGrid prig;
            var grid = CreateAGrid(smobj);            
            if (i == 0)
            {                
                pubg = grid.left.AddComponent<PublicGrid>();                
                prig = grid.right.AddComponent<PrivateGrid>();
            }
            else
            {                
                prig = grid.left.AddComponent<PrivateGrid>();                
                pubg = grid.right.AddComponent<PublicGrid>();
            }
            sm.publicGrid = pubg;
            sm.privateGrid = prig;
            pubg.stateManager = sm;
            prig.stateManager = sm;

            // Now add cells to grids
            foreach (var cell in pubg.gameObject.GetComponentsInChildren<Cell>())
            {
                pubg.cells.Add(cell);
                cell.parentGrid = pubg;

                // add extra stuff here
                cell.ground.altitude = GroundName.Void;
                cell.stateManager = sm;
            }

            foreach (var cell in prig.gameObject.GetComponentsInChildren<Cell>())
            {
                prig.cells.Add(cell);
                cell.parentGrid = prig;

                // add extra stuff here
                cell.ground.altitude = GroundName.Void;
                cell.stateManager = sm;
            }

            foreach (var spellTile in grid.ui.GetComponentsInChildren<SpellTile>())
            {
                sm.spellTiles.Add(spellTile);
                spellTile.stateManager = sm;
            }

            foreach (var buffSpellTile in grid.ui.GetComponentsInChildren<BuffSpellTile>())
            {
                sm.buffTiles.Add(buffSpellTile);
                buffSpellTile.stateManager = sm;
            }

            foreach (var buildingTile in grid.ui.GetComponentsInChildren<BuildingTile>())
            {
                sm.buildingTiles.Add(buildingTile);
                buildingTile.stateManager = sm;
            }

            // TODO: implement next turn tile

        }
    }

    [ContextMenu("Delete DELETEME")]
    public void DeleteDeleteme()
    {
        var toDelete = GameObject.FindGameObjectsWithTag("DELETEME");
        foreach (var d in toDelete)
            UnityEditor.EditorApplication.delayCall += () =>
                DestroyImmediate(d);
    }


    private GridStruct CreateAGrid(GameObject holder)
    {
        
        var left = new GameObject();
        left.tag = "DELETEME";
        left.transform.SetParent(holder.transform);
        left.transform.localPosition = new Vector3(-8, 0, 0);
        left.transform.localScale = new Vector3(1, 1, 1);
        left.name = "Left Grid";
        PopulateBoard(left, cellPref, voidPref);

        var right = new GameObject();
        right.tag = "DELETEME";
        right.transform.SetParent(holder.transform);
        right.transform.localPosition = new Vector3(8, 0, 0);
        right.transform.localScale = new Vector3(1, 1, 1);
        right.name = "Right Grid";
        PopulateBoard(right, cellPref, voidPref);

        var ui = new GameObject();
        ui.tag = "DELETEME";
        ui.transform.SetParent(holder.transform);
        ui.transform.localPosition = new Vector3(0, 0, 0);
        ui.transform.localScale = new Vector3(1, 1, 1);
        ui.name = "VariousTiles";

        var tiles = new List<GameObject>();

        tiles.Add( Red <Firestorm>      (fireStormPref) );
        tiles.Add( Red <IlluminateSpell>(illuminatePref));
        tiles.Add( Red <Convert>        (convertPref)   );
        tiles.Add( Red <Flood>          (floodPref)     );
        tiles.Add( Blue<Zealots>        (zealotsPref)   );
        tiles.Add( Red <Lightning>      (lightningPref) );
        tiles.Add( Blue<Purge>          (purgePref)     );
        tiles.Add( Blue<Swapcells>      (swapCellsPref) );
        tiles.Add( Blue<Shield>         (shieldPref)    );
        tiles.Add( ManaTile()                           );
        tiles.Add( Gold()                               );
        tiles.Add( Gold()                               );
        tiles.Add( Gold()                               );
        tiles.Add( Gold()                               );
        tiles.Add( Gold()                               );
        tiles.Add( Gold<Hut>      (hutPref)             );
        tiles.Add( Gold<Stable>   (stablePref)          );
        tiles.Add( Gold<Beacon>   (beaconPref)          );
        tiles.Add( Gold<Monastery>(monasteryPref)       );

        PopulateUI(ui, tiles);

        return new GridStruct
        {
            tiles = tiles,
            ui = ui,
            left = left,
            right = right,
        };
    }

    private void PopulateBoard(
        GameObject container, 
        GameObject cellPref, 
        GameObject cellFilling)
    {
        var cells = new List<Cell>();
        var t = Instantiate(cellFilling);
        var size = t.GetComponent<Renderer>().bounds.size;
        UnityEditor.EditorApplication.delayCall += () =>
                DestroyImmediate(t);

        float yGapScaling = - size.y / size.x * 2 * offsetPercX;
        sizeScale = 1.0f / size.x * (1.0f - offsetPercX) * 2.0f;

        int abs(int n) => n < 0 ? -n : n;
        

        for (int y = 6; y >= -6; y -= 2)
        {
            int xrange = 6 - abs(y) / 2;
            for (int x = -xrange; x <= xrange; x += 2)
            {
                var cell = Instantiate(cellPref);
                cell.transform.SetParent(container.transform);
                cell.transform.localPosition = new Vector3(x, y + y * yGapScaling, y);
                cell.name = $"Cell at x = {x}, y = {y}";

                Cell cellScript = cell.GetComponent<Cell>();
                cellScript.ground =
                    InstantiateGroundOnCell(cellScript, cellFilling);

                if (x < -4)
                {
                    cellScript.building = 
                        InstantiateBuildingOnCell<Hut>(cellScript, hutPref);
                }
                else if (x < -2)
                {
                    cellScript.building =
                        InstantiateBuildingOnCell<Hut>(cellScript, stablePref);
                }
                else if (x < 2)
                {
                    cellScript.building =
                        InstantiateBuildingOnCell<Hut>(cellScript, monasteryPref);
                }
                //else
                //{
                //    cellScript.building =
                //        InstantiateBuildingOnCell<Hut>(cellScript, beaconPref, sizeScale);
                //}

                if (cellScript.building)
                {
                    cellScript.building.gameObject.name = "HELLO";

                }

                cellScript.gridPos = new Vector2Int(x, y);
                
                var polcol = cell.AddComponent<PolygonCollider2D>();
                SetupPolCol(polcol);
                cell.AddComponent<ColliderTest>();
            }
        }
    }


    public float earthPrefSizeScale, seaPrefSizeScale, mountainPrefSizeScale, voidPrefSizeScale;

    private void CalculatePrefSizeScales()
    {
        float calcSize(GameObject e) =>
            (1.0f - offsetPercX) * 2.0f /
            e.GetComponent<Renderer>().bounds.size.x;                 

        GameObject e0 = Instantiate(earthPref);
        earthPrefSizeScale = calcSize(e0);
        GameObject e1 = Instantiate(seaPref);
        seaPrefSizeScale = calcSize(e1);
        GameObject e2 = Instantiate(mountainPref);
        mountainPrefSizeScale = calcSize(e2);
        GameObject e3 = Instantiate(voidPref);
        voidPrefSizeScale = calcSize(e3);

        UnityEditor.EditorApplication.delayCall += () =>
        {
            DestroyImmediate(e0);
            DestroyImmediate(e1);
            DestroyImmediate(e2);
            DestroyImmediate(e3);
        };
    }

    public void LoopGroundOnCellByAltitude(Cell cell, GroundName groundName)
    {
        GroundName newtype;

        // Void, Sea -> Grassland
        if (groundName == GroundName.Void || groundName == GroundName.Sea)
        {
            cell.ground = 
                InstantiateGroundOnCell(cell, earthPref);
            newtype = GroundName.Grassland;
        }
        // Mountain -> Sea
        else if (groundName == GroundName.Mountain)
        {
            cell.ground = 
                InstantiateGroundOnCell(cell, seaPref);
            newtype = GroundName.Sea;
        }
        // Grassland -> Mountain
        //else if (groundName == GroundName.Grassland)
        else
        {
           cell.ground = 
                InstantiateGroundOnCell(cell, mountainPref);
            newtype = GroundName.Mountain;
        }

        cell.ground.altitude = newtype;
        cell.ground.gameObject.name = newtype.ToString();
    }

    public Ground InstantiateGroundOnCell(
        Cell cell, 
        GameObject pref)         
    {
        float calcSize(GameObject e) =>
            (1.0f - offsetPercX) * 2.0f /
            e.GetComponent<Renderer>().bounds.size.x;

        var instance = Instantiate(pref, cell.transform, false);
        var sizeScale = calcSize(instance);
        instance.transform.localScale = new Vector3(
            sizeScale * instance.transform.localScale.x,
            sizeScale * instance.transform.localScale.y,
            1);
        return instance.AddComponent<Ground>();
    }


    public T InstantiateBuildingOnCell<T>(
        Cell cell,
        GameObject pref) //cell.transform.scale.x
        where T : Building
    {

        var instance = Instantiate(pref);
        Vector3 localPos = instance.transform.localPosition;
        Debug.Log(localPos);
        instance.transform.SetParent(cell.gameObject.transform);
        instance.transform.localPosition = new Vector3(
            localPos.x,
            localPos.y + objectOffset,
            -2
            );
        instance.transform.localScale = new Vector3(
            instance.transform.localScale.x * sizeScale,
            instance.transform.localScale.y * sizeScale,
            1);
        return instance.AddComponent<T>();
    }

    public void ResetHoverOnCell(Cell cell)
    {
        var colliderTest = cell.GetComponent<ColliderTest>();
        colliderTest.chang = cell.ground.GetComponent<SpriteRenderer>();
        colliderTest.AutoHover();
    }


    public void CreateBuildingOnCellByType(Cell cell, BuildingName buildingName)
    {
        if (buildingName == BuildingName.Hut)
        {
            cell.building = InstantiateBuildingOnCell<Hut>(cell, hutPref);
        }
        else if (buildingName == BuildingName.Stable)
        {
            cell.building = InstantiateBuildingOnCell<Stable>(cell, stablePref);
        }
        else if (buildingName == BuildingName.Monastery)
        {
            cell.building = InstantiateBuildingOnCell<Monastery>(cell, monasteryPref);
        }
        else if (buildingName == BuildingName.Beacon)
        {
            cell.building = InstantiateBuildingOnCell<Beacon>(cell, beaconPref);
        }

        cell.building.gameObject.name = buildingName.ToString();

    }



    private void PopulateUI(GameObject container, List<GameObject> tiles)
    {
        var size = tiles[0].transform.GetChild(0).GetComponent<Renderer>().bounds.size;
        int i = 0;

        float offsetPercX = 0.05f;
        float yGapScaling = -size.y / size.x * 2.0f * offsetPercX;
        float sizeScale = 1.0f / size.x * (1.0f - offsetPercX) * 2.0f;

        int abs(int n) => n < 0 ? -n : n;

        for (int y = 6; y >= -6; y -= 2)
        {
            int xrange = abs(y) / 2;
            for (int x = -xrange; x <= xrange; x += 2)
            {
                var tile = tiles[i];
                tile.transform.SetParent(container.transform);
                tile.transform.localPosition = new Vector3(x, y + y * yGapScaling, y);
                tile.transform.localScale = new Vector3(1, 1, 1);

                if (tile.transform.childCount > 0)
                {
                    var cell = tile.transform.GetChild(0);
                    cell.localScale = new Vector3(sizeScale, sizeScale, 1);

                    if (cell.childCount > 0)
                    {
                        var child = cell.GetChild(0);
                        child.SetParent(cell);
                        child.localPosition =
                            new Vector3(
                                child.localPosition.x,
                                child.localPosition.y,
                                -1);
                        tile.gameObject.name = child.name.Substring(0, child.name.Length - 7);
                    }
                    cell.gameObject.name = "Cell";
                }
                else
                {
                    tile.gameObject.name = $"Tile at x = {x}, y = {y}";
                }

                i++;

                tile.AddComponent<PolygonCollider2D>();
                var polcol = tile.GetComponent<PolygonCollider2D>();
                SetupPolCol(polcol);
                tile.AddComponent<ColliderTest>();
            }
        }
    }

    private GameObject Red<T>(GameObject obj) where T : Spell, new()
    {
        var cell = Instantiate(redPref);
        var tile = InstantiateAndSetParentAndWrap(obj, cell);
        tile.AddComponent<SpellTile>();
        var script = tile.GetComponent<SpellTile>();
        script.spell = new T();
        return tile;
    }

    private GameObject Blue<T>(GameObject obj) where T : BuffSpell, new()
    {
        var cell = Instantiate(bluePref);
        var tile = InstantiateAndSetParentAndWrap(obj, cell);
        tile.AddComponent<BuffSpellTile>();
        var script = tile.GetComponent<BuffSpellTile>();
        script.buff = new T();
        return tile;
    }


    private GameObject Gold<T>(GameObject obj) where T : Building
    {
        var cell = Instantiate(goldenPref);
        var tile = InstantiateAndSetParentAndWrap(obj, cell);
        tile.AddComponent<BuildingTile>();
        var buil = cell.transform.GetChild(0).gameObject;
        buil.AddComponent<T>();
        tile.GetComponent<BuildingTile>().building = buil.GetComponent<T>();
        return tile;
    }

    private GameObject Gold()
    {
        var cell = Instantiate(goldenPref);
        var tile = new GameObject();
        cell.transform.SetParent(tile.transform);
        return tile;
    }

    private GameObject InstantiateAndSetParentAndWrap(GameObject pref, GameObject parent)
    {
        var cont = new GameObject();
        parent.transform.SetParent(cont.transform);
        parent.transform.localPosition = new Vector3(0, 0, -1);
        var instance = Instantiate(pref);
        instance.transform.SetParent(parent.transform);
        return cont;
    }

    private GameObject ManaTile()
    {
        var cell = Instantiate(manaPref);
        var tile = new GameObject();
        cell.transform.SetParent(tile.transform);
        return tile;
    }


    private void SetupPolCol(PolygonCollider2D polcol)
    {
        polcol.pathCount = 1;
        polcol.SetPath(0, new List<Vector2>{
            new Vector2( 0.915f,  0.456f),
            new Vector2( 0.915f, -0.464f),
            new Vector2( 0.003f, -1.006f),
            new Vector2(-0.921f, -0.460f),
            new Vector2(-0.907f,  0.465f),
            new Vector2( 0.034f,  1.013f),
        });
    }
}
