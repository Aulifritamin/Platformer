using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _volumeParameter;

    private Slider _slider;

    private const float MinSliderValue = 0f;
    private const float MaxSliderValue = 1f;
    private const float MuteDbValue = -80f; 
    private const float LinearToDbMultiplier = 20f;
    private const float DbCalculationBase = 10f;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        
        _slider.minValue = MinSliderValue;
        _slider.maxValue = MaxSliderValue;

        _slider.onValueChanged.AddListener(SliderValueChanged);
    }

    private void OnEnable()
    {
        if (_audioMixer.GetFloat(_volumeParameter, out float dbValue))
        {
            if (dbValue <= MuteDbValue)
            {
                _slider.SetValueWithoutNotify(MinSliderValue);
            }
            else
            {
                float linearValue = Mathf.Pow(DbCalculationBase, dbValue / LinearToDbMultiplier);
                _slider.SetValueWithoutNotify(linearValue);
            }
        }
    }

    private void SliderValueChanged(float value)
    {
        if (value <= MinSliderValue)
        {
            _audioMixer.SetFloat(_volumeParameter, MuteDbValue);
        }
        else
        {

            float dB = Mathf.Log10(value) * LinearToDbMultiplier;
            _audioMixer.SetFloat(_volumeParameter, dB);
        }
    }
}