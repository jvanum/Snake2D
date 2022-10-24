using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float speed = 16f; // speed of snake
    [SerializeField] private float speedBoost = 1f; //speed boost for snake/powerup
    [SerializeField] private int initialSize = 4; // num of segments on game start
    [SerializeField] private int unitsIncrease = 1; // num of segments to grow for snake
    [SerializeField] private int unitsDecrease = 1; // num of segments to remove for snake
    private float powerUpDuration; // the duration of powerups being active
    private float nextUpdate;
    private bool canScoreBoost; // bool for scoreboost powerup pickup
    private bool canShield; // bool for shield powerup pickup

    private Vector2 snakeDirection = Vector2.right; // initial snake direction
    private Vector2 input; // input for snake direction

    [SerializeField] private ScoreController scoreController;
    [SerializeField] private TimerIndicator timerIndicator;
    [SerializeField] private PauseResume pauseResume;
    [SerializeField] private GameOver gameOver;
    [SerializeField] private PowerUpsManager powerUpsManager;
    [SerializeField] private FoodsManager foodsManager;
    [SerializeField] private Transform snakeBodyPrefab;

    private List<Transform> snakeSegments = new();
    
    // Start is called before the first frame update
    private void Start()
    {
        RespawnSnake();
    }

    // Update is called once per frame
    private void Update()
    {
        PauseGame();
        SnakeDirection();
    }
    private void FixedUpdate()
    {
        SnakeMovement(speedBoost);
    }

    private float RandomDuration()   //randow powerup duration
    {
        powerUpDuration = Random.Range(5, 10);
        return powerUpDuration;
        
    }

    //taking input for direction of snake
    private void SnakeDirection()
    {
        //snake can move left/right only when moving up/down
        if (snakeDirection.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) { input = Vector2.left; }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) { input = Vector2.right; }
        }
        //snake can move up/down only when moving left/right
        if (snakeDirection.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) { input = Vector2.up; }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) { input = Vector2.down; }
        }

    }
    //movement of the snake based on direction of input
    private void SnakeMovement(float speedBoost)
    {
        if (Time.time < nextUpdate)
        {
            return;
        }
        //sets direction of the snakehead
        if (input != Vector2.zero)
        {
            snakeDirection = input;
        }

        for (int i = snakeSegments.Count - 1; i > 0; i--)
        {
            snakeSegments[i].position = snakeSegments[i - 1].position;
        }
        
        float x = Mathf.Round(transform.position.x) + snakeDirection.x;
        float y = Mathf.Round(transform.position.y) + snakeDirection.y;

        transform.position = new Vector2(x, y);

        nextUpdate = Time.time + (1f / (speed * speedBoost));
    }
    // increase snake by one segment when eaten positive food
    private void GrowSnake()
    {
        Transform segment = Instantiate(this.snakeBodyPrefab);
        segment.position = snakeSegments[snakeSegments.Count - 1].position;
        snakeSegments.Add(segment);
    }
    // reduce snake by one segment when eaten negative food
    private void ReduceSnake()
    {
        if (snakeSegments.Count > 4)
        {
            Destroy(snakeSegments[snakeSegments.Count - 1].gameObject);
            snakeSegments.RemoveAt(snakeSegments.Count - 1);
        }
    }
    // respawn snake at start or after death
    private void RespawnSnake()
    {
        for (int i = 1; i < snakeSegments.Count; i++)
        {
            Destroy(snakeSegments[i].gameObject);
        }
        snakeSegments.Clear();
        snakeSegments.Add(this.transform);

        for (int i = 1; i < initialSize; i++)
        {
            snakeSegments.Add(Instantiate(this.snakeBodyPrefab));
        }
        this.transform.position = Vector3.zero;
    }


    // pause game function
    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
            Time.timeScale = 0;
            AudioListener.pause = true;
            pauseResume.gameObject.SetActive(true);
        }

    }


    //resets powerups after given time
    private void ResetPowerUp()
    {
        powerUpsManager.hasSpawn = true;
        powerUpsManager.ReSpawnPowerUp();
        speedBoost = 1f;
        canShield = false;
        canScoreBoost = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // reset position on colliding border, screen wrapping for all the directions
        if (collision.gameObject.CompareTag("Borders"))
        {
            Vector3 resetPosition = transform.position;

            if (snakeDirection.x != 0f)
            {
                resetPosition.x = -collision.transform.position.x + snakeDirection.x;
            }
            else if (snakeDirection.y != 0f)
            {
                resetPosition.y = -collision.transform.position.y + snakeDirection.y;
            }
            transform.position = resetPosition;
        }

        // snake eating itself
        if (collision.gameObject.CompareTag("Respawn"))
        {
            if (canShield)
            {
                return;
            }
            else
            {
                SoundManager.Instance.Play(SoundTypes.SNAKEDEATH);
                gameOver.gameObject.SetActive(true);
            }
        }
        // snake eating different kinds of eatables
        if (collision.gameObject.TryGetComponent(out PowerUps powerUps))
        {
            RandomDuration();
            if (powerUps.powerUpsTypes == PowerUpsTypes.SpeedUp)
            {
                SoundManager.Instance.Play(SoundTypes.SPEEDUP);
                Debug.Log("speedup collected");
                speedBoost = 2f;
                timerIndicator.gameObject.SetActive(true);
                timerIndicator.TimerName("Speed Up ");
                timerIndicator.TimerStart(powerUpDuration);
                Invoke(nameof(ResetPowerUp), powerUpDuration);
            }
            else if (powerUps.powerUpsTypes == PowerUpsTypes.ScoreBoost)
            {
                SoundManager.Instance.Play(SoundTypes.SCOREBOOST);
                Debug.Log("Scoreboost collected");
                canScoreBoost = true;
                timerIndicator.gameObject.SetActive(true);
                timerIndicator.TimerName("Score Booster ");
                timerIndicator.TimerStart(powerUpDuration);
                Invoke(nameof(ResetPowerUp), powerUpDuration);
            }
            else if (powerUps.powerUpsTypes == PowerUpsTypes.Shield)
            {
                SoundManager.Instance.Play(SoundTypes.SHIELD);
                Debug.Log("shield collected");
                canShield = true;
                timerIndicator.gameObject.SetActive(true);
                timerIndicator.TimerName("Shield ");
                timerIndicator.TimerStart(powerUpDuration);
                Invoke(nameof(ResetPowerUp), powerUpDuration);
            }
            powerUpsManager.DestroyPowerUp();

        }

        if (collision.gameObject.TryGetComponent(out Foods foods))
        {
            if (foods.foodsTypes == FoodsTypes.PositiveFood)
            {
                SoundManager.Instance.Play(SoundTypes.POSITIVEFOOD);
                for (int i = 0; i < unitsIncrease; i++)
                {
                    GrowSnake();
                }
                if (canScoreBoost)
                {
                    scoreController.IncreaseScore(20);
                }
                else
                {
                    scoreController.IncreaseScore(10);
                }
                foodsManager.DestroyFoodPositive();
                foodsManager.hasSpawnfoodp = true;
                foodsManager.ReSpawnFood();

            }
            else if (foods.foodsTypes == FoodsTypes.NegativeFood)
            {
                
                if(!canShield)
                {
                    SoundManager.Instance.Play(SoundTypes.NEGATIVEFOOD);
                    for (int i = 0; i < unitsDecrease; i++)
                    {
                        ReduceSnake();
                    }
                    scoreController.DecreaseScore(5);
                }
                foodsManager.DestroyFoodNegative();
                foodsManager.hasSpawnfoodn = true;
                foodsManager.ReSpawnFood();
            }
            
        }


    }
}
