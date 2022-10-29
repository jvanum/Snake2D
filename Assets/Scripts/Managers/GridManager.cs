using UnityEngine;
public class GridManager : MonoBehaviour
{
    public static Bounds bounds;
    private BoxCollider2D boxCollider2D;
    private static Snakes snakes;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        bounds = boxCollider2D.bounds;
        snakes = FindObjectOfType<Snakes>();
    }
    public static Vector2 RandomPosition()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        x = Mathf.Round(x);
        y = Mathf.Round(y);

        while (snakes.Occupies(x, y))
        {
            x++;

            if (x > bounds.max.x)
            {
                x = bounds.min.x;
                y++;

                if (y > bounds.max.y)
                {
                    y = bounds.min.y;
                }
            }
        }

        Vector2 randomPosition = new Vector2(x, y);
        return randomPosition;
    }
}