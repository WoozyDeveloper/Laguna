using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const int startingRoadsNum = 5;
    private CarMovement car;
    private OtherCar otherCar;
    public GameObject road,spawningCar;

    void Start()
    {
        car = FindObjectOfType<CarMovement>();
        otherCar = FindObjectOfType<OtherCar>();
    }

    void Update()
    {
        SpawnTerrain();
        SpawnCars();
    }

    private void SpawnCars()
    {
        if (car.transform.position.z - road.transform.position.z >= road.transform.localScale.z * 10f)
        {
            Instantiate(spawningCar, new Vector3(spawningCar.transform.position.x, spawningCar.transform.position.y, spawningCar.transform.position.z + startingRoadsNum * 10 * road.transform.localScale.z),
                 Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void SpawnTerrain()
    {
        if (car.transform.position.z - road.transform.position.z >= road.transform.localScale.z * 10f)
        {
            Instantiate(road, new Vector3(road.transform.position.x, road.transform.position.y, road.transform.position.z + startingRoadsNum * 10 * road.transform.localScale.z),
                 Quaternion.identity);
            Destroy(gameObject);
        }
    }
}