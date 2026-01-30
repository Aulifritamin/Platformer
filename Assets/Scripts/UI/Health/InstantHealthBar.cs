using UnityEngine.UI;

public class InstantHealthBar : HealthView
{
    protected Slider Slider;

    protected virtual void Awake()
    {
        Slider = GetComponent<Slider>();
    }
    
    protected override void HealthChanged(float currentHealth, float maxHealth)
    {
        Slider.value = currentHealth / maxHealth;
    }
}
