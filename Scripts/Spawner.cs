using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Rigidbody car;
    public GameObject road;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentPosition(car.gameObject) >= CurrentPosition(road) - road.transform.localScale.z)
        {
            Instantiate(road, new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z + 50f),
                Quaternion.Euler(Vector3.zero));
        }
    }

    double CurrentPosition(GameObject obj)
    {
        return obj.transform.position.z;
    }
}
