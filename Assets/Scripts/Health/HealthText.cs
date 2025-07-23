using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public int numberOfHealth = 20;

    [SerializeField] private Health health;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Awake()
    {
        healthText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        if (healthText != null)
        {
            health.OnHealthChange += UpdateHealthText;
        }
    }

    /// <summary>
    /// Reduce the number on the healthText by 1
    /// </summary>
    void UpdateHealthText(int currentHealth, int maxHealth)
    {
        numberOfHealth--;
        healthText.text = "Health: " + numberOfHealth;
    }
}
