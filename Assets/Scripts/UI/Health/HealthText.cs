using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthText : HealthView
{    
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    protected override void HealthChanged(float currentHealth, float maxHealth)
    {
        _text.text = $"{currentHealth} / {maxHealth}";
    }
}
