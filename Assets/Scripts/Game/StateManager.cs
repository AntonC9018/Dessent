using System.Collections.Generic;
using UnityEngine;

public delegate void DelegateButtonClick(Cell fun, StateManager sm);

public class StateManager : MonoBehaviour
{   
    //public static Spell[] allSpells;
    // the actual grid of the player
    public PublicGrid publicGrid; 
    // the grid of the other player as they see it
    public PrivateGrid privateGrid;

    public List<SpellTile> spellTiles = new List<SpellTile>();
    public NextTurnTile nextTurnTile;
    public List<BuildingTile> buildingTiles = new List<BuildingTile>();

    public IDictionary<BuildingName, int> buildingCount
        = new Dictionary<BuildingName, int>();
    public IDictionary<SpellName, SpellBase> spells
        = new Dictionary<SpellName, SpellBase>();

    public GameManager gameManager;
    public Mana mana;

    public WhichPlayer whichPlayer;

    public int turn = 0;
    public TurnPhase turnPhase = TurnPhase.Start;
    public GamePhase phase = GamePhase.Terraforming;

    public Instantiator instantiator;
    
    void Start()
    {
        mana = new Mana();
        mana.stateManager = this;
        selectedAction.cells = new List<Cell>();
    }

    // Method that hands the request to GameManager
    public void Request(Request req)
    {
        gameManager.Request(this, req);
    }


    // Method that receives a response to a given request 
    // (from this StateManager to GameManager), to this StateManager 
    public void ReceiveResponse(Response response)
    {
        // here is where the logic happens 1
        switch (response.headerName)
        {

            case HeaderName.ApplySpell:
                {
                    var res = (ApplySpellResponse)response;
                    var spell = FindSpell(res.name);
                    spell.RealizeResponse(res, this, true);
                    mana.UseMana(spell.manacost);
                    mana.UpdateMana();
                    break;
                }


            case HeaderName.ApplyBuff:
                {
                    var res = (ApplyBuffResponse)response;
                    var buff = FindSpell(res.name);
                    buff.RealizeResponse(res, this, true);
                    mana.UseMana(buff.manacost);
                    mana.UpdateMana();
                    break;
                }


            case HeaderName.ConstructBuilding:
                {
                    var res = (ConstructBuildingResponse)response;
                    // Find the relevant cell
                    Cell cell = publicGrid.GetCellAt(res.coord);
                    // Instantiate building with script
                    instantiator.SpawnBuildingOnCellByType(cell, res.type);
                    // Get Illuminate
                    var illumRes = res.illuminate;
                    // Apply illuminate
                    if (illumRes != null)
                    {
                        var illum = GetIlluminateBuffSpell();
                        illum.RealizeResponse(illumRes, this, true);
                    }
                    // Spend mana
                    mana.UseMana(cell.building.GetBuildManaCost());
                    // Update mana stats like mps and max mana
                    mana.UpdateMana();
                    // update count
                    buildingCount[cell.building.type] += 1;
                    break;
                }

            // response to the end turn request initiated by the player
            case HeaderName.EndTurn:
                {
                    var res = (EndTurnResponse)response;
                    turn = res.turnCount;
                    SyncGamePhase();
                    OnEndOfTurn();
                    break;
                }
            // TODO: add more cases in a similar way
            // See enums.cs -> enum HeaderName
        }
    }


    // Helper method that hand a response over to GameManager
    public void Respond(Response res)
    {
        gameManager.Respond(this, res);
    }


    public void ReceivePacket(Packet packet)
    {
        switch (packet.headerName)
        {

            // This happens only when a player runs out of time
            // and their turn ends forcefully
            case HeaderName.EndTurn:
                {
                    // TODO: Timeout animation
                    var pack = (EndTurnPacket)packet;
                    turn = pack.turnCount;
                    SyncGamePhase();
                    OnEndOfTurn();
                    break;
                }

            case HeaderName.StartTurn:
                {
                    var pack = (StartTurnPacket)packet;
                    turn = pack.turnCount;
                    SyncGamePhase();
                    OnStartOfTurn();
                    break;
                }
                // TODO: Add receiving of packets for all buffs
                // and spells
        }
    }


    public void SetBuildingOnCell(Cell cell, BuildingStruct building)
    {
        if (cell.building != null && cell.building.type != building.type)
        {
            Destroy(cell.building.gameObject);
        }
        instantiator.SpawnBuildingOnCellByType(cell, building.type);

        cell.building.hp = building.hp != 0 ? building.hp : cell.building.hp;
        cell.building.religion = building.religion != 0 ? building.religion : cell.building.religion;
        cell.building.activeState = building.activeState;
        cell.building.level = building.level != 0 ? building.level : cell.building.level;
    }
    

