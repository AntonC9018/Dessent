using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    public static GameObject waterPref, grasslandPref, mountainPref, voidPref;
    public GroundName altitude;


    public static Ground Create(GroundName a, Cell parentCell)
    {
        GameObject pref;
        if (a == GroundName.Grassland)
        {
            pref = grasslandPref;
        }
        else if (a == GroundName.Sea)
        {
            pref = waterPref;
        }
        else if (a == GroundName.Mountain)
        {
            pref = mountainPref;
        }
        else
        {
            pref = voidPref;
        }

        GameObject parent = parentCell.gameObject;

        GameObject ground = Instantiate(
            pref,
            parent.transform.position,
            Quaternion.identity,
            parent.transform.parent);

        Ground groundScript = ground.GetComponent<Ground>();

        groundScript.altitude = a;

        return groundScript;
    }

}
