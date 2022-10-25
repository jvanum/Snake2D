using System.Collections;
using UnityEditor;
using UnityEngine;

public class FoodsManager : MonoBehaviour
{
    public GameObject[] Foods;
    private GameObject FoodPositive, FoodNegative;
    [SerializeField] private Collider2D gridArea;
    [HideInInspector]public bool hasSpawnfoodp, hasSpawnfoodn;
    private bool foodAlive;
    private void Start()
    {
        hasSpawnfoodp = true;
        hasSpawnfoodn = true;
        ReSpawnFood();
    }

    IEnumerator TimePassed()
    {
        foodAlive = true;
        while (foodAlive)
        {
            Debug.Log("food coroutine called");
            yield return new WaitForSeconds(10f);
            //code here will execute after 10 seconds
            if (FoodPositive != null)
            {
                Debug.Log("positive food coroutine executed");
                FoodPositive.transform.position = RandomPosition();
            }
            if (FoodNegative != null)
            {
                Debug.Log("negative food coroutine executed");
                FoodNegative.transform.position = RandomPosition();
            }
        }
    }

    public void ReSpawnFood()
    {
        if (hasSpawnfoodp)
        {
            FoodPositive = Instantiate(Foods[0]);
            StartCoroutine(TimePassed());
            hasSpawnfoodp = false;
        }

        if (hasSpawnfoodn)
        {
            FoodNegative = Instantiate(Foods[1]);
            StartCoroutine(TimePassed());
            hasSpawnfoodn = false;
        }

    }

    public void DestroyFoodPositive()
    {
        foodAlive = false;
        Destroy(FoodPositive);
        Debug.Log("Positive food destroyed");
    }

    public void DestroyFoodNegative()
    {
        foodAlive = false;
        Destroy(FoodNegative);
        Debug.Log("Negative food destroyed");

    }

    public Vector2 RandomPosition()
    {
        Bounds bounds = gridArea.bounds;
        // spawn at random position on gridarea
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        x = Mathf.Round(x);
        y = Mathf.Round(y);

        Vector2 randomPosition = new Vector2(x, y);

        return randomPosition;
    }
}