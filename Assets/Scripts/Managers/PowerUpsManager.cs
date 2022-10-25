using System.Collections;
using UnityEditor;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public GameObject[] PowerUps;
    private GameObject PowerUp;
    private float powerSpawningIn;
    private bool canSpawnPower;
    [SerializeField] private float destroyPowerUpAfter = 10f;

    private void Start()
    {
        canSpawnPower = true;
        PowerSpawningIn();
    }

    // random delay for spawning powerup 
    private void PowerSpawningIn()   
    {
        powerSpawningIn = Random.Range(4, 15);
        Invoke(nameof(SpawnPowerUp), powerSpawningIn);
    }

    // selects any one powerup for spawning
    private int RandomPower()
    {
        int option = UnityEngine.Random.Range(0, PowerUps.Length);
        return option;
    }

    //spawn powerup
    private void SpawnPowerUp()
    {
        if (canSpawnPower)
        {
            PowerUp = Instantiate(PowerUps[RandomPower()]);
            CancelInvoke(nameof(DestroyPowerUp));
            PowerUp.transform.position = GridManager.RandomPosition();
            canSpawnPower = false;
            Invoke(nameof(DestroyPowerUp), destroyPowerUpAfter);  // destroys powerup after not picking up
        }
    }

    //destroy power up
    public void DestroyPowerUp()
    {
        Destroy(PowerUp);
        canSpawnPower = true;
        PowerSpawningIn();
    }

}