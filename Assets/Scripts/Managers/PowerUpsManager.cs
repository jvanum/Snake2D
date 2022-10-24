using UnityEditor;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public GameObject[] PowerUps;
    private GameObject powerUp;
    [SerializeField] private Collider2D gridArea;
    [HideInInspector] public bool hasSpawn;
    [SerializeField]private float powerLifeSpan = 10f;
    float timePassed = 0;

    private void Start()
    {
        hasSpawn = true;
        ReSpawnPowerUp();
    }
    /*private void Update()
    {
         timePassed += Time.deltaTime;
         if(timePassed > 10f)
            {
                RandomPowerPosition();
            } 
    }*/
    public int RandomPower()
    {
        int Selection = UnityEngine.Random.Range(0, PowerUps.Length);
        return Selection;
    }
    public void ReSpawnPowerUp()
    {
        if (hasSpawn)
        {
            powerUp = Instantiate(PowerUps[RandomPower()]);
            powerUp.transform.position = RandomPowerPosition();
            hasSpawn = false;
        }

    }

     private Vector2 RandomPowerPosition()
    {
        Bounds bounds = gridArea.bounds;

        // spawn at random position on gridarea
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        x = Mathf.Round(x);
        y = Mathf.Round(y);
        Vector2 randomPosition = new Vector2(x, y);
        Debug.Log("entered randompowerposition");
        return randomPosition;
    }

    public void DestroyPowerUp()
    {
        Debug.Log(" destroyed powerup");
        Destroy(powerUp);
    }
}