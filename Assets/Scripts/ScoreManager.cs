using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI textMeshPro;

    int score;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        textMeshPro.text = "0000000000";
    }

    public void AddScore(int number)
    {
        int desiredLength = 10;
        score += number;

        string scoreString = score.ToString();
        scoreString = scoreString.PadLeft(desiredLength, '0');

        textMeshPro.text = scoreString;
    }

    public int GetScore() => score;
}
