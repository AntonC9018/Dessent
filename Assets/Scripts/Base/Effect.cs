using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this might be, for example, the effect of illumination
public abstract class Effect
{
    public virtual void Apply
        (MonoBehaviour caller, Source source) {}
}
