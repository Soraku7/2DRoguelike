using System.Collections.Generic;
using Manager;
using UnityEngine;

public class UIManager : MonoBehaviour , IGameStateListener
{
    [Header("Panel")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject waveTransitionPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject weaponSelectionPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject stageCompletePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject restartConfirmationPanel;
    [SerializeField] private GameObject CharacterSelectionPanel;

    private List<GameObject> panels = new List<GameObject>();

    private void Awake()
    {
        panels.AddRange(new GameObject[]
        {
            shopPanel,
            waveTransitionPanel,
            gamePanel,
            menuPanel,
            weaponSelectionPanel,
            gameOverPanel,
            stageCompletePanel
        });
        
        
        GameManager.onGamePaused += GamePausedCallback;
        GameManager.onGameResumed += GameResumedCallback;
        
        pausePanel.SetActive(false);
        HideRestartConfirmationPanel();  
        HideCharacterSelectionPanel();
    }
    
    private void OnDestroy()
    {
        GameManager.onGamePaused -= GamePausedCallback;
        GameManager.onGameResumed -= GameResumedCallback;
    }
    
    private void GameResumedCallback()
    {
        pausePanel.SetActive(false);
    }

    private void GamePausedCallback()
    {
        pausePanel.SetActive(true);
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
            
            case GameState.WEAPONSELECTION:
                ShowPannel(weaponSelectionPanel);
                break;
            
            case GameState.GAMEOVER:
                ShowPannel(gameOverPanel);
                break;
            
            case GameState.STAGECOMPLETE:
                ShowPannel(stageCompletePanel);
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
    public void ShowRestartConfirmationPanel()
    {
        restartConfirmationPanel.SetActive(true);
    }
    
    public void HideRestartConfirmationPanel()
    {
        restartConfirmationPanel.SetActive(false);
    }
    
    public void ShowCharacterSelectionPanel()
    {
        CharacterSelectionPanel.SetActive(true);
    }
    
    public void HideCharacterSelectionPanel()
    {
        CharacterSelectionPanel.SetActive(false);
    }
}