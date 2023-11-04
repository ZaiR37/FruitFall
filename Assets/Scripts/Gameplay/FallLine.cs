using UnityEngine;

public class FallLine : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform startPoint;

    int maxLine = 5;

    private void Start()
    {
        lineRenderer.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Physics2D.Raycast(startPoint.position, -transform.up))
        {
            RaycastHit2D hit = Physics2D.Raycast(startPoint.position, -transform.up);
            DrawLine(startPoint.position, hit.point);
        }
        else
        {
            DrawLine(startPoint.position, startPoint.up * -maxLine);
        }
    }

    void DrawLine(Vector2 startPosition, Vector2 endPosition)
    {
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, new Vector3(startPosition.x, endPosition.y));
    }
}
