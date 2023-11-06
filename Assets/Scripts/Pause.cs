using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] CanvasGroup pauseSettingGroup;

    public void OnPauseSetting()
    {
        AudioPlayer.Instance.PlayRandomButtonSFX();
        
        pauseSettingGroup.alpha = 1;
        pauseSettingGroup.interactable = true;
        pauseSettingGroup.blocksRaycasts = true;
    }
}
