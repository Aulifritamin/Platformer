using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    
    private float _currentHealth;
    private const float MinHealth = 0f;

    public event Action Died;
    public event Action<float, float> HealthChanged;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            return;
        }

        _currentHealth = Mathf.Max(_currentHealth - damage, MinHealth);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth == MinHealth)
        {
            Died?.Invoke();
        }
    }

    public void Heal(float healAmount)
    {
        if (healAmount < 0)
        {
            return;
        }
        
        _currentHealth = Mathf.Min(_currentHealth + healAmount, _maxHealth);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
}