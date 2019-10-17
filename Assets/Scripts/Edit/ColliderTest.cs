using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{

    public SpriteRenderer chang;
    Material defaultMaterial;
    Material hoverMaterial;

    void Start()
    {
        chang = GetComponentInChildren<SpriteRenderer>();
        if (chang == null)
        {
            Debug.Log($"Chang == null at \"{gameObject.name}\"");
        }
        defaultMaterial = chang.material;
        hoverMaterial = Resources.Load<Material>("Materials\\Test4");
    }

    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        //Debug.Log("Mouse is over GameObject.");
        chang.material = hoverMaterial;
    }

    public void AutoHover()
    {
        chang.material = hoverMaterial;
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        //Debug.Log("Mouse is no longer on GameObject.");
        chang.material = defaultMaterial;
    }

    void OnMouseDown()
    {
        //Debug.Log("Click");
    }
}
