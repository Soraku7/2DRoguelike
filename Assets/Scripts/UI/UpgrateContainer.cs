using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgrateContainer : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private TextMeshProUGUI upgradeVauleText;

    [field: SerializeField]public Button Button
    {
        get;
        private set;
    }

    public void Configure(Sprite icon, string upgradeName, string upgradeVaule)
    {
        image.sprite = icon;
        upgradeNameText.text = upgradeName;
        upgradeVauleText.text = upgradeVaule;
    }

}
