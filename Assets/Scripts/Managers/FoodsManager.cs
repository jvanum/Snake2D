using System;
using System.Reflection;
using UnityEngine;

public class FoodsManager : MonoBehaviour
{
    public GameObject[] Foods;
    private GameObject FoodPositive;
    private GameObject FoodNegative;
    private SnakeController snakeObj;
    private float foodSpawningIn;
    private int lengthsnake;
    private bool canSpawnFoodP;
    private bool canSpawnFoodN;
    [SerializeField] private float resetPositiveFoodIn = 15f;
    [SerializeField] private float resetNegativeFoodIn = 10f;

    private void Start()
    {
        canSpawnFoodP = true;
        canSpawnFoodN = true;
        FoodSpawningIn();
    }

    private void Update()
    {
        SnakeLength();
    }
    private void Awake()
    {
        snakeObj = FindObjectOfType<SnakeController>();
    }

    // returns the length of snake
    private int SnakeLength()
    {
        lengthsnake = snakeObj.GetSnakeCount();
        return lengthsnake;
    }

    // food will spawn after a few seconds gap
    private void FoodSpawningIn() 
    {
        foodSpawningIn = UnityEngine.Random.Range(1, 4);
        Invoke(nameof(SpawnFood), foodSpawningIn);
    }

    // spawns food
    private void SpawnFood()
    {
                     
        if (canSpawnFoodP)
        {
            FoodPositive = Instantiate(Foods[0]);
            CancelInvoke(nameof(ResetPosition));
            FoodPositive.transform.position = GridManager.RandomPosition();
            canSpawnFoodP = false;
            InvokeRepeating(nameof(ResetPosition), resetPositiveFoodIn, resetPositiveFoodIn);      // destroys positive food after not picking up
        }
        if (lengthsnake > 6)   // only spawn negative food when snake length is more than 6
        {
            if (canSpawnFoodN)
            {
                FoodNegative = Instantiate(Foods[1]);
                CancelInvoke(nameof(ResetPosition));
                FoodNegative.transform.position = GridManager.RandomPosition();
                canSpawnFoodN = false;
                InvokeRepeating(nameof(ResetPosition), resetNegativeFoodIn, resetNegativeFoodIn);      // destroys negative food after not picking up
            }
        }
    }

    private void ResetPosition()
    {
        if (FoodPositive != null)
        {
            Debug.Log("POSITIVE Food RESET");
            FoodPositive.transform.position = GridManager.RandomPosition();
        }
        else if (FoodNegative != null)
        { 
            FoodNegative.transform.position = GridManager.RandomPosition();
        }
    }
    public void DestroyFoodP()
    {
        Destroy(FoodPositive);
        canSpawnFoodP = true;
        SpawnFood();
    }

    public void DestroyFoodN()
    {
        Destroy(FoodNegative);
        canSpawnFoodN = true;
        FoodSpawningIn();
    }
}