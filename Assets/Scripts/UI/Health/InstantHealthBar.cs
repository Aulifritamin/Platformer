using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class InstantHealthBar : HealthView
{
    protected Slider Slider;

    protected virtual void Awake()
    {
        Slider = GetComponent<Slider>();
    }
    
    protected override void OnUpdateView(float currentHealth, float maxHealth)
    {
        Slider.value = currentHealth / maxHealth;
    }
}
