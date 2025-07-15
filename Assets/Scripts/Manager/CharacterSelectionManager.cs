using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Transform characterButtonsParent;
    [SerializeField] private CharacterButton characterButtonPrefab;
    [SerializeField] private Image centerCharacterImage;
    
    [SerializeField] private CharacterInfoPanel characterInfoPanel;
    
    [Header("Data")]
    private CharacterDataSO[] characterDatas;
    private List<bool> unLockedStats = new List<bool>();
    
    [Header("Settings")]
    private int selectedCharacterIndex;

    private void Awake()
    {
        characterDatas = ResourcesManager.Characters;
        
        for(int i = 0 ; i < characterDatas.Length; i++)
        {
            unLockedStats.Add(false);
        }
    }

    private void Start()
    {
        characterInfoPanel.Button.onClick.RemoveAllListeners();
        characterInfoPanel.Button.onClick.AddListener(PurchaseSelectedCharacter);
        
        Initialize();
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

        if (unLockedStats[index]) characterInfoPanel.Button.interactable = false;
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
    }

}
