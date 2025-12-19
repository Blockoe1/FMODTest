using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private string busName;
    [SerializeField] private Slider slider;

    private float volume = 1;
    private Bus volBus;

    private void Awake()
    {
        // String must be formatted with the bus:/ prefix
        volBus = RuntimeManager.GetBus("bus:/" + busName);
        // Load teh correct volume value;
        slider.value = volume;
        volBus.setVolume(volume);
    }

    public void OnSliderValueChanged(float newValue)
    {
        volume = newValue;
        volBus.setVolume(volume);
    }
}
