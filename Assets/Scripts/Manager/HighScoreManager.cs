using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    /*
       The HighScore is represented by 3 Stars. The player's remaining health when they win a Level determines how many stars they get.
       For example, if they have 19 Health remaining and win, they get 3 stars
    */

    public bool hasLevelCompleted = false;

    [SerializeField] private Health playerHealth;
    [SerializeField] private GameObject collectedStar01;
    [SerializeField] private GameObject collectedStar02;
    [SerializeField] private GameObject collectedStar03;
    [SerializeField] private GameObject uncollectedStar01;
    [SerializeField] private GameObject uncollectedStar02;
    [SerializeField] private GameObject uncollectedStar03;
    [SerializeField] private int threeStars = 18;
    [SerializeField] private int twoStars = 10;
    [SerializeField] private int oneStar = 9; 

    private void Start()
    {
        collectedStar01.SetActive(false);
        collectedStar02.SetActive(false);
        collectedStar03.SetActive(false);
        uncollectedStar01.SetActive(false);
        uncollectedStar02.SetActive(false);
        uncollectedStar03.SetActive(false);
    }

    /// <summary>
    /// When the player has completed a level, as in, either won or lost, Check the amount of Health they have 
    /// </summary>
    public void LevelCompleted()
    {
        Time.timeScale = 0f;
        hasLevelCompleted = true;
        CheckPlayerHealth();
    }

    /// <summary>
    /// Based on the amount of Health the player has, show Stars accordingly, give no stars if player loses.
    /// Also, show the GameOverScreen
    /// </summary>
    private void CheckPlayerHealth()
    {
        int currentHealth = playerHealth.CurrentHealth;

        if (currentHealth >= threeStars)
        {
            collectedStar01.SetActive(true);
            collectedStar02.SetActive(true);
            collectedStar03.SetActive(true);
        }
        else if (currentHealth >= twoStars && currentHealth < threeStars)
        {
            collectedStar01.SetActive(true);
            collectedStar02.SetActive(true);
            uncollectedStar03.SetActive(true);
        }
        else if (currentHealth >= oneStar && currentHealth < twoStars)
        {
            collectedStar01.SetActive(true);
            uncollectedStar02.SetActive(true);
            uncollectedStar03.SetActive(true);
        }
        else
        {
            uncollectedStar01.SetActive(true);
            uncollectedStar02.SetActive(true);
            uncollectedStar03.SetActive(true);
        }
    }
}
