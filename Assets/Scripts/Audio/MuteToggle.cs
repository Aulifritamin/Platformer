using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class MuteToggle : MonoBehaviour
{
    private Toggle _muteToggle;

    private void Awake()
    {
        _muteToggle = GetComponent<Toggle>();
        _muteToggle.isOn = AudioListener.pause;

        _muteToggle.onValueChanged.AddListener(ToggleValueChanged);
    }

    private void OnEnable()
    {
        _muteToggle.SetIsOnWithoutNotify(AudioListener.pause);
    }

    private void OnDisable()
    {
        if (_muteToggle != null)
        {
            _muteToggle.onValueChanged.RemoveListener(ToggleValueChanged);
        }
    }

    private void ToggleValueChanged(bool isMuted)
    {
        AudioListener.pause = isMuted;
    }
}
