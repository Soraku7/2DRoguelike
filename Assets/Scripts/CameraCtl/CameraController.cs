using System;
using Manager;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector2 minMaxXY;
    private void LateUpdate()
    {

        if (target == null)
        {
            Debug.LogWarning("loss target");

            return;
        }
        
        Vector3 targetPos = target.position;
        targetPos.z = -10;
        
        if(!GameManager.instance.UseInfiniteMap)
        {
            targetPos.x = Mathf.Clamp(targetPos.x, -minMaxXY.x, minMaxXY.x);
            targetPos.y = Mathf.Clamp(targetPos.y, -minMaxXY.y, minMaxXY.y);
        }

        transform.position = targetPos;
    }
}
