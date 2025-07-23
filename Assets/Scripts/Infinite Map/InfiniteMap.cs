using System;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject mapChunkPrefab;

    [Header("Settings")] 
    [SerializeField] private float mapChunkSize;

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        for (int i = -1; i <= 1; i++)
        {
            for(int j = -1 ; j <= 1; j++)
            {
                GenerateMapChunk(i , j);
            }
        }
    }

    private void GenerateMapChunk(int x, int y)
    {
        Vector3 spawnPosition = new Vector3(x , y , 0) * mapChunkSize;  
        Instantiate(mapChunkPrefab , spawnPosition, Quaternion.identity, transform);
    }
}
