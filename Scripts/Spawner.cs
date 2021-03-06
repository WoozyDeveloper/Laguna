using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const int startingRoadsNum = 5;
    private CarMovement car;
    public GameObject road;

    void Start()
    {
        car = FindObjectOfType<CarMovement>();
        StartLevel();
    }

    void StartLevel()
    {
        Instantiate(road, new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 0));
        Instantiate(road, new Vector3(0f, 0f, 50f), Quaternion.Euler(0, 0, 0));
        Instantiate(road, new Vector3(0f, 0f, 100f), Quaternion.Euler(0, 0, 0));
        Instantiate(road, new Vector3(0f, 0f, 150f), Quaternion.Euler(0, 0, 0));
        Instantiate(road, new Vector3(0f, 0f, 200f), Quaternion.Euler(0, 0, 0));
    }
    void Update()
    {
        if (car.transform.position.z - road.transform.position.z >= road.transform.localScale.z * 10f) 
        {
            Debug.Log("---Spawn---");
            Instantiate(road, new Vector3(road.transform.position.x, road.transform.position.y, road.transform.position.z + startingRoadsNum * 10 * road.transform.localScale.z),
                 Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
