using System;
using System.Collections.Generic;
using System.Linq;
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
            if (Player.instance.HasLevelUp())
            {
                SetGameState(GameState.WAVETRANSITION);
            }
            else
            {
                 SetGameState(GameState.SHOP);
            }
        }
    }
}

public interface IGameStateListener
{
    void GameStateChangedCallback(GameState gameState);
}