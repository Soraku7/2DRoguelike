using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float waveDuration;
    private float timer;
    
    [Header("Waves")]
    [SerializeField] private Wave[] waves;
    private List<float> localCounters = new List<float>();

    private void Start()
    {
        localCounters.Add(1);
    }
    
    private void Update()
    {
        if (timer < waveDuration) ManageCurrentWave();
    }

    private void ManageCurrentWave()
    {
        Wave currentWave = waves[0];

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
                Instantiate(currentSegment.prefab , Vector2.zero , Quaternion.identity , transform);
                localCounters[i]++;
            } 
        }
        
        timer += Time.deltaTime;
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
}