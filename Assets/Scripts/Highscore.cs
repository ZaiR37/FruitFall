[System.Serializable]
public class Highscore
{
    public Highscore(string name, int score)
    {
        Name = name;
        Score = score;
    }

    public string Name;
    public int Score;
}