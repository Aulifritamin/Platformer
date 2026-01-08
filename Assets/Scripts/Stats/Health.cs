using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour, IDemagable
{
    [SerializeField] private float _currentHealth;
    private Inventory _inventory;
    private float _maxHealth = 100f;
    private float _minHealth = 0f;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void OnEnable()
    {
        if (_inventory != null)
        {
            _inventory.AidKidCollected += HealOnAidKidCollected;
        }
    }

    private void OnDisable()
    {
        if (_inventory != null)
        {
            _inventory.AidKidCollected -= HealOnAidKidCollected;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= Mathf.Min(damage, _currentHealth);

        if (_currentHealth <= _minHealth)
        {
            Die();
        }
    }

    private void HealOnAidKidCollected(AidKid aidKid)
    {
        Heal(aidKid.HealAmount);
    }

    public void Heal(float healAmount)
    {
        _currentHealth += Mathf.Min(healAmount, _maxHealth - _currentHealth);
    }
}
