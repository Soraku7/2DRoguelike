using System;
using System.Collections;
using UnityEngine;

public abstract class DroppableCurrency : MonoBehaviour, ICollectable
{
    private bool collected;

    private void OnEnable()
    {
        collected = false;
    }

    public void Collect(Player player)
    {
        if (collected) return;
        
        collected = true;

        StartCoroutine(MoveTowardsPlayer(player));
    }

    /// <summary>
    /// 收集物体朝向玩家移动
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    IEnumerator MoveTowardsPlayer(Player player)
    {
        float timer = 0;
        Vector3 initialPosition = transform.position;
       
        
        while (timer < 1)
        {
            Vector3 targetPosition = player.GetCenter();
            transform.position = Vector3.Lerp(initialPosition, targetPosition, timer);
            
            timer += Time.deltaTime;
            yield return null;
        }
        
        Collected();
    }

    protected abstract void Collected();
}
