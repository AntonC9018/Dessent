using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerGameManager : GameManager
{
    public StateManager[] stateManagers = new StateManager[2];
    private StateManager activePlayer;

    private int turnCount = 0;

    void Start()
    {
        activePlayer = stateManagers[0];
    }

    override public StateManager GetOpponent(StateManager s)
    {
        if (s == stateManagers[0]) return stateManagers[1];
        return stateManagers[0];
    }

    public StateManager GetOpponent(int id)
    {
        return stateManagers[id == 0 ? 1 : 0];
    }

    override public void Request(StateManager from, Request request)
    {

        // Ignore requests that do not come from 
        // the currently active player
        if (from != activePlayer) return;

        // redirect the request to the opponent
        StateManager opponent = GetOpponent(from);

        // here is where the logic happens 2
        switch (request.headerName)
        {

            case HeaderName.ApplySpell:
                {
                    var req = (ApplySpellRequest)request;
                    Cell cell = opponent.publicGrid.GetCellAt(req.coord);
                    if (cell != null)
                    {
                        SpellBase spell = opponent.FindSpell(req.name);
                        if (spell is TwinSpell)
                        {
                            ((TwinSpell)spell).spell.Apply(cell, opponent);
                        }
                        else
                        {
                            ((Spell)spell).Apply(cell, opponent);
                        }
                    }
                    break;
                }

            case HeaderName.ApplyBuff:
                {
                    var req = (ApplyBuffRequest)request;

                    BuffSpell buffSpell;
                    SpellBase spell = opponent.FindSpell(req.name);
                    if (spell is TwinSpell)
                    {
                        buffSpell = ((TwinSpell)spell).buffSpell;
                    }
                    else
                    {
                        buffSpell = (BuffSpell)spell;
                    }

                    var response = buffSpell.GenerateResponse(req, from, opponent);

                    if (response != null)
                    {
                        from.ReceiveResponse(response);
                    }

                    var packet = buffSpell.GeneratePacket(req, from, opponent);

                    if (packet != null)
                    {
                        opponent.ReceivePacket(packet);
                    }

                    break;
                }

            case HeaderName.ConstructBuilding:
                {
                    // check enemy's vision 
                    // if they have it, send a packet
                    // otherwise, don't do anything

                    // NOTE: for the server-ful multiplayer implementation
                    // this should include error checking (validation) at server
                    var req = (ConstructBuildingRequest)request;
                    Cell cell = opponent.privateGrid.GetCellAt(req.coord);

                    var res = new ConstructBuildingResponse
                    {
                        coord = req.coord,
                        type = req.type,
                        illuminate = new ApplyIlluminateBuffResponse
                        {
                            // respond with the list of bonuses that the tile has
                            bonuses = Bonus.ConvertToStructs(cell.bonuses),
                            coord = req.coord,
                        }
                    };

                    from.ReceiveResponse(res);
                    break;
                }

            case HeaderName.EndTurn:
                {

                    NextTurn();

                    var res = new EndTurnResponse
                    {
                        turnCount = turnCount,
                    };

                    from.ReceiveResponse(res);

                    var pack = new StartTurnPacket
                    {
                        turnCount = turnCount,
                    };

                    opponent.ReceivePacket(pack);


                    break;
                }
        }

        // the opponent will call the Respond() method 
        // afterwards to send the response

    }

    override public void Respond(StateManager from, Response res)
    {
        // redirect the response to the opponent
        StateManager opponent = GetOpponent(from);
        opponent.ReceiveResponse(res);
    }

    private void NextTurn()
    {
        turnCount += 1;
        // pass the turn
        activePlayer = GetOpponent(activePlayer);
        // move to the second player's board
        Camera.main.transform.position = new Vector3(
            activePlayer.transform.position.x,
            activePlayer.transform.position.y,
            Camera.main.transform.position.z);
    }

}