using UnityEngine;

public static class ResourcesManager
{
    private const string statIconsDataPath = "Data/Stat Icons";
    private const string objectsDataPath = "Data/Objects/";
    
    private static StatIcon[] statIcons;
    public static Sprite GetStatIcon(Stat stat)
    {
        if (statIcons == null)
        {
            StatIconSo data = Resources.Load<StatIconSo>(statIconsDataPath);
            if (data != null)
            {
                statIcons = data.StatIcons;
            }
            else
            {
                Debug.LogError($"Failed to load StatIconSo from path: {statIconsDataPath}");
                return null;
            }
        }

        foreach (var statIcon in statIcons)
        {
            if(stat == statIcon.stat) return statIcon.icon;
        }
        
        return null;
    }

    private static ObjectDataSO[] objectDatas;
    public static ObjectDataSO[] Objects
    {
        get
        {
            if(objectDatas == null)
            {
                objectDatas = Resources.LoadAll<ObjectDataSO>(objectsDataPath); 
            }
            return objectDatas;
        }
        private set
        {
            
        }
    }
}
