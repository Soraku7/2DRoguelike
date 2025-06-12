using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour , IGameStateListener
{
    [Header("Panel")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject waveTransitionPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject menuPanel;

    private List<GameObject> panels = new List<GameObject>();

    private void Awake()
    {
        panels.AddRange(new GameObject[]
        {
            shopPanel,
            waveTransitionPanel,
            gamePanel,
            menuPanel
        });
    }
    
    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAME:
                ShowPannel(gamePanel);
                break;
            
            case GameState.WAVETRANSITION:
                ShowPannel(waveTransitionPanel);
                break;
            
            case GameState.SHOP:
                ShowPannel(shopPanel);
                break;
            
            case GameState.MENU:
                ShowPannel(menuPanel);
                break;
        }
    }
    
    private void ShowPannel(GameObject panel , bool hidePreviousPanel  = true)
    {
        if(hidePreviousPanel)
        {
            foreach (var p in panels)
            {
                p.SetActive(p == panel);
            }
        }
        else
        {
            panel.SetActive(true);
        }
    }
}
