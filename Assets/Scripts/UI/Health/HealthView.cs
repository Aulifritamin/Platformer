using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class HealthView : MonoBehaviour
{
    [SerializeField] protected Health Health;

    protected virtual void OnEnable()
    {
        Health.HealthChanged += HealthChanged;
    }

    protected virtual void OnDisable()
    {
        Health.HealthChanged -= HealthChanged;
    }

    protected abstract void HealthChanged(float currentHealth, float maxHealth);
}
