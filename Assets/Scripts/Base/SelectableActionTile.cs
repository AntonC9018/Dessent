using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelectableActionTile : MonoBehaviour
{
    public StateManager stateManager;

    // TODO: make cursor optional?
    public Texture2D specialCursor;
    public Vector2 hotspot;
    public CursorMode cursorMode = CursorMode.Auto;
    public GameObject actionPrefab;

    public virtual void OnMouseButtonUp()
    {
        stateManager.SetSelectedAction(this);
        ActivateCursor();
    }

    public virtual void OnMouseButtonDown() {}

    public virtual void ActivateCursor()
    {
        Cursor.SetCursor(specialCursor, hotspot, cursorMode);
    }

    public virtual void DeactivateCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    // It is optional to override these methods. 
    // These are called by StateManager on corresponding events.
    // ------------------------------------------
    // IMPORTANT: When the tile finished its function,     
    // it should call stateManager.ResetSelectedAction().
    // When the next (optional) phase in logic is reached,
    // one should call the stateManager.ProgressSelectedActionPhase().
    public virtual void OnMouseEnterOnCell(Cell cell) {}
    public virtual void OnMouseExitOnCell(Cell cell) {}
    public virtual void OnMouseDraggedOnCell(Cell cell) {}
    public virtual void OnMouseDraggedOntoCell(Cell cell) {}
    public virtual void OnMouseDraggedOutOfCell(Cell cell) {}
    public virtual void OnMouseButtonDownOnCell(Cell cell) {}
    public virtual void OnMouseButtonUpOnCell(Cell cell) {}
    public virtual void OnActionCanceled()
    {
        CancelAction();
        DeactivateCursor();
    }

    public virtual void ApplyAction(Cell cell) {}
    public abstract void CancelAction();


    // Events of this
    private bool mouseOver;

    void Update()
    {
        if (mouseOver)
        {
            if (Input.GetMouseButtonUp(0))
            {
                OnMouseButtonUp();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                OnMouseButtonDown();
            }
        }
    }

    public void OnMouseEnter()
    {
        mouseOver = true;
    }

    public void OnMouseExit()
    {
        mouseOver = false;
    }
}