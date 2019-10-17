using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class Instantiator : MonoBehaviour
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
            sm.instantiator = this;
            gm.stateManagers[i] = sm;
            sm.whichPlayer = (WhichPlayer)i;

            PublicGrid pubg;
            PrivateGrid prig;
            var grid = CreateAGrid(smobj);            
            if (i == 0)
            {  
                // Left player
                pubg = grid.left.AddComponent<PublicGrid>();                
                prig = grid.right.AddComponent<PrivateGrid>();
            }
            else
            {                
                // Right player
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
    // Destroy all objects in scene
    public void DeleteDeleteme()
    {
        var toDelete = GameObject.FindGameObjectsWithTag("DELETEME");
        foreach (var d in toDelete)
            UnityEditor.EditorApplication.delayCall += () =>
                DestroyImmediate(d);
    }


    private GridStruct CreateAGrid(GameObject holder)
    {
        
        // Set up the LEFT BOARD
        var left = new GameObject();
        left.transform.SetParent(holder.transform);
        left.transform.localPosition = new Vector3(-8, 0, 0);
        left.transform.localScale = new Vector3(1, 1, 1);
        left.name = "Left Grid";
        PopulateBoard(left, cellPref, voidPref);

        // Set up the RIGHT BOARD
        var right = new GameObject();
        right.transform.SetParent(holder.transform);
        right.transform.localPosition = new Vector3(8, 0, 0);
        right.transform.localScale = new Vector3(1, 1, 1);
        right.name = "Right Grid";
        PopulateBoard(right, cellPref, voidPref);

        // Set up UI tiles
        var ui = new GameObject();
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

    private void PopulateBoard
        (GameObject container, GameObject cellPref, GameObject cellFilling)
    {
        var cells = new List<Cell>();
        var t = Instantiate(cellFilling);
        var size = t.GetComponent<Renderer>().bounds.size;
        UnityEditor.EditorApplication.delayCall += () =>
                DestroyImmediate(t);

        float yGapScaling = -size.y / size.x * 2 * offsetPercX;

        // modify the global sizeScale as this value will be used later
        sizeScale = 1.0f / size.x * (1.0f - offsetPercX) * 2.0f;

        // define a quick absolute value expression
        int abs(int n) => n < 0 ? -n : n;        

        for (int y = 6; y >= -6; y -= 2)
        {
            int xrange = 6 - abs(y) / 2;
            for (int x = -xrange; x <= xrange; x += 2)
            {
                // Setup, move, rescale
                var cell = Instantiate(cellPref);
                cell.transform.SetParent(container.transform);
                cell.transform.localPosition = new Vector3(x, y + y * yGapScaling, y);
                // Rename
                cell.name = $"Cell at {x}, {y}";

                // Setup components
                Cell cellScript = cell.AddComponent<Cell>();
                cellScript.gridPos = new Vector2Int(x, y);
                cellScript.ground =
                    InstantiateGroundOnCell(cellScript, cellFilling);

                // -----------------------------------------------------
                // DEBUGGING ZONE
                if (x < -4)
                {
                    cellScript.building = 
                        InstantiateBuildingOnCell<Hut>(cellScript, hutPref);
                }
                else if (x < -2)
                {
                    cellScript.building =
                        InstantiateBuildingOnCell<Stable>(cellScript, stablePref);
                }
                else if (x < 2)
                {
                    cellScript.building =
                        InstantiateBuildingOnCell<Monastery>(cellScript, monasteryPref);
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
                // DEBUGGING ZONE ENDED
                // --------------------------------------------------------
                                
                // Add and set up collider script
                var polcol = cell.AddComponent<PolygonCollider2D>();
                SetupPolCol(polcol);

                // Uncomment for debugging
                // TODO: rename
                cell.AddComponent<ColliderTest>();
            }
        }
    }

    public void LoopGroundOnCellByAltitude(Cell cell, GroundName groundName)
    {

        // Void, Sea -> Grassland
        if (groundName == GroundName.Void || groundName == GroundName.Sea)
        {
            cell.ground = InstantiateGroundOnCell(cell, earthPref);
            cell.ground.altitude = GroundName.Grassland;
        }

        // Mountain -> Sea
        else if (groundName == GroundName.Mountain)
        {
            cell.ground = InstantiateGroundOnCell(cell, seaPref);
            cell.ground.altitude = GroundName.Sea;
        }

        // Grassland -> Mountain
        //else if (groundName == GroundName.Grassland)
        else
        {
            cell.ground =  InstantiateGroundOnCell(cell, mountainPref);
            cell.ground.altitude = GroundName.Mountain;
        }

        // rename the object in the editor
        cell.ground.gameObject.name = cell.ground.altitude.ToString();
    }

    public Ground InstantiateGroundOnCell(Cell cell, GameObject pref)
    {
        var instance = Instantiate(pref, cell.transform, false);
        instance.transform.localScale = new Vector3(
            sizeScale * instance.transform.localScale.x,
            sizeScale * instance.transform.localScale.y,
            1);
        // return the Ground instance
        return instance.AddComponent<Ground>();
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

    // This can be use for Hut, Stable, Monastery, Beacon and 
    // on any other buildings that might be added in future
    public T InstantiateBuildingOnCell<T>(Cell cell, GameObject pref)
        where T : Building
    {

        var instance = Instantiate(pref);
        // save the localPosition in a variable as we need to 
        // keep the relative offset of the building
        Vector3 localPos = instance.transform.localPosition;

        // the new object remains at place, so need to shift it back
        instance.transform.SetParent(cell.gameObject.transform);

        // move the object back and shift it up a little
        instance.transform.localPosition = new Vector3(
            localPos.x,
            localPos.y + objectOffset,
            -2 // keep it in front
            );

        // rescale it so that it is the same size as the cell
        instance.transform.localScale = new Vector3(
            instance.transform.localScale.x * sizeScale,
            instance.transform.localScale.y * sizeScale,
            1);

        // return the Building instance
        return instance.AddComponent<T>();
    }


    public void ResetHoverOnCell(Cell cell)
    {
        var colliderTest = cell.GetComponent<ColliderTest>();
        colliderTest.chang = cell.ground.GetComponent<SpriteRenderer>();
        colliderTest.AutoHover();
    }





    private void PopulateUI(GameObject container, List<GameObject> tiles)
    {
        // we assume all tile sprites are of same size
        var size = tiles[0].transform.GetChild(0).GetComponent<Renderer>().bounds.size;
        int i = 0;

        // we need to recalculate this as the sprites for 
        // golden and mana tiles are not the same (in fact, they're smaller)
        float yGapScaling = -size.y / size.x * 2.0f * offsetPercX;
        float sizeScale = 1.0f / size.x * (1.0f - offsetPercX) * 2.0f;

        // define a quick absolute value for integers
        int abs(int n) => n < 0 ? -n : n;

        for (int y = 6; y >= -6; y -= 2)
        {
            int xrange = abs(y) / 2;
            for (int x = -xrange; x <= xrange; x += 2)
            {
                // get the non-positioned tile
                var tile = tiles[i++];

                // reposition, rescale
                tile.transform.SetParent(container.transform);
                tile.transform.localPosition = new Vector3(x, y + y * yGapScaling, y);
                tile.transform.localScale = new Vector3(sizeScale, sizeScale, 1);

                // rename
                RenameTileAndChildren(tile);

                // Add the collider
                var polcol = tile.AddComponent<PolygonCollider2D>();
                // Set it up
                SetupPolCol(polcol);

                // Uncomment this for debugging
                // TODO: rename
                tile.AddComponent<ColliderTest>();
            }
        }
    }

    private void RenameTileAndChildren(GameObject tile)
    {
        if (tile.transform.childCount > 0)
        {
            var cell = tile.transform.GetChild(0);

            if (cell.childCount > 0)
            {
                var child = cell.GetChild(0);
                tile.gameObject.name =
                    // remove the " (Copy)" at the end
                    child.name.Substring(0, child.name.Length - 7);
            }
            // rename the cell to "Cell"
            cell.gameObject.name = "Cell";
        }
        else
        {
            tile.gameObject.name = $"Tile";
        }
    }

    // IMPORTANT: The following methods do not reposition 
    // the elements they create, they just set them up.
    // Repositioning is done in PopulateUI()
    private GameObject Red<T>(GameObject obj) 
        where T : Spell, new()
    {
        // Tile (SpellTile) {  RedCell (basically an image)  }
        var cell = Instantiate(redPref);
        var tile = InstantiateAndSetParentAndWrap(obj, cell);

        // Make the tile a SpellTile
        tile.AddComponent<SpellTile>()
            // + add the Spell on it
            .spell = new T();

        // return the container object
        return tile;
    }

    private GameObject Blue<T>(GameObject obj) 
        where T : BuffSpell, new()
    {
        // Tile (BuffSpellTile) {  BlueCell (basically an image)  }
        var cell = Instantiate(bluePref);
        var tile = InstantiateAndSetParentAndWrap(obj, cell);

        // Make the tile a BuffSpellTile
        tile.AddComponent<BuffSpellTile>()
            // + add the BuffSpell on it
            .buffSpell = new T();

        // return the container object
        return tile;
    }

    private GameObject Gold<T>(GameObject obj)
        where T : Building
    {
        // Tile (BuildingTile) {  GoldenCell {  Building  }  }
        var cell = Instantiate(goldenPref);
        var tile = InstantiateAndSetParentAndWrap(obj, cell);

        var buil = cell.transform.GetChild(0).gameObject;
        // Make the container a BuildingTile 
        tile.AddComponent<BuildingTile>()
            // + make the building a Building
            .building = buil.AddComponent<T>();
        
        // return the container object
        return tile;
    }

    private GameObject Gold()
    {
        // Tile {  Cell (without script)  }
        var cell = Instantiate(goldenPref);
        var tile = new GameObject();
        cell.transform.SetParent(tile.transform);
        return tile;
    }

    // A helper methot to reduce a bunch of code
    private GameObject InstantiateAndSetParentAndWrap
        (GameObject pref, GameObject parent)
    {
        var cont = new GameObject();

        // wrap the parent in container
        parent.transform.SetParent(cont.transform);
        parent.transform.localPosition = new Vector3(0, 0, -1);

        // wrap the prefab in parent
        var instance = Instantiate(pref);
        instance.transform.SetParent(parent.transform);
        instance.transform.localPosition = new Vector3(
            instance.transform.localPosition.x,
            instance.transform.localPosition.y, 
            -2);

        return cont;
    }

    private GameObject ManaTile()
    {
        var cell = Instantiate(manaPref);
        var tile = new GameObject();
        cell.transform.SetParent(tile.transform);
        return tile;
    }


    // Use the hardcoded Path coordinates to set up collider on cells
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
