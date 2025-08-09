using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set;}
    public bool hasLevelConcluded = false;
    public Health playerHealth;

    private HighScoreManager highScoreManager;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            return;
        }
        playerHealth = GetComponent<Health>();
    }

    /// <summary>
    /// When the player has completed a level, as in, either won or lost, Check the amount of Health they have 
    /// </summary>
    public void LevelConcluded()
    {
        StopAllCoroutines();
        HighScoreManager highScoreManager = GetComponentInChildren<HighScoreManager>();
        Time.timeScale = 0f;
        hasLevelConcluded = true;
        highScoreManager.StarsAwarded();
    }
}
