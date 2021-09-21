using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const int startingRoadsNum = 6;
    private CarMovement car;
    public GameObject road;
    [SerializeField] private GameObject money;

    void Start()
    {
        car = FindObjectOfType<CarMovement>();
    }

    void Update()
    {
        SpawnTerrain();
        SpawnMoney(money);
    }

    //spawn money
    private void SpawnMoney(GameObject money)
    {
        int[] possible_positions_ox = new int[] { -6, -2, 2, 6 };
        int choice = Random.Range(0,5);

        Vector3 next_position = new Vector3(possible_positions_ox[choice], 3f, car.transform.position.z + startingRoadsNum * 10 * road.transform.localScale.z);

    }

    //spawn the road
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