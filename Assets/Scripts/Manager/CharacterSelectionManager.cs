using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Transform characterButtonsParents;
    [SerializeField] private CharacterButton characterButtonPrefab;
    [SerializeField] private Image centerCharacterImage;
    
    [SerializeField] private CharacterInfoPanel characterInfoPanel;
    
    [Header("Data")]
    private CharacterDataSO[] characterDatas;

    private void Awake()
    {
        characterDatas = ResourcesManager.Characters;
    }

    private void Start()
    {
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
        
        CharacterButton characterButtonInstance = Instantiate(characterButtonPrefab, characterButtonsParents);
        characterButtonInstance.Configure(characterData.Sprite);
        
        characterButtonInstance.Button.onClick.RemoveAllListeners();
        characterButtonInstance.Button.onClick.AddListener(() => CharacterSelectionCallback(index));
    }

    private void CharacterSelectionCallback(int index)
    {
        CharacterDataSO characterData = characterDatas[index];
        
        centerCharacterImage.sprite = characterData.Sprite;
        characterInfoPanel.Configure(characterData , false);
    }
}
