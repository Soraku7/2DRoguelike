using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    
    [field: SerializeField] public int Currency { get; private set; }
    [field: SerializeField] public int PremiumCurrency { get; private set; }

    [Header("Action")] 
    public static Action onUpdate;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
        
        Candy.onCollection += CandyCollectedCallback;
        Cash.onCollection += CashCollectedCallback;
    }


    private void Start()
    {
        UpdateTexts();
    }
    
    private void OnDestroy()
    {
        Candy.onCollection -= CandyCollectedCallback;
        Cash.onCollection -= CashCollectedCallback;
    }

    private void CashCollectedCallback(Cash obj)
    {
        AddPremiumCurrency(1);
    }
    
    private void CandyCollectedCallback(Candy candy)
    {
        AddCurrency(1);
    }


    [NaughtyAttributes.Button]
    private void Add500Currency()
    {
        AddCurrency(500);
    }
    
    [NaughtyAttributes.Button]
    private void Add500PremiumCurrency()
    {
        AddPremiumCurrency(500);
    }

    public void AddCurrency(int amount)
    {
        Currency += amount;
        Debug.Log($"Currency added: {amount}. Total currency: {Currency}");
        UpdateVisual();
    }
    
    public void AddPremiumCurrency(int amount)
    {
        PremiumCurrency += amount;
        Debug.Log($"Premium currency added: {amount}. Total premium currency: {PremiumCurrency}");
        UpdateVisual();
    }

    private void UpdateVisual()
    {
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
        
        PremunCurrencyText[] premiumCurrencyTexts = FindObjectsByType<PremunCurrencyText>(FindObjectsInactive.Include , FindObjectsSortMode.None);
        
        foreach (PremunCurrencyText text in premiumCurrencyTexts)
        {
            text.UpdateText(PremiumCurrency.ToString());
        }
    }

    public bool HasEnoughCurrency(int price)
    {
        return Currency >= price;
    }
    
    public bool HasEnoughPremiumCurrency(int price)
    {
        return PremiumCurrency >= price;
    }

    public void UseCurrency(int price)
    {
        AddCurrency(-price);
        UpdateTexts();
    }
    
    public void UsePremiumCurrency(int price)
    {
        AddPremiumCurrency(-price);
        UpdateTexts();
    }
}