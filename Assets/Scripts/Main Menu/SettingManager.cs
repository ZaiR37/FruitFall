using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private Transform playButton;

    [Header("Settings")]
    [SerializeField] private CanvasGroup settingsCanvas;
    [SerializeField] private CanvasGroup audioSettingsCanvas;
    [SerializeField] private CanvasGroup gameSettingsCanvas;
    [SerializeField] private AudioSettings audioSettings;

    [Space(10)]
    [SerializeField] private Toggle firstTab;

    private bool audioTab, gameTab;

    public void CloseSettings()
    {
        settingsCanvas.alpha = 0;
        settingsCanvas.interactable = false;
        settingsCanvas.blocksRaycasts = false;

        audioSettings.UnsubscribeOnValueChanged();

        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(playButton.gameObject);

        if (firstTab.isOn) AudioPlayer.Instance.PlayRandomButtonSFX();
        else firstTab.isOn = true;
    }

    public void AudioTab(bool toogle)
    {
        if (toogle != true) return;
        if (audioTab == true) return;

        audioTab = true;
        gameTab = false;

        AudioPlayer.Instance.PlayRandomButtonSFX();
        audioSettings.SubscribeOnValueChanged();

        audioSettingsCanvas.alpha = 1;
        audioSettingsCanvas.interactable = true;
        audioSettingsCanvas.blocksRaycasts = true;

        gameSettingsCanvas.alpha = 0;
        gameSettingsCanvas.interactable = false;
        gameSettingsCanvas.blocksRaycasts = false;
    }

    public void GameTab(bool toogle)
    {
        if (toogle != true) return;
        if (gameTab == true) return;

        gameTab = true;
        audioTab = false;

        AudioPlayer.Instance.PlayRandomButtonSFX();
        audioSettings.UnsubscribeOnValueChanged();

        gameSettingsCanvas.alpha = 1;
        gameSettingsCanvas.interactable = true;
        gameSettingsCanvas.blocksRaycasts = true;

        audioSettingsCanvas.alpha = 0;
        audioSettingsCanvas.interactable = false;
        audioSettingsCanvas.blocksRaycasts = false;
    }

}
