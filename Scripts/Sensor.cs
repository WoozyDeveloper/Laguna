using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    int result;
    GameObject currentArea;
    void Start()
    {
        currentArea = GetComponent<GameObject>();
        result = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Physics.IgnoreCollision(transform.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        if (transform.tag == "LeftSensor")
        {
            if (collision.gameObject.tag == "OtherCar")
                result = 0;
            result = -1;
        }
        else if (transform.tag == "RightSensor")
        {
            if (collision.gameObject.tag == "OtherCar")
                result = 0;
            result = 1;
        }
        else
            Debug.LogError("Sensor tag problem");
        result = 0;
    }

    public int SensorResult()
    {
        return result;
    }
}
