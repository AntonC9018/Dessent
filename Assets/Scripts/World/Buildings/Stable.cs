using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stable : Building
{
    // TODO: Add animation / image to this object
    public static int capacityPerLevel = 1;

    public int capacity
    {
        get { return capacity; }
        set { return; }
    }

    void Start() {
        type = BuildingName.Stable;
        capacity = 1;
    }

    public override void Upgrade()
    {
        base.Upgrade();
        this.capacity = capacityPerLevel * level;
    }

    public override int GetManaCapacity() {
        return capacity;
    }

}