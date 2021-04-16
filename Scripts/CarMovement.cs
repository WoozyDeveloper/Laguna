using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    //right, left OX
    //up, down OY
    //front, back OZ
    CameraScript myCamera;
    private Rigidbody car;
    public float carSpeed;

    void Start()
    {
        Application.targetFrameRate = 160;
        car = GetComponent<Rigidbody>();
        myCamera = FindObjectOfType<CameraScript>();
    }

    void Update()
    {
        car.velocity = new Vector3(0, 0, 2 * carSpeed);
        Vector3 mousePos = Input.mousePosition;
        if (Input.GetKey(KeyCode.D) || (Input.GetMouseButton(0) && mousePos.x > Screen.width / 2))
        {
            if (car.velocity.z <= 0f)
                car.velocity = new Vector3(0f, 0f, car.velocity.z);
            car.AddForce(new Vector3(carSpeed / 2f, 0f, 0f), ForceMode.Impulse);
            myCamera.oxDirection = 4;
        }
        else if (Input.GetKey(KeyCode.A) || (Input.GetMouseButton(0) && mousePos.x < Screen.width / 2))
        {
            if (car.velocity.z >= 0f)
                car.velocity = new Vector3(0f, 0f, car.velocity.z);
            car.AddForce(new Vector3(-carSpeed / 2f, 0f, 0f), ForceMode.Impulse);
            myCamera.oxDirection = -4;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            car.velocity = new Vector3(0, 0, 10 * carSpeed);
        }
    }

    public void Death()
    {
        this.transform.position = new Vector3(0f, 0f, 0f);
    }
}