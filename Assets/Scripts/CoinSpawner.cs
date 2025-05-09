using UnityEngine;
using System.Collections.Generic;

public class CoinSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject coinPrefab;
    public Transform player;

    [Header("Spawn Settings")]
    public float firstSpawnDistance = 20f;
    public float spawnInterval = 15f;
    public float laneWidth = 3f;
    public int maxCoinsPerRow = 2;
    public float minGapBetweenGroups = 5f;
    public float maxGapBetweenGroups = 10f;
    float horizontalOffset = 0.3f;

    [Header("Appearance")]
    [Range(0, 100)] public int coinGroupChance = 70;
    [Range(0, 100)] public int singleLaneChance = 60;
    [Range(0, 100)] public int doubleLaneChance = 30;

    [Header("Arc")]
    public bool allowArcs = true;
    public float arcHeight = 2.5f;
    public int arcCoinCount = 6;

    private float nextSpawn;
    private float nextGapLength;

    void Start()
    {
        nextSpawn = player.position.z + firstSpawnDistance;
        CalculateNextGap();
    }

    void Update()
    {
        if (player.position.z > nextSpawn - (spawnInterval * 2))
        {
            if (SpawnGroup())
            {
                SpawnCoinGroup();
            }
            else
            {
                nextSpawn += nextGapLength;
                CalculateNextGap();
            }
        }
    }

    bool SpawnGroup()
    {
        return Random.Range(0, 100) < coinGroupChance;
    }

    void SpawnCoinGroup()
    {
        int groupType = Random.Range(0, 100);
        int rowsInGroup = Random.Range(3, 7);
        
        if (groupType < singleLaneChance)
        {
            int lane = Random.Range(-1, 2);
            for (int i = 0; i < rowsInGroup; i++)
            {
                SpawnCoin(lane, nextSpawn + (i * 2f));
            }
        }
        else if (groupType < singleLaneChance + doubleLaneChance)
        {
            int mainLane = Random.Range(-1, 2);
            int secondLane = (mainLane + Random.Range(1, 3)) % 3 - 1;
            
            for (int i = 0; i < rowsInGroup; i++)
            {
                if (Random.value > 0.3f) SpawnCoin(mainLane, nextSpawn + (i * 2f));
                if (Random.value > 0.3f) SpawnCoin(secondLane, nextSpawn + (i * 2f));
            }
        }
        else
        {
            if (allowArcs && Random.value < 1.0f)
            {
                int lane = Random.Range(-1, 2);
                SpawnCoinArc(lane, nextSpawn);
                nextSpawn += spawnInterval;
            }
            else
            {
                for (int i = 0; i < rowsInGroup; i++)
                {
                    if (Random.value > 0.4f) SpawnCoin(-1, nextSpawn + (i * 2f));
                    if (Random.value > 0.4f) SpawnCoin(0, nextSpawn + (i * 2f));
                    if (Random.value > 0.4f) SpawnCoin(1, nextSpawn + (i * 2f));
                }
                nextSpawn += rowsInGroup * 2f;
            }
        }
        CalculateNextGap();
    }

    void SpawnCoin(int lane, float zPos)
    {
        Vector3 spawnPos = new Vector3(
            (lane * laneWidth) + horizontalOffset,
            player.position.y,
            zPos
        );
        Instantiate(coinPrefab, spawnPos, Quaternion.identity);
    }

    void CalculateNextGap()
    {
        nextGapLength = Random.Range(minGapBetweenGroups, maxGapBetweenGroups);
        nextSpawn += nextGapLength;
    }

    //Spawn coins in an arc shape

        void SpawnCoinArc(int lane, float startZ)
    {
        for (int i = 0; i < arcCoinCount; i++)
        {
            float t = i / (float)(arcCoinCount - 1); 
            float zPos = startZ + t * 5f; 
            float yPos = Mathf.Sin(t * Mathf.PI) * arcHeight + player.position.y; // sine wave
            Vector3 spawnPos = new Vector3((lane * laneWidth) + horizontalOffset, yPos, zPos);
            Instantiate(coinPrefab, spawnPos, Quaternion.identity);
        }
    }
}