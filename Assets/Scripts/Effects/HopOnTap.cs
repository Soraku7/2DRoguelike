using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class HopOnTap : MonoBehaviour , IPointerDownHandler
{
    private RectTransform rt;
    private Vector2 initialPosition;
    
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    private void Start()
    {
        initialPosition = rt.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        float targetY = initialPosition.y + Screen.height / 50f;
        
        rt.DOPunchPosition(new Vector3(0, targetY, 0), 0.5f , 1 , 0);
    }
}
