using TMPro;
using UnityEngine;
using Toggle = UnityEngine.UI.Toggle;

public class GameSettings : MonoBehaviour
{
    enum Quality
    {
        LOW,
        MEDIUM,
        HIGH
    }

    [SerializeField] private Toggle firstQuality;
    [SerializeField] private TMP_Dropdown resolutionDropDown;

    private Quality currentQuality = Quality.HIGH;
    private bool isFullScreen = true;

    private void Start()
    {
        QualitySettings.SetQualityLevel(QualitySettings.GetQualityLevel());
        switch (QualitySettings.GetQualityLevel())
        {
            case 0:
                currentQuality = Quality.LOW;
                break;
            case 1:
                currentQuality = Quality.MEDIUM;
                break;
            case 2:
                currentQuality = Quality.HIGH;
                break;
        }

        switch (Screen.width)
        {
            case 1920:
                resolutionDropDown.SetValueWithoutNotify(0);
                break;
            case 1280:
                resolutionDropDown.SetValueWithoutNotify(1);
                break;
            case 640:
                resolutionDropDown.SetValueWithoutNotify(2);
                break;
        }
    }

    public void ResolutionDropDown(int value)
    {
        AudioPlayer.Instance.PlayRandomButtonSFX();

        int screenWidth = 0;
        int screenHeight = 0;

        switch (value)
        {
            case 0:
                screenWidth = 1920;
                screenHeight = 1080;
                break;

            case 1:
                screenWidth = 1280;
                screenHeight = 720;
                break;

            case 2:
                screenWidth = 640;
                screenHeight = 480;
                break;
        }

        Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
    }

    public void FullScreen(bool toggle)
    {
        AudioPlayer.Instance.PlayRandomButtonSFX();
        Screen.fullScreen = toggle;
        isFullScreen = toggle;
    }

    public void FastQualityButton(bool toggle)
    {
        if (currentQuality == Quality.LOW) return;
        currentQuality = Quality.LOW;

        AudioPlayer.Instance.PlayRandomButtonSFX();
        QualitySettings.SetQualityLevel(0);
    }

    public void GoodQualityButton(bool toggle)
    {
        if (currentQuality == Quality.MEDIUM) return;
        currentQuality = Quality.MEDIUM;

        AudioPlayer.Instance.PlayRandomButtonSFX();
        QualitySettings.SetQualityLevel(1);
    }

    public void HighQualityButton(bool toggle)
    {
        if (currentQuality == Quality.HIGH) return;
        currentQuality = Quality.HIGH;

        AudioPlayer.Instance.PlayRandomButtonSFX();
        QualitySettings.SetQualityLevel(2);
    }
}
