using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class HealthBarView : MonoBehaviour
{
    [SerializeField] protected Health Health;
    
    protected Slider Slider;

    private void Awake()
    {
        Slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        Health.HealthChanged += HealthChanged;
    }

    private void OnDisable()
    {
        Health.HealthChanged -= HealthChanged;
    }

    protected abstract void HealthChanged(float currentHealth, float maxHealth);
}
