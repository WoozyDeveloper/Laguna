using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    GameObject wheel;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 50;//deg
        wheel = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        wheel.transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
    }
}
