using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float shakeMagnitude;
    [SerializeField] private float shakeFrequency;

    private void Awake()
    {
        RangeWeapon.onBulletShot += Shake;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Shake();
        }
    }
    
    private void OnDestroy()
    {
        RangeWeapon.onBulletShot -= Shake;
    }

    [NaughtyAttributes.Button]
    private void Shake()
    {
        Vector2 direction = Random.onUnitSphere.With(z : 0f).normalized;

        transform.localPosition = Vector3.zero;
        
        transform.DOLocalMove(direction * shakeMagnitude , shakeFrequency)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => transform.localPosition = Vector3.zero);
    }
}
