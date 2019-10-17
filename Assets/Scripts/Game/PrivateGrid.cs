using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateGrid : HexGrid
{
    override public bool IsPublic()
    {
        return false;
    }
}