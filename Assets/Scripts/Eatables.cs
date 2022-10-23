using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EatableTypes
{
    PositiveFood,
    NegativeFood,
    Shield,
    SpeedUp,
    ScoreBoost
}
public class Eatables : MonoBehaviour
{
    public EatableTypes eatableTypes;
    [SerializeField] private Collider2D gridArea;
    // Start is called before the first frame update
    private void Start()
    {
        RandomSpawn();
    }

    private void RandomSpawn()
    {
        Bounds bounds = gridArea.bounds;

        // spawn at random position on gridarea
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        x = Mathf.Round(x);
        y = Mathf.Round(y);

        transform.position = new Vector2(x, y);
    }

    //snake and eatable collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out SnakeController snakeController))
        {
            RandomSpawn();
            switch (eatableTypes)
            {
                case EatableTypes.PositiveFood:
                    SoundManager.Instance.Play(SoundTypes.POSITIVEFOOD);
                    Debug.Log("Positivefood collected");
                    break;
                case EatableTypes.NegativeFood:
                    SoundManager.Instance.Play(SoundTypes.NEGATIVEFOOD);
                    Debug.Log("Negativefood collected");
                    break;
                case EatableTypes.SpeedUp:
                    SoundManager.Instance.Play(SoundTypes.SPEEDUP);
                    Debug.Log("speedup collected");
                    break;
                case EatableTypes.ScoreBoost:
                    SoundManager.Instance.Play(SoundTypes.SCOREBOOST);
                    Debug.Log("Scoreboost collected");
                    break;
                case EatableTypes.Shield:
                    SoundManager.Instance.Play(SoundTypes.SHIELD);
                    Debug.Log("shield collected");
                    break;
            }
            
        }
        
    }
}

  
