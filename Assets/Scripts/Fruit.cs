using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] string fruitName;
    [SerializeField] int fruitValue;
    [SerializeField] Transform nextFruitPrefab;
    private bool sameFruitCollided;
    private bool fallFruit;

    private void Start()
    {
        gameObject.name = fruitName;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!fallFruit && PlayerController.Instance.GetCurrentFruit() == transform)
        {
            if (other.gameObject.name == "FinalLine") return;
            PlayerController.Instance.GetNextFruit();
            ScoreManager.Instance.AddScore(fruitValue);
            fallFruit = true;
        }

        if (!other.gameObject.tag.Equals("Fruits")) return;
        if (!other.gameObject.name.Equals(fruitName)) return;
        if (sameFruitCollided) return;
        if (nextFruitPrefab == null) return;

        other.gameObject.GetComponent<Fruit>().SetCollided();

        Destroy(other.gameObject);
        Destroy(gameObject);

        Transform container = PlayerController.Instance.GetFruitContainer();
        Transform newFruit = Instantiate(nextFruitPrefab, transform.position, Quaternion.identity, container);
        newFruit.GetComponent<Fruit>().SetFruitStatus();

        int score = nextFruitPrefab.GetComponent<Fruit>().GetFruitValue() - (fruitValue * 2);
        ScoreManager.Instance.AddScore(score);
    }

    public void SetCollided() => sameFruitCollided = true;
    public void SetFruitStatus() => fallFruit = true;
    public int GetFruitValue() => fruitValue;
    public bool GetFruitStatus() => fallFruit;

}
