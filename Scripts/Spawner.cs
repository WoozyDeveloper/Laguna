using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private int roadNumber;
    private CarMovement car;
    public GameObject road1, road2;

    void Start()
    {
        roadNumber = 1;
        car = FindObjectOfType<CarMovement>();
    }

    void Update()
    {
        if (car.transform.position.z - road1.transform.position.z >= 50f) 
        {
            if (roadNumber % 2 == 1) //road1
            {
                Instantiate(road1, new Vector3(road1.transform.position.x, road1.transform.position.y, car.transform.position.z + roadNumber * 50f),
                    Quaternion.identity);
                Destroy(road1.gameObject);
            }
            else //road2
            {
                Instantiate(road2, new Vector3(road1.transform.position.x, road1.transform.position.y, car.transform.position.z + roadNumber * 50f),
                    Quaternion.identity);
                Destroy(road2.gameObject);
            }
            roadNumber++;
        }
    }
}
