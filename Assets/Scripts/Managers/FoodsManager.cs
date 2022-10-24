using UnityEditor;
using UnityEngine;

public class FoodsManager : MonoBehaviour
{
    public GameObject[] Foods;
    private GameObject FoodPositive, FoodNegative;
    [SerializeField] private Collider2D gridArea;
    [HideInInspector]public bool hasSpawnfoodp, hasSpawnfoodn;
    float timePassed = 0;
    [SerializeField]private float foodLifeSpan = 10f;
    private void Start()
    {
        hasSpawnfoodp = true;
        hasSpawnfoodn = true;
        ReSpawnFood();
    }
   /* private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 10f)
        {
            RandomFoodPosition();
        }
    }*/
    public void ReSpawnFood()
    {
        if (hasSpawnfoodp)
        {
            FoodPositive = Instantiate(Foods[0]);
            FoodPositive.transform.position = RandomFoodPosition();
            hasSpawnfoodp = false;
        }

        if (hasSpawnfoodn)
        {
            FoodNegative = Instantiate(Foods[1]);
            FoodNegative.transform.position = RandomFoodPosition();
            hasSpawnfoodn = false;
        }

    }

    private Vector2 RandomFoodPosition()
    {
        Debug.Log("entered randomfoodposition");

        Bounds bounds = gridArea.bounds;

        // spawn at random position on gridarea
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        x = Mathf.Round(x);
        y = Mathf.Round(y);
  
        Vector2 randomPosition = new Vector2 (x, y);

        return randomPosition;
    }
    public void DestroyFoodPositive()
    {
        Destroy(FoodPositive);
        Debug.Log("Positive food destroyed");
    }

    public void DestroyFoodNegative()
    {
        Destroy(FoodNegative);
        Debug.Log("Negative food destroyed");

    }
}