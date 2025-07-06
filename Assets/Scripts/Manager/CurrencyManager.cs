using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    
    [field: SerializeField] public int Currency { get; private set; }

    [Header("Action")] 
    public static Action onUpdate;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }
    
    private void Start()
    {
        UpdateTexts();
    }

    [NaughtyAttributes.Button]
    private void Add500Currency()
    {
        AddCurrency(500);
    }

    public void AddCurrency(int amount)
    {
        Currency += amount;
        Debug.Log($"Currency added: {amount}. Total currency: {Currency}");
        UpdateTexts();
        
        onUpdate?.Invoke();
    }

    private void UpdateTexts()
    {
        CurrencyText[] texts = FindObjectsByType<CurrencyText>(FindObjectsInactive.Include , FindObjectsSortMode.None);
        
        foreach (CurrencyText text in texts)
        {
            text.UpdateText(Currency.ToString());
        }
    }

    public bool HasEnoughCurrency(int price)
    {
        return Currency >= price;
    }

    public void UseCoins(int price)
    {
        AddCurrency(-price);
        UpdateTexts();
    }
}
