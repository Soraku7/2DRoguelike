using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

[RequireComponent(typeof(RectTransform))]
public class ScalenRotate : MonoBehaviour , IPointerDownHandler
{
    private RectTransform rt;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rt.localScale = Vector3.one;
        rt.transform.DOScale(Vector2.one * 1.1f, 1).SetEase(Ease.OutBack);
        
        rt.rotation = Quaternion.identity;
        int sign = (int)Mathf.Sign(Random.Range(-1f, 1f));
        rt.transform.DORotate(new Vector3(0, 0, sign * Random.Range(10f, 20f)), 1)
            .SetEase(Ease.OutBack).OnComplete(() =>
        {
            rt.transform.DORotate(Vector3.zero, 1).SetEase(Ease.OutBack);
        });
    }
}
