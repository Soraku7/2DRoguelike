using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        
        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }
        
        private void Start()
        {
            Application.targetFrameRate = 60;
            SetGameState(GameState.MENU);
        }

        public void StartGame()
        {
            SetGameState(GameState.GAME);
        }
        
        public void StartShop()
        {
            SetGameState(GameState.SHOP);
        }
        
        public void StartWeaponSelection()
        {
            SetGameState(GameState.WEAPONSELECTION);
        }

        public void SetGameState(GameState gameState)
        {
            IEnumerable<IGameStateListener> gameStateListeners =
                FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameStateListener>();

            foreach (var gameStateListener in gameStateListeners)
            {
                gameStateListener.GameStateChangedCallback(gameState);
            }
        }
        
        public void WaveCompleteCallback()
        {
            if (Player.instance.HasLevelUp() || WaveTransitionManager.instance.HasCollectedChest())
            {
                SetGameState(GameState.WAVETRANSITION);
            }
            else
            {
                SetGameState(GameState.SHOP);
            }
        }

        public void ManageGameover()
        {
            
            float timer = 0;

            SceneManager.LoadScene(0);
        }
    }
}

public interface IGameStateListener
{
    void GameStateChangedCallback(GameState gameState);
}