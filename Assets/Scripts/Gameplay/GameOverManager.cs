using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI finalScore;
    [SerializeField] HandFollow handFollow;
    [SerializeField] Transform newScoreTransform;

    private string scoreName = "Player";
    private bool newScore;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void GameOver()
    {
        Highscore newHighscore = new Highscore(scoreName, ScoreManager.Instance.GetScore());
        ScoreManager.Instance.AddScoreToList(newHighscore);

        int score = ScoreManager.Instance.GetScore();
        finalScore.text = "Final Score : " + score.ToString();

        if (ScoreManager.Instance.IsHigscoreUnderTwenties(newHighscore))
        {
            newScore = true;
            newScoreTransform.gameObject.SetActive(true);
        }

        handFollow.enabled = false;
        PlayerController.Instance.SetGameOver();
        ShowScreen(true);
    }

    public void ScoreNameChanged(string scoreName)
    {
        this.scoreName = scoreName;
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
        SaveNewScore();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        SaveNewScore();
    }

    private void SaveNewScore()
    {
        if (newScore)
        {
            Highscore newHighscore = new Highscore(scoreName, ScoreManager.Instance.GetScore());
            ScoreManager.Instance.AddScoreToList(newHighscore);

            List<Highscore> highscores = ScoreManager.Instance.GetHighscoreList();
            highscores.RemoveAt(highscores.Count-1);
            HighscoreManager.SaveHighscore(highscores);
        }
    }
}
