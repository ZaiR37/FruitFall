using UnityEngine;

public class FinalLine : MonoBehaviour
{
    bool gameover;

    private void Start()
    {
        gameObject.name = "FinalLine";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckObject(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CheckObject(other);
    }

    private void CheckObject(Collider2D other)
    {
        if (gameover) return;
        if (!other.gameObject.tag.Equals("Fruits")) return;
        bool fallFruit = other.gameObject.GetComponent<Fruit>().GetFruitStatus();

        if (!fallFruit) return;
        GameOverManager.Instance.GameOver();
        gameover = true;
    }
}
