using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarMovement : MonoBehaviour
{
    //right, left OX
    //up, down OY
    //front, back OZ
    private const float turnSpeed = 7f;
    CameraScript myCamera;
    private Rigidbody car;
    public float carSpeed = 10;//  10 ->  50[0, 0,  -24]
    //. . . . . . . . . . . . .// 100 -> 220[0, 0, -171]


    void Start()
    {
        Application.targetFrameRate = 160;
        car = GetComponent<Rigidbody>();
        myCamera = FindObjectOfType<CameraScript>();
    }

    void Update()
    {
        car.velocity = new Vector3(0, 0, carSpeed);
        Vector2 mousePos = Input.mousePosition;//touch/mouse position

        //for right direction
        if (Input.GetKey(KeyCode.D) || (Input.GetMouseButton(0) && mousePos.x > Screen.width / 2))
        {
            #region movement of the car to right
            if (car.velocity.z <= 0f)
                car.velocity = new Vector3(0f, 0f, car.velocity.z);
            car.AddForce(new Vector3(turnSpeed, 0f, 0f), ForceMode.Impulse);
            myCamera.oxDirection = 4;
            #endregion
            #region tilt the car to the left
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 15), 10 * Time.deltaTime);
            
            #endregion
        }
        //for left direction
        else if (Input.GetKey(KeyCode.A) || (Input.GetMouseButton(0) && mousePos.x < Screen.width / 2))
        {
            #region movement of the car to left
            if (car.velocity.z >= 0f)
                car.velocity = new Vector3(0f, 0f, car.velocity.z);
            car.AddForce(new Vector3(-turnSpeed, 0f, 0f), ForceMode.Impulse);
            myCamera.oxDirection = -4;
            #endregion
            #region tilt the car to the right
           
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -15), 10 * Time.deltaTime);

            #endregion
        }
        //when there's no action
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 20 * Time.deltaTime);

        //boost / nitro / N20 / TOKYO DRIFT / ETC
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (carSpeed <= 100f)//100 is the max speed
                carSpeed += 0.5f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-20, 0, 0), 10 * Time.deltaTime);
        }
        else//hoo prrr easy easy
            if (carSpeed >= 15f)
            {
                //TODO: LIGHTS!
                carSpeed -= 0.3f;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(20, 0, 0), 10 * Time.deltaTime);
            }
    }

    public void Death()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Debug.Log("Noi codam, dar si cantam");
    }
}