using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct SpawnData
{
    public GameObject EnemyToSpawn;
    public float TimeBeforeSpawn;
    public Transform SpawnPoint;
    public Transform EndPoint;
}

[System.Serializable]
public struct WaveData
{
    public float TimeBeforeWave;
    public List<SpawnData> EnemyData;
}


public class WaveManager : MonoBehaviour
{
    public List<WaveData> LevelWaveData;
    public bool HasWaveStarted;

    [SerializeField] private GameObject waveStarterButton;

    void Update()
    {
        if(HasWaveStarted)
        {
            StartLevel();
            waveStarterButton.SetActive(false);
            HasWaveStarted = false;
        }
    }

    /// <summary>
    /// Time before the Wave starts
    /// </summary>
    public void StartLevel()
    {
        StartCoroutine(WaveStarted());
    }

    /// <summary>
    /// Spawn a certain amount of enemies at specific times
    /// </summary>
    IEnumerator WaveStarted()
    {
        foreach (WaveData currentWave in LevelWaveData)
        {
            foreach (SpawnData currentEnemyToSpawn in currentWave.EnemyData)
            {
                yield return new WaitForSeconds(currentEnemyToSpawn.TimeBeforeSpawn);
                SpawnEnemy(currentEnemyToSpawn.EnemyToSpawn, currentEnemyToSpawn.SpawnPoint, currentEnemyToSpawn.EndPoint);
            }
        }
    }

    /// <summary>
    /// Handles spawning the enemy into the world and assigning its endPoint
    /// </summary>
    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint, Transform endPoint)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Initialized(endPoint);
    }

    /// <summary>
    /// On Pressing "Press Here To Start Wave" button, Begin Wave
    /// </summary>
    public void BeginWave()
    {
        HasWaveStarted = true;
    }
}
