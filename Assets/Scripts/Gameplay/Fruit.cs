using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] string fruitName;
    [SerializeField] int fruitValue;
    [SerializeField] Transform nextFruitPrefab;
    private bool alreadyCollided;
    private bool fallFruit;

    private void Start()
    {
        gameObject.name = fruitName;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!fallFruit && PlayerController.Instance.GetCurrentFruit() == transform)
        {
            FirstContact(other.gameObject);
        }

        if (!CanMerge(other.gameObject)) return;

        other.gameObject.GetComponent<Fruit>().SetCollided();

        Transform container = PlayerController.Instance.GetFruitContainer();
        Vector3 newFruitPosition = MiddlePoint(transform.position, other.transform.position);

        Destroy(other.gameObject);
        Destroy(gameObject);

        CreateNewFruit(newFruitPosition, container);

        if (nextFruitPrefab.GetComponent<Fruit>().IsThereNextFruit()) AudioPlayer.Instance.PlayRandomFruitMergeSFX();
        else AudioPlayer.Instance.PlayLastFruitMergeSFX();

        int score = nextFruitPrefab.GetComponent<Fruit>().GetFruitValue() - (fruitValue * 2);
        ScoreManager.Instance.ChangeScore(score);
    }

    private bool CanMerge(GameObject objectCollided)
    {
        if (!objectCollided.tag.Equals("Fruits")) return false;
        if (!objectCollided.name.Equals(fruitName)) return false;
        if (nextFruitPrefab == null) return false;
        if (alreadyCollided) return false;

        return true;
    }

    private void FirstContact(GameObject objectCollided)
    {
        if (objectCollided.name == "FinalLine") return;
        PlayerController.Instance.GetNextFruit();
        ScoreManager.Instance.ChangeScore(fruitValue);
        fallFruit = true;
    }

    private void CreateNewFruit(Vector3 position, Transform parent)
    {
        Transform newFruit = Instantiate(nextFruitPrefab, position, Quaternion.identity, parent);
        newFruit.GetComponent<Fruit>().SetFruitStatus();
    }

    private Vector3 MiddlePoint(Vector3 position1, Vector3 position2)
    {
        return (position1 + position2) / 2f;
    }

    public bool IsThereNextFruit() => nextFruitPrefab != null;
    public void SetCollided() => alreadyCollided = true;
    public void SetFruitStatus() => fallFruit = true;
    public int GetFruitValue() => fruitValue;
    public bool GetFruitStatus() => fallFruit;

}
