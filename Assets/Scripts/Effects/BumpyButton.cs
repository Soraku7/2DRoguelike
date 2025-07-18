using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BumpyButton : MonoBehaviour, IPointerUpHandler , IPointerDownHandler , IPointerExitHandler
{
    [Header("Elements")] 
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!button.interactable) return;


        button.transform.DOScale(Vector2.one, 0.6f).SetEase(Ease.OutBack);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!button.interactable) return;
        

        button.transform.DOScale(new Vector2(1.1f , 0.9f), 0.6f).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}