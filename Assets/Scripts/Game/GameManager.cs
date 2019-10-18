using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class GameManager : MonoBehaviour
{
    public abstract StateManager GetOpponent(StateManager s);
    public abstract void Request(StateManager from, Request req);
    public abstract void Respond(StateManager s, Response res);

}
