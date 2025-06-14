using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class WaveTransitionManager : MonoBehaviour , IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Button[] upgradeContainers;
    
    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WEAPONSELECTION:
                ConfigureUpgradeContainers();
                break;
        }
    }
    
    private void ConfigureUpgradeContainers()
    {
        for(int i = 0; i < upgradeContainers.Length; i++)
        {
            string randomStatString = "";
            int randomStatIndex = Random.Range(1, Enum.GetValues(typeof(Stat)).Length);
            Stat stat = (Stat)Enum.GetValues(typeof(Stat)).GetValue(randomStatIndex);
            
            randomStatString = Enums.FormatStat(stat) ;
            
            upgradeContainers[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = randomStatString;
            
            upgradeContainers[i].onClick.RemoveAllListeners();
            upgradeContainers[i].onClick.AddListener(() => Debug.Log(randomStatString));
        }
    }
}
