using System.Collections;
using UnityEditor;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public GameObject[] PowerUps;
    private GameObject powerUp;
    [SerializeField] private Collider2D gridArea;
    [HideInInspector] public bool hasSpawn;
    private bool powerUpAlive = false;

    private void Start()
    {
        hasSpawn = true;
        ReSpawnPowerUp();
    }
    
    IEnumerator TimePassed()
    {
        powerUpAlive = true;
        while (powerUpAlive)
        {
            Debug.Log("power Coroutine called");
            yield return new WaitForSeconds(10f);
            //code here will execute after foodLifespan seconds
            if (powerUp != null)
            {
                Debug.Log("power coroutine executed");

                powerUp.transform.position = RandomPosition();
            }
        }
    }
    private int RandomPower()
    {
        int option = UnityEngine.Random.Range(0, PowerUps.Length - 1);
        return option;
    }
    public void ReSpawnPowerUp()
    {
        if (hasSpawn)
        {
            powerUp = Instantiate(PowerUps[RandomPower()]);
            StartCoroutine(TimePassed());
            hasSpawn = false;
        }

    }

    public void DestroyPowerUp()
    {
        powerUpAlive = false;
        Debug.Log(" destroyed powerup");
        Destroy(powerUp);
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