    public void SetGroundOnCell(Cell cell, GroundName groundName)
    {
        Destroy(cell.ground.gameObject);
        instantiator.SpawnGroundOnCellByAltitude(cell, groundName);
    }



    private void OnEndOfTurn()
    {
        turnPhase = TurnPhase.End;
        mana.UpdateMana();

        // Add more logic here
        // ...
        // ...

        turnPhase = TurnPhase.Opponent;
    }

    private void OnStartOfTurn()
    {
        turnPhase = TurnPhase.Start;
        mana.UpdateMana();

        // Add more logic here
        // ...
        // ...

        turnPhase = TurnPhase.You;
    }


    //public void Packet(Packet req)
    //{
    //    //gameManager.Packet(this, req);
    //}

    // TODO: perform a recalculation of the current mps value
    // and reset the current value (including UI)


    // TODO: make some history struct that will hold
    // Requests (from server), Packets, Responses (to server)

    // NOTE: this list never includes Requests from another player
    // THEY SHOULD BE RECEIVED JUST BY THE GAMEMANAGER
    // these StateManager's should not be concerned with realizing Requests 
    // from the other player, neither should they ever see one.
    // The requests are either from the server, or they are just packets
    // (unequivocally called a Packet)
    // (also from the server) with just some optional info (for some buffs)

    // TODO: Maybe address this ambiguity of Requests and Responses in the future
    //public History history;


    public struct SelectedAction
    {
        public SelectableActionTile tile;
        public List<Cell> cells;
        public int phase;
        public bool set;
    }

    public SelectedAction selectedAction;

    private Cell lastActiveCell;
    private bool ignoringEvents = false; // i.e. right mouse button pressed
    private Cell ignoredCell;
    private bool rightReleased;


    public void SetSelectedAction(SelectableActionTile tile)
    {
        if (selectedAction.set)
        {
            selectedAction.tile.OnActionCanceled();
            ResetSelectedAction();
        }
        selectedAction.tile = tile;
        selectedAction.set = true;
    }


    public void ResetSelectedAction()
    {
        selectedAction.set = false;
        ResetSelectedActionProgress();
    }


    public void ProgressSelectedActionPhase(Cell cell)
    {
        // progress phase
        selectedAction.phase++;
        // store previously clicked cells
        selectedAction.cells.Add(cell);
    }

