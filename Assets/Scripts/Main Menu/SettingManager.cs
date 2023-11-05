using UnityEngine;
using UnityEngine.EventSystems;

public class SettingManager : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private Transform playButton;

    [Header("Settings")]
    [SerializeField] private CanvasGroup settingsCanvas;
    [SerializeField] private AudioSettings audioSettings;

    public void CloseSettings()
    {
        AudioPlayer.Instance.PlayRandomButtonSFX();
        
        settingsCanvas.alpha = 0;
        settingsCanvas.interactable = false;
        settingsCanvas.blocksRaycasts = false;

        audioSettings.UnsubscribeOnValueChanged();

        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(playButton.gameObject);
    }
}
