using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public static int EnemiesAlive;
    public List<WaveData> LevelWaveData;

    [SerializeField] private GameObject waveStarterButton;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Health playerHealth;
    [SerializeField] private TextMeshProUGUI wavesCompletedText;
    [SerializeField] private int MaxWaveCount; // Set in Unity for each Level
    private bool hasAllWavesFinished = false;
    private int wavesCompletedCount = 0;

    private void Awake()
    {
        waveStarterButton.SetActive(true);
        EnemiesAlive = 0;
    }

    void Update()
    {
        wavesCompletedText.text = "Waves Completed: " + wavesCompletedCount + "/" + MaxWaveCount;
        if(hasAllWavesFinished && EnemiesAlive <= 0 || playerHealth.CurrentHealth <= 0)
        {
            gameManager.LevelConcluded();
        }
    }

    /// <summary>
    /// On Pressing "Press Here To Start Wave" button, Begin Waves
    /// </summary>
    public void BeginWaves()
    {
        StartLevel();
        Time.timeScale = 1f;
        waveStarterButton.SetActive(false);
    }

    /// <summary>
    /// Called in BeginWave to start spawning in enemies
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
            yield return new WaitForSeconds(currentWave.TimeBeforeWave);
            foreach (SpawnData currentEnemyToSpawn in currentWave.EnemyData)
            {
                yield return new WaitForSeconds(currentEnemyToSpawn.TimeBeforeSpawn);
                SpawnEnemy(currentEnemyToSpawn.EnemyToSpawn, currentEnemyToSpawn.SpawnPoint, currentEnemyToSpawn.EndPoint);
            }
            wavesCompletedCount++;
        }
        hasAllWavesFinished = true;
    }

    /// <summary>
    /// Handles spawning the enemy into the world and assigning its endPoint
    /// </summary>
    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint, Transform endPoint)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Initialized(endPoint);
        EnemiesAlive++; // To keep track how many enemies spawn in
    }
}
