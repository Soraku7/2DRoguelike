using System;
using System.Collections;
using UnityEngine;

public class Candy : DroppableCurrency
{
    [Header("Action")]
    public static Action<Candy> onCollection;
    
    protected override void Collected()
    {
       onCollection?.Invoke(this);
    }
}
