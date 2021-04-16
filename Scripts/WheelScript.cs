using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    public GameObject wheel;
    private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = -100;
    }

    // Update is called once per frame
    void Update()
    {
        wheel.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
