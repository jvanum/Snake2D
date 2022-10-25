using UnityEngine;
using System.Collections;
using UnityEditor;

public enum FoodsTypes
{
    PositiveFood,
    NegativeFood,
}
public class Foods : MonoBehaviour
{
    public FoodsTypes foodsTypes;

    private void Start()
    {

    }

}


//items should not spawn on snake
//if snake is small don't spawn negative food
//random spawn of power ups on random time interval
//random spawn of food on random time interval
//spawns should disappear after some idle time
//dual player, snake should kill each other