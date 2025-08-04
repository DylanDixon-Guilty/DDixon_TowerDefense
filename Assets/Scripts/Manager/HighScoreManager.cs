using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    /*
       The HighScore is represented by 3 Stars. Your remaining health when you win a Level determines how many stars you get.
       For example, if you have 19 Health remaining and win, you get 3 stars
    */

    [SerializeField] private Health playerHealth;

    [Header("Star Threshold")]
    [SerializeField] private int threeStars = 18;
    [SerializeField] private int twoStars = 10;
    [SerializeField] private int oneStar = 9;

    private bool hasWon = false;
    
    
    public void WonLevel()
    {
        hasWon = true;
        CheckHighScore();
    }

    private void CheckHighScore()
    {
        if (hasWon || playerHealth.IsDead())
        {
            int currentHealth = playerHealth.CurrentHealth;
            int starsAwarded;

            if (currentHealth >= threeStars)
            {
                starsAwarded = 3;
            }
            else if (currentHealth >= twoStars && currentHealth < threeStars)
            {
                starsAwarded = 2;
            }
            else if (currentHealth >= oneStar && currentHealth < twoStars)
            {
                starsAwarded = 1;
            }
            else
            {
                starsAwarded = 0;
            }
            Debug.Log("Stars Awarded: " + starsAwarded);
        }
    }
}
