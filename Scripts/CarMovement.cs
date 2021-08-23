using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{
    //right, left OX
    //up, down OY
    //front, back OZ
    public Button acceleration;
    private bool buttonPressed;
    public GameObject redArrow;
    private const float turnSpeed = 7f;
    CameraScript myCamera;
    private Rigidbody car;
    public float carSpeed = 10, topSpeed;//  10 ->  50[0, 0,  -24]
    //. . . . . . . . . . . . .// 100 -> 220[0, 0, -171]


    void Start()
    {
        //Time.timeScale = 0.0f;//stop the game until you press start

        Application.targetFrameRate = 160;//target frame rate
        buttonPressed = false;
        car = GetComponent<Rigidbody>();
        myCamera = FindObjectOfType<CameraScript>();

        /*
         * switch (gameobject.tag)
         * {
         * case 'lambo' : topSpeed = 220;
         *                break;
         * etc etc...
         */
        topSpeed = 80;//change it for every car
    }

    void Update()
    {
        //rotation of the arrow from the accelerometer
        redArrow.transform.rotation = Quaternion.Slerp(redArrow.transform.rotation, Quaternion.Euler(0, 0, -carSpeed + 50), Time.deltaTime);

        //velocity of the car
        car.velocity = new Vector3(0, 0, carSpeed);
        
        //touch/mouse position
        Vector2 mousePos = Input.mousePosition;

        if (Input.GetMouseButton(0) && mousePos.x > Screen.width / 2 && mousePos.y < 190)
        {
            buttonPressed = true;
        }

        //for RIGHT direction
        if (Input.GetKey(KeyCode.D) || (mousePos.x > Screen.width / 2 && Input.GetMouseButton(0) && buttonPressed == false))
        {
            Debug.Log("DREAPTA");
            #region movement of the car to right
            if (car.velocity.z <= 0f)
                car.velocity = new Vector3(0f, 0f, car.velocity.z);
            car.AddForce(new Vector3(turnSpeed, 0f, 0f), ForceMode.Impulse);
            myCamera.oxDirection = 4;
            #endregion
            #region tilt the car to the left
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 7), 2 * Time.deltaTime);
            
            #endregion
        }
        //for LEFT direction
        if (Input.GetKey(KeyCode.A) || (Input.GetMouseButton(0) && mousePos.x < Screen.width / 2))
        {
            Debug.Log("STANGA");
            #region movement of the car to left
            if (car.velocity.z >= 0f)
                car.velocity = new Vector3(0f, 0f, car.velocity.z);
            car.AddForce(new Vector3(-turnSpeed, 0f, 0f), ForceMode.Impulse);
            myCamera.oxDirection = -4;
            #endregion
            #region tilt the car to the right
           
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -7), 2 * Time.deltaTime);

            #endregion
        }
        //when there's no action
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 20 * Time.deltaTime);

        //boost / nitro / NOS / TOKYO DRIFT / ETC
        if (Input.GetKey(KeyCode.LeftShift) || buttonPressed == true)//! ! ! TODO: add a button on the screen
        {
            if (carSpeed <= topSpeed)//100 is the max speed
                carSpeed += 0.2f;//acceleration
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-10, 0, 0), 10 * Time.deltaTime);
            if (acceleration.transform.localScale.y >= 0.8f)
                acceleration.transform.localScale = new Vector2(acceleration.transform.localScale.x, acceleration.transform.localScale.y - 0.01f);
        }
        else//hoo prrr easy easy
        {
            if (acceleration.transform.localScale.y <= 1f)
                acceleration.transform.localScale = new Vector2(acceleration.transform.localScale.x, acceleration.transform.localScale.y + 0.02f);

            if (carSpeed >= 15f)
            {
                //TODO: LIGHTS!
                carSpeed -= 0.3f;//deceleration
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(10, 0, 0), 10 * Time.deltaTime);
            }
        }
        buttonPressed = false;

    }

    public void Death()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Debug.Log("Noi codam, dar si cantam");
    }
}