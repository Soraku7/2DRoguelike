using System;
using System.Collections.Generic;
using Tabsil.Sijil;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour , IWantToBeSaved
{
    [Header("Elements")] 
    [SerializeField] private Transform characterButtonsParent;
    [SerializeField] private CharacterButton characterButtonPrefab;
    [SerializeField] private Image centerCharacterImage;
    
    [SerializeField] private CharacterInfoPanel characterInfoPanel;
    
    [Header("Data")]
    private CharacterDataSO[] characterDatas;
    private List<bool> unLockedStats = new List<bool>();
    private const string unlockedStatsKey = "UnlockedStatsKey";
    private const string lastSelectedCharacterKey = "LastSelectedCharacterKey";
    
    [Header("Settings")]
    private int selectedCharacterIndex;
    private int lastSelectedCharacterIndex;
    
    [Header("Action")]
    public static Action<CharacterDataSO> OnCharacterSelected;
    
    private void Start()
    {
        characterInfoPanel.Button.onClick.RemoveAllListeners();
        characterInfoPanel.Button.onClick.AddListener(PurchaseSelectedCharacter);

        CharacterSelectionCallback(lastSelectedCharacterIndex);
    }
    
    private void Initialize()
    {
        
        for(int i = 0 ; i < characterDatas.Length; i++)
        {
            CreateCharacterButton(i);
        }
    }

    private void CreateCharacterButton(int index)
    {
        CharacterDataSO characterData = characterDatas[index];
        
        CharacterButton characterButtonInstance = Instantiate(characterButtonPrefab, characterButtonsParent);
        characterButtonInstance.Configure(characterData.Sprite , unLockedStats[index]);
        
        characterButtonInstance.Button.onClick.RemoveAllListeners();
        characterButtonInstance.Button.onClick.AddListener(() => CharacterSelectionCallback(index));
    }

    private void CharacterSelectionCallback(int index)
    {
        selectedCharacterIndex = index;
        
        CharacterDataSO characterData = characterDatas[index];

        if (unLockedStats[index])
        {
            lastSelectedCharacterIndex = index;
            characterInfoPanel.Button.interactable = false;
            Save();
            
            OnCharacterSelected?.Invoke(characterData);
        }
        else CurrencyManager.instance.HasEnoughPremiumCurrency(characterData.PurchasePrice);
        
        centerCharacterImage.sprite = characterData.Sprite;
        characterInfoPanel.Configure(characterData , unLockedStats[index]);
    }

    private void PurchaseSelectedCharacter()
    {
        int price = characterDatas[selectedCharacterIndex].PurchasePrice;
        CurrencyManager.instance.UsePremiumCurrency(price);
        
        unLockedStats[selectedCharacterIndex] = true;
        
        characterButtonsParent.GetChild(selectedCharacterIndex).GetComponent<CharacterButton>().UnLock();
        
        CharacterSelectionCallback(selectedCharacterIndex);
        
        Save();
    }

    public void Load()
    {
        characterDatas = ResourcesManager.Characters;
        
        for(int i = 0 ; i < characterDatas.Length; i++)
        {
            unLockedStats.Add(false);
        }
        
        if(Sijil.TryLoad(this , unlockedStatsKey , out object unlockedStatsObject)) unLockedStats = (List<bool>)unlockedStatsObject;
        if (Sijil.TryLoad(this, lastSelectedCharacterKey, out object lastSelectedCharacterObject))
            lastSelectedCharacterIndex = (int)lastSelectedCharacterObject;
        
        Initialize();
        

    }

    public void Save()
    {
       Sijil.Save(this , unlockedStatsKey , unLockedStats);
       Sijil.Save(this , lastSelectedCharacterKey , lastSelectedCharacterIndex);
    }
    
}