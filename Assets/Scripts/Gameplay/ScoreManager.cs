using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] int score;
    private List<Highscore> highscoreList;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        textMeshPro.text = "0000000";

        string loadedString = PlayerPrefs.GetString("highscore");
        SerializableList<Highscore> serializableList = JsonUtility.FromJson<SerializableList<Highscore>>(loadedString);
        highscoreList = serializableList.list;
    }

    public bool IsHigscoreUnderTwenties(Highscore highscore)
    {
        int index = highscoreList.IndexOf(highscore);

        if (index < 20)
        {
            highscoreList.Remove(highscore);
            return true;
        }
        else return false;
    }

    public void AddScoreToList(Highscore newHighscore)
    {
        highscoreList.Add(newHighscore);
        highscoreList = HighscoreManager.SortHighScore(highscoreList);
    }

    public void ChangeScore(int number)
    {
        int desiredLength = 7;
        score += number;

        string scoreString = score.ToString();
        scoreString = scoreString.PadLeft(desiredLength, '0');

        textMeshPro.text = scoreString;
    }

    public List<Highscore> GetHighscoreList() => highscoreList;
    public int GetScore() => score;
}
