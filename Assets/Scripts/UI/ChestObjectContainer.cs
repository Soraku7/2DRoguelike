using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestObjectContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    
    [Header("Stats")]
    [SerializeField] private Transform statsContainerParent;
    [SerializeField] private StatContainer statContainerPrefab;
    
    [field: SerializeField] public Button TakeButton { get ; private set;}
    [field: SerializeField] public Button RecycleButton { get ; private set;}
    
    [Header("Color")]
    [SerializeField] private Image[] levelDependentImages;
    [SerializeField] private Image outline;


    public void Configure(ObjectDataSO objectData)
    {
        icon.sprite = objectData.Icon;
        nameText.text = objectData.Name;

        Color imageColor;
        imageColor = ColorHolder.GetColor(objectData.Rarity);
        nameText.color = imageColor;
        
        outline.color = ColorHolder.GetOutlineColor(objectData.Rarity);

        foreach (var image in levelDependentImages)
        {
            image.color = imageColor;
        }
        
        ConfigureStatContainers(objectData.BaseStats);
    }

    private void ConfigureStatContainers(Dictionary<Stat, float> stats)
    {
        StatContainerManager.GenerateStatContainer(stats , statsContainerParent);
    }

}
