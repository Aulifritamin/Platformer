public class InstantHealthBar : HealthBarView
{
    protected override void HealthChanged(float currentHealth, float maxHealth)
    {
        Slider.value = currentHealth / maxHealth;
    }
}
