using UnityEngine;
using UnityEngine.UI;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] protected Health Health;

    protected virtual void OnEnable()
    {
        Health.Changed += OnUpdateView;
    }

    protected virtual void OnDisable()
    {
        Health.Changed -= OnUpdateView;
    }

    protected abstract void OnUpdateView(float currentHealth, float maxHealth);
}
