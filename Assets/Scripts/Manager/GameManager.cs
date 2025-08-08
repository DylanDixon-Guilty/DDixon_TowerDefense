using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set;}
    public bool hasLevelCompleted = false;
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
    public void LevelCompleted()
    {
        StopAllCoroutines();
        Time.timeScale = 0f;
        hasLevelCompleted = true;
        highScoreManager.StarsAwarded();
    }
}
