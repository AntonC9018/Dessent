using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicGrid : HexGrid
{
    PublicGrid() {}
    override public bool IsPublic()
    {
        return true;
    }
}