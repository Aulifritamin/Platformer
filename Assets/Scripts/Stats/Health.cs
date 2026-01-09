using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth;

    private float _minHealth = 0f;

    public event Action Die;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= Mathf.Min(damage, _currentHealth);

        if (_currentHealth <= _minHealth)
        {
            Die?.Invoke();
        }
    }

    public void Heal(float healAmount)
    {
        _currentHealth += Mathf.Min(healAmount, _maxHealth - _currentHealth);
    }
}
