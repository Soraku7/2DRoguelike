using System;
using System.Collections;
using UnityEngine;

public class Cash : DroppableCurrency
{
    [Header("Action")]
    public static Action<Cash> onCollection;
    
    protected override void Collected()
    {
        onCollection?.Invoke(this);
    }
}
