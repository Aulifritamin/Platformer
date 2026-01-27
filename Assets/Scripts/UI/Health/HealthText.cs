using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthText : MonoBehaviour
{
    [SerializeField] private Health _health;
    
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _health.HealthChanged += HealthChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= HealthChanged;
    }

    private void HealthChanged(float currentHealth, float maxHealth)
    {
        _text.text = $"{currentHealth} / {maxHealth}";
    }
}
