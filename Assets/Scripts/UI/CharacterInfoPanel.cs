using TMPro;
using UnityEngine;

public class CharacterInfoPanel : MonoBehaviour
{
     [Header("Elements")]
     [SerializeField] private TextMeshProUGUI nameText;
     [SerializeField] private TextMeshProUGUI priceText;
     [SerializeField] private GameObject priceContainer;
     [SerializeField] private Transform statsParent;

     public void Configure(CharacterDataSO characterData , bool unlocked)
     {
          nameText.text = characterData.Name;
          priceText.text = characterData.PurchasePrice.ToString();
          
          priceContainer.SetActive(!unlocked);
          
          StatContainerManager.GenerateStatContainer(characterData.NonNeutralStats , statsParent);
     }
}
