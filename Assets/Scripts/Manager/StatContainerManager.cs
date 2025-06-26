using System.Collections.Generic;
using UnityEngine;

public class StatContainerManager : MonoBehaviour
{
    public static StatContainerManager instance;
    
    [Header("Elements")]
    [SerializeField] private StatContainer statContainer;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void GenerateContainer(Dictionary<Stat, float> statsDictionary , Transform parent)
    {
        List<StatContainer> statContainers = new List<StatContainer>();
           
        foreach (var kvp in statsDictionary)
        {
            StatContainer containerInstance = Instantiate(statContainer, parent);
            
            statContainers.Add(containerInstance);
            
            containerInstance.Configure(ResourcesManager.GetStatIcon(kvp.Key),
                Enums.FormatStat(kvp.Key),
                kvp.Value);
        }

        ResizeTexts(statContainers);
    }

    private void ResizeTexts(List<StatContainer> statContainers)
    {
        float minFontSize = 5000;
        
        for(int i = 0 ; i < statContainers.Count ; i++)
        {
            var statContainer = statContainers[i];
            float fontSize = statContainer.GetFontSize();

            if (fontSize < minFontSize)
            {
                minFontSize = fontSize;
            }
        }

        for (int i = 0; i < statContainers.Count; i++)
        {
            statContainers[i].SetFontSize(minFontSize);
        }
    }

    public static void GenerateStatContainer(Dictionary<Stat, float> statsDictionary , Transform parent)
    {
        instance.GenerateContainer(statsDictionary , parent);
    }

}
