using System;
using System.Collections.Generic;
using Manager;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(WaveManagerUI))]
public class WaveManager : MonoBehaviour , IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Player player;
    private WaveManagerUI waveManagerUI;
    
    [Header("Settings")]
    [SerializeField] private float waveDuration;
    private float timer;
    private bool isTimeOn;
    private int currentWaveIndex;
    
    [Header("Waves")]
    [SerializeField] private Wave[] waves;
    private List<float> localCounters = new List<float>();

    private void Awake()
    {
        waveManagerUI = GetComponent<WaveManagerUI>();
    }
    

    private void Update()
    {
        if(!isTimeOn) return;

        if (timer < waveDuration)
        {
            ManageCurrentWave();

            string timeString = ((int)(waveDuration - timer)).ToString();
            waveManagerUI.UpdateTimerText(timeString);
        }
        else StartWaveTransition();
    }

    private void StartWaveTransition()
    {
        waveManagerUI.UpdateWaveText("Wave" + (currentWaveIndex + 1) + " / " + waves.Length);
        
        isTimeOn = false;
        
        currentWaveIndex++;
        
        if(currentWaveIndex >= waves.Length)
        {
            waveManagerUI.UpdateWaveText("Game Over");
            waveManagerUI.UpdateTimerText("");
            GameManager.instance.SetGameState(GameState.STAGECOMPLETE);
        }
        else
        {
            GameManager.instance.WaveCompleteCallback();
            DefeatAllEnemies();
        }
    }

    private void ManageCurrentWave()
    {
        Wave currentWave = waves[currentWaveIndex];

        for (int i = 0; i < currentWave.segments.Count; i++)
        {
            WaveSegment currentSegment = currentWave.segments[i];
            
            float tStgart = currentSegment.tStartEnd.x / 100 * waveDuration;
            float tEnd = currentSegment.tStartEnd.y / 100 * waveDuration;
            
            if(timer < tStgart || timer > tEnd) continue;
            
            float timeSinceSegementStart = timer - tStgart;
            float spawnDelay = 1 / currentSegment.spawnFrequency;
            
            if (timeSinceSegementStart / spawnDelay > localCounters[i])
            {
                Instantiate(currentSegment.prefab , GetSpawnPosition() , Quaternion.identity , transform);
                localCounters[i]++;

                if (currentSegment.spawnOnce) localCounters[i] += Mathf.Infinity;
            } 
        }
        
        timer += Time.deltaTime;
    }

    [NaughtyAttributes.Button]
    private void DefeatAllEnemies()
    {
        foreach(var enemy in transform.GetComponentsInChildren<Enemy>())
            enemy.PassAway();
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 direction = Random.onUnitSphere;
        Vector2 offset = direction.normalized * Random.Range(6, 10);
        Vector2 targetPosition = (Vector2)player.transform.position + offset;

        if(!GameManager.instance.UseInfiniteMap)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, -Constants.arenaSize.x / 2, Constants.arenaSize.x / 2);
            targetPosition.y = Mathf.Clamp(targetPosition.y, -Constants.arenaSize.y / 2, Constants.arenaSize.y / 2);
        }
        return targetPosition;
    }
    
    private void StartWave(int waveIndex)
    {
        Debug.Log("Start Wave" + currentWaveIndex);
        waveManagerUI.UpdateWaveText("Wave" + (currentWaveIndex + 1) + " / " + waves.Length);
        timer = 0;
        isTimeOn = true;
        
        localCounters.Clear();
        foreach (var waveSegment in waves[waveIndex].segments)
        {
            localCounters.Add(1);
        }
    }

    private void StartNextWave()
    {
        StartWave(currentWaveIndex);
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        Debug.Log("GameState Changed" + gameState);

        switch (gameState)
        {
            case GameState.GAME:
                StartNextWave();
                break;
            
            case GameState.GAMEOVER:
                isTimeOn = true;
                DefeatAllEnemies();
                break;
        }
    }
}


[Serializable]
public struct Wave
{
    public string name;
    public List<WaveSegment> segments;
}

[Serializable]
public struct WaveSegment
{
    [MinMaxSlider(0 , 100)] public Vector2 tStartEnd;
    public float spawnFrequency;
    public GameObject prefab;
    public bool spawnOnce;
}