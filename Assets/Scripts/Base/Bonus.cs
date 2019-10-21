using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{

    public BonusName type;

    // should respond to the event of being touched
    // must call selfDestruct
    public virtual void OnTouch(
        // some means of game state
    ) {
        // grant some bonus to the cell or to the player
        // selfDestruct()
        print("Touched bonus");
    }

    public void SelfDestruct()
    {
        // delete itself from bonus array
        Destroy(gameObject);
    }

    public static List<BonusStruct> ConvertToStructs(List<Bonus> bonuses)
    {
        var result = new List<BonusStruct>();
        foreach (var b in bonuses)
        {
            result.Add(b.ConvertToStruct());
        }

        return result;
    }

    public BonusStruct ConvertToStruct()
    {
        return new BonusStruct
        {
            name = type,
        };
    }

}




public struct BonusStruct
{
    public BonusName name;
}
