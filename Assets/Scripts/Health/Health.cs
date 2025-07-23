using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public event Action<int, int> OnHealthChange;

    [SerializeField] private int maxHealth = 20;
    [SerializeField] private int currentHealth;
    

    private void Awake()
    {
        currentHealth = maxHealth;
        
    }

    /// <summary>
    /// Function to state that the player has died.
    /// </summary>
    public bool IsDead()
    {
        return currentHealth > 0;
    }

    /// <summary>
    /// Function for the Enemy to call upon to deal damage to the player.
    /// </summary>
    public void TakeDamage(int damageAmount)
    {
        if(currentHealth > 0)
        {
            currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
            OnHealthChange?.Invoke(currentHealth, maxHealth);
            Debug.Log($"Current Health: {currentHealth}");
        }
    }
}
