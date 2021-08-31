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
    public bool freezeGame;
    public GameObject redArrow, speedometer;
    public float turnSpeed = 10f;
    public float  currentTurnSpeed = 0f;
    private Rigidbody car;
    public float carSpeed = 10, topSpeed;//  10 ->  50[0, 0,  -24]
    //. . . . . . . . . . . . .// 100 -> 220[0, 0, -171]


    void Start()
    {

        //Time.timeScale = 0.0f;//stop the game

        //hide the UI for the game
        acceleration.gameObject.SetActive(false);
        redArrow.gameObject.SetActive(false);
        speedometer.gameObject.SetActive(false);


        freezeGame = true;//freeze the game until you start it
        Application.targetFrameRate = 160;
        buttonPressed = false;//the acceleration

        car = GetComponent<Rigidbody>();//current car

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
        if(!freezeGame)
        {
            if(!acceleration.IsActive())
            {
                acceleration.gameObject.SetActive(true);
                redArrow.gameObject.SetActive(true);
                speedometer.gameObject.SetActive(true);
            }

            if(FindObjectOfType<CameraScript>().transform.rotation.y == 0f)
                FindObjectOfType<CameraScript>().GetComponent<Animator>().enabled = false;

            //rotation of the arrow from the accelerometer
            redArrow.transform.rotation = Quaternion.Slerp(redArrow.transform.rotation, Quaternion.Euler(0, 0, -carSpeed + 50), Time.deltaTime);

            //velocity of the car
            car.velocity = new Vector3(0, 0, carSpeed);
            
            //touch/mouse position
            Vector2 mousePos = Input.mousePosition;//mouse pos(x,y)

            if (Input.GetMouseButton(0) && mousePos.x > Screen.width / 2 && mousePos.y < 190)//if you press the acceleration for bottom right
            {
                //TODO: add sound
                buttonPressed = true;
            }

            //for RIGHT direction
            if (Input.GetKey(KeyCode.D) || (mousePos.x > Screen.width / 2 && Input.GetMouseButton(0) && buttonPressed == false))
            {
                #region movement of the car to right

                const float step = 0.08f;
                car.AddForce(new Vector3(currentTurnSpeed += step, 0f, 0f), ForceMode.Impulse);

                #endregion
                #region tilt the car to the left
                
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 15, 10), 5 * Time.deltaTime);
                
                #endregion
            }
            //for LEFT direction
            else if (Input.GetKey(KeyCode.A) || (Input.GetMouseButton(0) && mousePos.x < Screen.width / 2))
            {
                #region movement of the car to left

                const float step = 0.08f;
                car.AddForce(new Vector3(-(currentTurnSpeed += step), 0f, 0f), ForceMode.Impulse);

                #endregion
                #region tilt the car to the right
            
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -15, -10), 5 * Time.deltaTime);

                #endregion
            }
            //when there's no action
            else
            {
                float descendingTurn = car.velocity.x;
                bool sign = descendingTurn > 0;
                car.velocity = new Vector3(0, 0, carSpeed);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 8 * Time.deltaTime);
                currentTurnSpeed = 0f;
            }

            //boost / nitro / NOS / TOKYO DRIFT / ETC
            if (Input.GetKey(KeyCode.LeftShift) || buttonPressed == true)//! ! ! TODO: add a button on the screen
            {
                if (carSpeed <= topSpeed)//100 is the max speed
                    carSpeed += 0.2f;//acceleration
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-10, this.transform.rotation.y, this.transform.rotation.z), 10 * Time.deltaTime);
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
                    this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(10, 0, 0), 10 * Time.deltaTime);
                }
            }
            buttonPressed = false;
        }

    }

    public void Death()
    {
        //add a pop-up with score etc. ...
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Debug.Log("Noi codam, dar si cantam");
    }
}