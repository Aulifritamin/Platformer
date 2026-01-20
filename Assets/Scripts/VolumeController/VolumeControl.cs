using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSource _buttonMusicSource;
    [SerializeField] private AudioClip[] _musicClips;

    private string _masterParam = "MasterVolume";
    private string _buttonParam = "ButtonVolume";
    private string _bgmParam = "BackgroundVolume";
    
    private float _lastMasterValue = 1f;
    private bool _isMuted = false;

    public void SetButtonVolume(float sliderValue) => ApplyVolume(_buttonParam, sliderValue);
    public void SetBackgroundVolume(float sliderValue) => ApplyVolume(_bgmParam, sliderValue);

    public void SetMasterVolume(float sliderValue)
    {
        _lastMasterValue = sliderValue;

        if (!_isMuted)
        {
            ApplyVolume(_masterParam, sliderValue);
        }
            
    }

    public void PlayMusic(int clipIndex)
    {
        if (_musicClips != null && clipIndex >= 0 && clipIndex < _musicClips.Length)
        {
            _buttonMusicSource.clip = _musicClips[clipIndex];
            _buttonMusicSource.Play();
        }
    }

    public void MuteVolume(bool isMuted)
    {
        _isMuted = isMuted;
        float targetVolume = isMuted ? 0.0001f : _lastMasterValue;       

        ApplyVolume(_masterParam, targetVolume);
    }

    private void ApplyVolume(string parameterName, float sliderValue)
    {
        float level = Mathf.Clamp(sliderValue, 0.0001f, 1f);
        float dB = Mathf.Log10(level) * 20;

        _audioMixer.SetFloat(parameterName, dB);
    }
}