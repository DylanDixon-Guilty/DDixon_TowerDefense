using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        if (healthText != null)
        {
            health.OnHealthChange += UpdateHealthText;
        }
    }

    /// <summary>
    /// Reduce the number on the healthText by the damage the Enemy does
    /// </summary>
    private void UpdateHealthText(int currentHealth, int maxHealth)
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }
}