    public void ResetSelectedActionProgress()
    {
        // reset phase
        selectedAction.phase = 0;
        // reset the list
        selectedAction.cells.Clear();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            foreach (Cell cell in publicGrid.cells)
                cell.ResetIsChanged();

            if (rightReleased)
            {
                IgnoreEvents(false);
                IgnoreCell(lastActiveCell);
                rightReleased = false;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            // start ignoring events
            IgnoreEvents(true);
            if (selectedAction.set)
            {
                selectedAction.tile.OnActionCanceled();
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            IgnoreCell(lastActiveCell);
            if (Input.GetMouseButton(0))
            {
                rightReleased = true;
            }
            else IgnoreEvents(false);
        }
    }

    void Setup()
    {
        selectedAction.cells = new List<Cell>();
    }

    public void IgnoreEvents(bool ignore)
    {
        if (ignore)
        {
            ignoringEvents = true;
            // turn off animations of selected action
        }
        else
        {
            ignoringEvents = false;
        }
    }

    public bool IsIgnoring(Cell cell)
    {
        if (ignoredCell == cell)
        {
            return true;
        }
        if (ignoringEvents)
        {
            lastActiveCell = cell;
            return true;
        }
        return false;
    }

    public void IgnoreCell(Cell cell)
    {
        ignoredCell = cell;
    }

    public void StopIgnoringCell()
    {
        ignoredCell = null;
    }

    public void ResetLastActiveCell(Cell cell)
    {
        lastActiveCell = cell;
    }

    private void TerraformCell(Cell cell)
    {
        if (cell.parentGrid.IsPublic() && !cell.isChanged)
        {
            Destroy(cell.ground.gameObject);
            instantiator.LoopGroundOnCellByAltitude(cell, cell.ground.altitude);
            cell.isChanged = true;
        }
    }

    // Cell event callback methods
    public void OnMouseEnterOnCell(Cell cell)
    {
        // ignore the event if the right mouse button has been clicked
        // or is being held down
        if (IsIgnoring(cell)) return;

        // NOTE: I'm not sure whether we should check it all over the place
        // so for now I'm going to omit this check in future methods
        if (phase == GamePhase.Terraforming)
        {

        }
        // the action is set
        else if (selectedAction.set)
        {
            selectedAction.tile.OnMouseEnterOnCell(cell);
        }
        // action is not selected
        else 
        {
            // do a contextual pop-up
            if (cell.building != null)
            {
                // show an arrow over the building
                //var arrow = Instantiate(
                //    cell.upgradeArrowPref,
                //    cell.transform,
                //    false);

                //arrow
                //    .GetComponent<OnClickCollider>()
                //    .AddListener(() =>
                //    {
                //        // make sure the building is still there
                //        if (!cell.building) return;

                //        if (
                //            // make sure max level not reached
                //            cell.building.CanUpgrade() &&
                //            // make sure there is enough mana
                //            mana.currentMana >= cell.building.GetUpgradeManaCost()
                //        )
                //        {
                //            // send the request to GM
                //            Request(
                //                new UpgradeBuildingRequest
                //                {
                //                    coord = cell.gridPos,
                //                    currentLevel = cell.building.level,
                //                });
                //        }
                //    });
                //cell.popups.Add(arrow);
            }
        }
        ResetLastActiveCell(cell);
    }

    public void OnMouseExitOnCell(Cell cell)
    {
        // reset the ignored cell
        StopIgnoringCell();

        if (selectedAction.set)
        {
            selectedAction.tile.OnMouseExitOnCell(cell);
        }

        foreach (var p in cell.popups)
        {
            Destroy(p);
        }
        cell.popups.Clear();
    }

    public void OnMouseDraggedOntoCell(Cell cell)
    {
        // ignore the event if the right mouse button has been clicked
        // or is being held down
        if (IsIgnoring(cell)) return;

        if (selectedAction.set)
        {
            // fire the necessary event on tile
            selectedAction.tile.OnMouseDraggedOntoCell(cell);
        }

        lastActiveCell = cell;
    }

    public void OnMouseDraggedOutOfCell(Cell cell)
    {
        // ignore the event if the right mouse button has been clicked
        // or is being held down
        if (IsIgnoring(cell)) return;
        if (phase == GamePhase.Terraforming)
        {
            TerraformCell(cell);
        }
        else if (selectedAction.set)
        {
            selectedAction.tile.OnMouseDraggedOutOfCell(cell);
        }
        ResetLastActiveCell(cell);

    }

    public void OnMouseDraggedOnCell(Cell cell)
    {
        // ignore the event if the right mouse button has been clicked
        // or is being held down
        if (IsIgnoring(cell)) return;

        if (selectedAction.set && phase != GamePhase.Terraforming)
        {
            selectedAction.tile.OnMouseDraggedOnCell(cell);
        }

        ResetLastActiveCell(cell);
    }
       
    public void OnMouseButtonUpOnCell(Cell cell)
    {
        // ignore the event if the right mouse button has been clicked
        // or is being held down
        if (IsIgnoring(cell)) return;

        if (phase == GamePhase.Terraforming)
        {
            TerraformCell(cell);           
        }
        else if (selectedAction.set)
        {
            // fire the necessary event on tile
            selectedAction.tile.OnMouseButtonUpOnCell(cell);
        }
        ResetLastActiveCell(cell);
    }

    public void OnMouseButtonDownOnCell(Cell cell)
    {
        // ignore the event if the right mouse button has been clicked
        // or is being held down
        if (IsIgnoring(cell)) return;

        // 
        if (selectedAction.set && phase != GamePhase.Terraforming)
        {
            selectedAction.tile.OnMouseButtonDownOnCell(cell);
        }
        ResetLastActiveCell(cell);
    }


    public SpellBase FindSpell(SpellName name)
    {
        if (spells.ContainsKey(name))
        {
            return spells[name];
        }
        return null;
    }

    public IlluminateTwinSpell GetIlluminateTwinSpell()
    {
        return (IlluminateTwinSpell)FindSpell(SpellName.Illuminate);
    }

    public IlluminateSpell GetIlluminateSpell()
    {
        return (IlluminateSpell)GetIlluminateTwinSpell().spell;
    }


    public IlluminateBuff GetIlluminateBuffSpell()
    {
        return (IlluminateBuff)GetIlluminateTwinSpell().buffSpell;
    }

    //public TwinSpell FindTwinSpell(TwinSpellName name)
    //{
    //    if (twinSpells.ContainsKey(name))
    //    {
    //        return twinSpell[name];
    //    }
    //    return null;
    //}

    public void SyncGamePhase()
    {
        if (turn > 1 && turn <= 30)
            phase = GamePhase.Building;
        else if (turn > 30)
            phase = GamePhase.Apocalypse;
    }
}