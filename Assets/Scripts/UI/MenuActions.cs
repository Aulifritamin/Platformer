using UnityEngine;

public class MenuActions : MonoBehaviour
{
    [SerializeField] private AudioSource _uiAudioSource;

    public void PlaySound(AudioClip clip)
    {
        if (_uiAudioSource != null && clip != null)
        {
            _uiAudioSource.clip = clip;
            _uiAudioSource.Play();
        }
    }
}