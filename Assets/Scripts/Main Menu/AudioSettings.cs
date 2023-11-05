using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("------ Volume Slider ------")]
    [SerializeField] private Slider MasterVolumeSlider;
    [SerializeField] private Slider BGMVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;

    private void Start()
    {
        MasterVolumeSlider.value = AudioPlayer.Instance.GetMasterVolume();
        BGMVolumeSlider.value = AudioPlayer.Instance.GetBGMVolume();
        SFXVolumeSlider.value = AudioPlayer.Instance.GetSFXVolume();
    }

    public void SubscribeOnValueChanged()
    {
        MasterVolumeSlider.onValueChanged.AddListener(delegate { SetMasterVolume(); });
        BGMVolumeSlider.onValueChanged.AddListener(delegate { SetBGMVolume(); });
        SFXVolumeSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });
    }

    public void UnsubscribeOnValueChanged()
    {
        MasterVolumeSlider.onValueChanged.RemoveListener(delegate { SetMasterVolume(); });
        BGMVolumeSlider.onValueChanged.RemoveListener(delegate { SetBGMVolume(); });
        SFXVolumeSlider.onValueChanged.RemoveListener(delegate { SetSFXVolume(); });
    }

    public void SetDefaultValue()
    {
        AudioPlayer.Instance.PlayRandomButtonSFX();

        MasterVolumeSlider.value = 0.8f;
        BGMVolumeSlider.value = 0.4f;
        SFXVolumeSlider.value = 0.9f;

        SetMasterVolume();
        SetBGMVolume();
        SetSFXVolume();
    }

    private void SetMasterVolume() => AudioPlayer.Instance.SetMasterVolume(MasterVolumeSlider.value);
    private void SetBGMVolume() => AudioPlayer.Instance.SetBGMVolume(BGMVolumeSlider.value);
    private void SetSFXVolume()
    {
        AudioPlayer.Instance.SetSFXVolume(SFXVolumeSlider.value);
        AudioPlayer.Instance.PlayRandomButtonSFX();
    }
}
