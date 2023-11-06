using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    [SerializeField] private Transform scoreNumberText;
    [SerializeField] private Transform scoreNameText;
    [SerializeField] private Transform scoreValueText;

    [SerializeField] private Transform numberColumn;
    [SerializeField] private Transform nameColumn;
    [SerializeField] private Transform scoreColumn;

    private List<Highscore> highscoreList;

    private void Start()
    {
        string loadedString = PlayerPrefs.GetString("highscore");

        if (string.IsNullOrEmpty(loadedString)) DefaultHighscoreBoard(20);
        else
        {
            SerializableList<Highscore> serializableList = JsonUtility.FromJson<SerializableList<Highscore>>(loadedString);
            highscoreList = serializableList.list;
        }

        UpdateScoreboard();
    }

    private void DefaultHighscoreBoard(int HighscoreNumber)
    {
        highscoreList = new List<Highscore>();

        for (int i = 0; i < HighscoreNumber; i++)
        {
            int randomHighscore = Random.Range(1000, 10000);
            Highscore newHighscore = new Highscore(GetRandomChar(10), randomHighscore);
            highscoreList.Add(newHighscore);
        }

        highscoreList = SortHighScore(highscoreList);
        SaveHighscore(highscoreList);
    }

    private string GetRandomChar(int charLength)
    {
        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        string randomChar = "";

        for (int i = 0; i < charLength; i++)
        {
            int index = Random.Range(0, alphabet.Length);
            randomChar += alphabet[index];
        }

        return randomChar;
    }

    public static List<Highscore> SortHighScore(List<Highscore> highscoreList)
    {
        Highscore[] highscoreArray = highscoreList.ToArray();

        for (int i = 0; i < highscoreList.Count; i++)
        {
            for (int j = i + 1; j < highscoreList.Count; j++)
            {
                if (highscoreArray[i].Score > highscoreArray[j].Score) continue;
                Highscore temporary = highscoreArray[i];
                highscoreArray[i] = highscoreArray[j];
                highscoreArray[j] = temporary;
            }
        }

        return highscoreArray.ToList();
    }

    public static void SaveHighscore(List<Highscore> highscoreList)
    {
        SerializableList<Highscore> serializableList = new SerializableList<Highscore>
        {
            list = highscoreList
        };

        string highscoreJson = JsonUtility.ToJson(serializableList);
        PlayerPrefs.SetString("highscore", highscoreJson);
        PlayerPrefs.Save();
    }

    public void UpdateScoreboard()
    {
        for (int i = 0; i < numberColumn.childCount; i++)
        {
            Destroy(numberColumn.GetChild(i).gameObject);
        }

        for (int i = 0; i < nameColumn.childCount; i++)
        {
            Destroy(nameColumn.GetChild(i).gameObject);
        }

        for (int i = 0; i < scoreColumn.childCount; i++)
        {
            Destroy(scoreColumn.GetChild(i).gameObject);
        }

        for (int index = 0; index < highscoreList.Count; index++)
        {
            string number = "0";
            if (index < 9) number += (index + 1).ToString();
            else number = (index + 1).ToString();

            Transform numberTextTransform = Instantiate(scoreNumberText, numberColumn);
            numberTextTransform.GetComponent<TextMeshProUGUI>().text = number;
            numberTextTransform.gameObject.name = "number " + number;

            Transform nameTextTransform = Instantiate(scoreNameText, nameColumn);
            nameTextTransform.GetComponent<TextMeshProUGUI>().text = highscoreList[index].Name;
            nameTextTransform.gameObject.name = "name " + number;

            Transform scoreTextTransform = Instantiate(scoreValueText, scoreColumn);
            scoreTextTransform.GetComponent<TextMeshProUGUI>().text = highscoreList[index].Score.ToString();
            scoreTextTransform.gameObject.name = "score " + number;
        }
    }

    public List<Highscore> GetHighscoreList() => highscoreList;
}
