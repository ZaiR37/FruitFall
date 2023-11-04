using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI finalScore;
    [SerializeField] HandFollow handFollow;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void GameOver()
    {
        int score = ScoreManager.Instance.GetScore();
        finalScore.text = "Final Score : " + score.ToString();

        handFollow.enabled = false;
        PlayerController.Instance.SetGameOver();
        ShowScreen(true);
    }

    public void ShowScreen(bool status)
    {
        var canvasGroup = transform.GetComponent<CanvasGroup>();

        canvasGroup.alpha = status ? 1 : 0;
        canvasGroup.interactable = status;
        canvasGroup.blocksRaycasts = status;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
