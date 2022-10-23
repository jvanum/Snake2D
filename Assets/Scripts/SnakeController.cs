using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Vector2 snakeDirection = Vector2.right;
    private Vector2 input;

    [SerializeField] private ScoreController scoreController;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        SnakeDirection();
    }
    private void FixedUpdate()
    {
        SnakeMovement();
    }

    //takes input for direction of snake
    private void SnakeDirection ()
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
    private void SnakeMovement()
    {//sets direction of the snakehead
        if (input != Vector2.zero)
        {
            snakeDirection = input;
        }

        float x = Mathf.Round(transform.position.x) + snakeDirection.x;
        float y = Mathf.Round(transform.position.y) + snakeDirection.y;

        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // reset position on colliding border, screen wrapping for all the directions
        if (collision.gameObject.CompareTag("Borders"))
        {
            Vector3 resetPosition = transform.position;

            if (snakeDirection.x != 0f)
            {
                resetPosition.x = - collision.transform.position.x + snakeDirection.x;
            }
            else if (snakeDirection.y != 0f)
            {
                resetPosition.y = - collision.transform.position.y + snakeDirection.y;
            }
            transform.position = resetPosition;
        }

        if(collision.gameObject.TryGetComponent(out Eatables eatables))
        {
            if(eatables.eatableTypes == EatableTypes.PositiveFood)
            {
                scoreController.IncreaseScore(10);
            }
            if(eatables.eatableTypes == EatableTypes.NegativeFood)
            {
                scoreController.DecreaseScore(5);
            }
        }
    }
}
