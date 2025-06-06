using System;
using UnityEngine;

public class Chest : MonoBehaviour , ICollectable
{
    [Header("Action")]
    public static Action onCollect;

    public void Collect(Player player)
    {
        onCollect?.Invoke();
        Debug.Log("宝箱删除");
        Destroy(gameObject);
    }
}