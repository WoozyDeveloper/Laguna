using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const int startingRoadsNum = 6;
    private CarMovement car;
    public GameObject road;

    void Start()
    {
        car = FindObjectOfType<CarMovement>();
    }

    void Update()
    {
        SpawnTerrain();
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