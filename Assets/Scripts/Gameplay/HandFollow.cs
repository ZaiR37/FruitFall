using UnityEngine;

public class HandFollow : MonoBehaviour
{
    public float minX = -1.51f;
    public float maxX = 2.72f; 

    void Update()
    {
        if (PlayerController.Instance.Stop()) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float clampedX = Mathf.Clamp(mousePosition.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
