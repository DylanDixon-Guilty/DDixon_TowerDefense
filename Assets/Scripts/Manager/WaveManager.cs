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
    public static int EnemiesAlive = 0;
    public int MaxWaveCount; // Set in Unity for each Level
    public List<WaveData> LevelWaveData;

    [SerializeField] private GameObject waveStarterButton;
    [SerializeField] private HighScoreManager highScoreManager;
    [SerializeField] private Health playerHealth;
    [SerializeField] private TextMeshProUGUI currentWaveText;
    private bool hasWaveFinished = false;
    private bool hasAllWavesFinished = false;
    private int currentWaveCount = 0;

    private void Awake()
    {
        waveStarterButton.SetActive(true);
        EnemiesAlive = 0;
    }

    void Update()
    {
        currentWaveText.text = "Current Wave: " + currentWaveCount + "/" + MaxWaveCount;

        if (hasAllWavesFinished && EnemiesAlive <= 0 || playerHealth.CurrentHealth <= 0)
        {
            highScoreManager.LevelCompleted();
            hasWaveFinished = false;
        }
    }

    /// <summary>
    /// On Pressing "Press Here To Start Wave" button, Begin Wave
    /// </summary>
    public void BeginWave()
    {
        StartLevel();
        waveStarterButton.SetActive(false);
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
        if(!hasWaveFinished)
        {
            foreach (WaveData currentWave in LevelWaveData)
            {
                yield return new WaitForSeconds(currentWave.TimeBeforeWave);
                foreach (SpawnData currentEnemyToSpawn in currentWave.EnemyData)
                {
                    yield return new WaitForSeconds(currentEnemyToSpawn.TimeBeforeSpawn);
                    SpawnEnemy(currentEnemyToSpawn.EnemyToSpawn, currentEnemyToSpawn.SpawnPoint, currentEnemyToSpawn.EndPoint);
                }
                hasWaveFinished = true;
                currentWaveCount++;
            }
            hasAllWavesFinished = true;
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
        EnemiesAlive++; // To keep track how many enemies spawn in
    }
}
