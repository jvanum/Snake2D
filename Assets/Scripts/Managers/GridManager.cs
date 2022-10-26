using UnityEngine;
public class GridManager : MonoBehaviour
{
    public static Bounds bounds;
    BoxCollider2D boxCollider2D;
    private Snakes _snake;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        bounds = boxCollider2D.bounds;
        _snake = FindObjectOfType<Snakes>();

    }
    public static Vector2 RandomPosition()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        x = Mathf.Round(x);
        y = Mathf.Round(y);

        Vector2 randomPosition = new Vector2(x, y);
        return randomPosition;
    }
}