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

    public bool IsDead()
    {
        return currentHealth > 0;
    }

    public void TakeDamage(int damageAmount)
    {
        if(currentHealth > 0)
        {
            currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
            OnHealthChange?.Invoke(currentHealth, maxHealth);
            Debug.Log($"Current Healht: {currentHealth}");
        }
    }
}
