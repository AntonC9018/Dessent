using System;
using System.Collections.Generic;
using UnityEngine;


public class OnClickCollider : MonoBehaviour
{
    private List<Action> callbacks = new List<Action>();

    public void AddListener(Action callback)
    {
        callbacks.Add(callback);
    }

    void OnMouseUp()
    {
        foreach(Action a in callbacks)
        {
            a();
        }
    }
}