using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public event Action<int, int> OnHealthChange;
    public int MaxHealth = 20;
    public int CurrentHealth;

    [SerializeField] private HighScoreManager highScoreManager;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    /// <summary>
    /// Function to state that the player has died
    /// </summary>
    public bool IsDead()
    {
        return CurrentHealth <= 0;
    }

    /// <summary>
    /// Function for the Enemy to call upon to deal damage to the player
    /// </summary>
    public void TakeDamage(int damageAmount)
    {
        if(CurrentHealth > 0)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - damageAmount, 0);
            OnHealthChange?.Invoke(CurrentHealth, MaxHealth);
        }
    }
}
