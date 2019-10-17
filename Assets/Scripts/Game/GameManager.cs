using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class GameManager : MonoBehaviour
{
    public abstract StateManager GetOpponent(StateManager s);
    public abstract void Request(StateManager from, Request req);
    public abstract void Respond(StateManager s, Response res);
    
    public WhichPlayer activePlayer;
    
    public void SwitchPlayer()
    {
        if (activePlayer == WhichPlayer.Player1)
        {
            transform.localPosition += Vector3.up * 1000;
            activePlayer = WhichPlayer.Player2;
        }
        else if (activePlayer == WhichPlayer.Player2)
        {
            transform.localPosition += Vector3.down * 1000;
            activePlayer = WhichPlayer.Player1;
        }
    }

    private void Start()
    {
        activePlayer = WhichPlayer.Player1;
    }

}
