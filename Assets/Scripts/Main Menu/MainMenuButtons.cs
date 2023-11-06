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

    public void ExitGameButton()
    {
        AudioPlayer.Instance.PlayRandomButtonSFX();

        Application.Quit();
    }
}
