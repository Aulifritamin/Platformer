using System.Collections;
using UnityEngine;

public class SmoothHealthBar : HealthBarView
{
    [SerializeField] private float _lerpDuration = 0.5f;

    private Coroutine _healthUpdateCoroutine;

    protected override void HealthChanged(float currentHealth, float maxHealth)
    {
        if (_healthUpdateCoroutine != null)
        {
            StopCoroutine(_healthUpdateCoroutine);
        }

        _healthUpdateCoroutine = StartCoroutine(UpdateHealthBar(currentHealth / maxHealth));
    }

    private IEnumerator UpdateHealthBar(float targetValue)
    {
        float startValue = Slider.value;
        float time = 0;

        while (time < _lerpDuration)
        {
            Slider.value = Mathf.Lerp(startValue, targetValue, time / _lerpDuration);
            time += Time.deltaTime;
            yield return null;
        }

        Slider.value = targetValue;
    }
}
