
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] Transform fruitContainer, fruitStartingPoint, nextFruitDisplay;
    [SerializeField] GameObject fallLine;
    [SerializeField] List<Transform> fruitList;

    [SerializeField] CanvasGroup pauseTransform;

    private bool fruitIsFalling, gameOver, inPause;

    [SerializeField] private Transform currentFruit, nextFruit;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        AudioPlayer.Instance.PlayGamePlayBGM();

        SetNextFruit();
        GetNextFruit();
    }

    private void Update()
    {
        if (Stop()) return;
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!inPause) Pause();
        }
        else if (fruitIsFalling) return;
        else if (Input.GetMouseButtonDown(0)) DropFruit();
    }

    private void Pause()
    {
        AudioPlayer.Instance.PlayRandomButtonSFX();

        inPause = true;

        pauseTransform.alpha = 1;
        pauseTransform.interactable = true;
        pauseTransform.blocksRaycasts = true;
    }

    public void UnPause()
    {
        AudioPlayer.Instance.PlayRandomButtonSFX();
        
        inPause = false;

        pauseTransform.alpha = 0;
        pauseTransform.interactable = false;
        pauseTransform.blocksRaycasts = false;
    }

    private void DropFruit()
    {
        AudioPlayer.Instance.PlayDropFruitSFX();
        currentFruit = Instantiate(currentFruit, fruitStartingPoint.position, Quaternion.identity, fruitContainer);
        fruitStartingPoint.GetComponent<SpriteRenderer>().sprite = null;
        SetLine(false);
        fruitIsFalling = true;
    }

    public void GetNextFruit()
    {
        SetLine(true);
        SetCurrentFruit(nextFruit);
        SetNextFruit();
        fruitIsFalling = false;
    }

    private void SetCurrentFruit(Transform newFruit)
    {
        currentFruit = newFruit;
        Sprite currentFruitSprite = newFruit.GetComponent<SpriteRenderer>().sprite;
        fruitStartingPoint.GetComponent<SpriteRenderer>().sprite = currentFruitSprite;
        fruitStartingPoint.localScale = newFruit.localScale;
    }

    private void SetNextFruit()
    {
        nextFruit = SelectRandomFruit();
        Sprite nextFruitSprite = nextFruit.GetComponent<SpriteRenderer>().sprite;
        nextFruitDisplay.GetComponent<Image>().sprite = nextFruitSprite;
    }

    private Transform SelectRandomFruit()
    {
        if (fruitList.Count == 0)
        {
            Debug.LogError("The fruit list is empty.");
            return null;
        }

        int randomIndex = Random.Range(0, fruitList.Count);
        return fruitList[randomIndex];
    }

    public bool Stop() => inPause || gameOver;
    private void SetLine(bool status) => fallLine.SetActive(status);

    public Transform GetFruitContainer() => fruitContainer;
    public Transform GetCurrentFruit() => currentFruit;
    public void SetGameOver() => gameOver = true;
}
