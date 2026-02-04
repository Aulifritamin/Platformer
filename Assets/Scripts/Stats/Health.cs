using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const float MinHealth = 0f;
    
    [SerializeField] private float _maxHealth = 100f;    
    
    private float _currentHealth;

    public event Action Died;
    public event Action<float, float> Changed;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        Changed?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            return;
        }

        _currentHealth = Mathf.Max(_currentHealth - damage, MinHealth);
        Changed?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth == MinHealth)
        {
            Died?.Invoke();
        }
    }

    public void Restore(float healAmount)
    {
        if (healAmount < 0)
        {
            return;
        }
        
        _currentHealth = Mathf.Min(_currentHealth + healAmount, _maxHealth);
        Changed?.Invoke(_currentHealth, _maxHealth);
    }
}