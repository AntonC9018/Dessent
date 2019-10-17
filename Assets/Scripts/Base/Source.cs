using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Source 
{
    private string type;
    public List<Effect> effects = new List<Effect>();
}


public class DamageSource : Source
{

    public int damage 
    {
        get { return damage; }
        set { damage = value; }
    }

    public DamageSource(int damage)
    {
        this.damage = damage;
    }

}

public class ReligionSource : Source
{

    public ReligionSource() { }

}
