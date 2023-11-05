using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private Transform playButton;

    [Header("Scoreboard")]
    [SerializeField] private CanvasGroup scoreboardCanvas;
    [SerializeField] private Scrollbar scoreboardScrollbar;

    [Header("Settings")]
    [SerializeField] private CanvasGroup settingsCanvas;
    [SerializeField] private AudioSettings audioSettings;

    private void Start()
    {
        AudioPlayer.Instance.PlayMainMenuBGM();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Gameplay");

        AudioPlayer.Instance.PlayRandomButtonSFX();
    }

    public void OpenScoreboardButton()
    {
        AudioPlayer.Instance.PlayRandomButtonSFX();

        scoreboardCanvas.alpha = 1;
        scoreboardCanvas.interactable = true;
        scoreboardCanvas.blocksRaycasts = true;

        scoreboardScrollbar.value = 1;
    }

    public void CloseScoreboardButton()
    {
        AudioPlayer.Instance.PlayRandomButtonSFX();

        scoreboardCanvas.alpha = 0;
        scoreboardCanvas.interactable = false;
        scoreboardCanvas.blocksRaycasts = false;

        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(playButton.gameObject);
    }

    public void OpenSettings()
    {
        AudioPlayer.Instance.PlayRandomButtonSFX();

        settingsCanvas.alpha = 1;
        settingsCanvas.interactable = true;
        settingsCanvas.blocksRaycasts = true;

        audioSettings.SubscribeOnValueChanged();
    }

    public void ExitGameButton()
    {
        AudioPlayer.Instance.PlayRandomButtonSFX();
        
        Application.Quit();
    }
}
