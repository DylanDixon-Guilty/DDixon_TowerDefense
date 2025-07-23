using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image healthBar;

    
    void Start()
    {
        if(health != null)
        {
            health.OnHealthChange += UpdateHealthBar;
        }
    }

    /// <summary>
    /// Reduce the Health bar when player is damaged
    /// </summary>
    void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }
}
