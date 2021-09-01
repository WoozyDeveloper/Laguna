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
    public Button acceleration, brake, left, right;
    private bool accelerationPressed, brakePressed, leftPressed, rightPressed;
    public bool freezeGame;
    public GameObject redArrow, speedometer;
    public float turnSpeed = 10f;
    public float  currentTurnSpeed = 0f;
    private Rigidbody car;
    public float carSpeed = 10, topSpeed;//  10 ->  50[0, 0,  -24]
    //. . . . . . . . . . . . .// 100 -> 220[0, 0, -171]


    void Start()
    {
        #region Init
        //hide the UI for the game
        
        acceleration.gameObject.SetActive(false);
        brake.gameObject.SetActive(false);
        redArrow.gameObject.SetActive(false);
        speedometer.gameObject.SetActive(false);
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(false);


        freezeGame = true;//freeze the game until you start it
        Application.targetFrameRate = 160;
        accelerationPressed = false;//the acceleration
        brakePressed = false;//the brake
        leftPressed = false;//left button
        rightPressed = false;//right button

        car = GetComponent<Rigidbody>();//current car

        /*
         * switch (gameobject.tag)
         * {
         * case 'lambo' : topSpeed = 220;
         *                break;
         * etc etc...
         */
        topSpeed = 80;//change it for every car
        #endregion
    }

    void Update()
    {
        if(!freezeGame)
        {
            if(transform.position.y == .0f)
                car.constraints = RigidbodyConstraints.FreezePositionY;
            if(!acceleration.IsActive())
            {
                acceleration.gameObject.SetActive(true);
                brake.gameObject.SetActive(true);
                redArrow.gameObject.SetActive(true);
                speedometer.gameObject.SetActive(true);
                left.gameObject.SetActive(true);
                right.gameObject.SetActive(true);
            }

            if(FindObjectOfType<CameraScript>().transform.rotation.y == 0f)
                FindObjectOfType<CameraScript>().GetComponent<Animator>().enabled = false;

            //rotation of the arrow from the accelerometer
            redArrow.transform.rotation = Quaternion.Slerp(redArrow.transform.rotation, Quaternion.Euler(0, 0, -carSpeed + 50), Time.deltaTime);

            //velocity of the car
            car.velocity = new Vector3(0, 0, carSpeed);
            
            //touch/mouse position
            Vector2 mousePos = Input.mousePosition;//mouse pos(x,y)

            //for RIGHT direction
            if (Input.GetKey(KeyCode.D) || rightPressed == true)
            {
                #region movement of the car to right

                const float step = 0.08f;

                if(currentTurnSpeed <= 10f)
                    currentTurnSpeed += step;
                car.AddForce(new Vector3(currentTurnSpeed, 0f, 0f), ForceMode.Impulse);

                #endregion
                #region tilt the car to the left
                
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 10, 10), 5 * Time.deltaTime);
                
                #endregion
            }
            //for LEFT direction
            else if (Input.GetKey(KeyCode.A) || leftPressed == true)
            {
                #region movement of the car to left

                const float step = 0.08f;
                if(currentTurnSpeed >= -10f)
                    currentTurnSpeed -= step;
                car.AddForce(new Vector3(currentTurnSpeed, 0f, 0f), ForceMode.Impulse);

                #endregion
                #region tilt the car to the right
            
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -10, -10), 5 * Time.deltaTime);

                #endregion
            }
            //when there's no action
            else
            {
                car.velocity = new Vector3(car.velocity.x, 0, carSpeed);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 8 * Time.deltaTime);
                currentTurnSpeed = 0f;
            }

            //boost / nitro / NOS / TOKYO DRIFT / ETC
            if (Input.GetKey(KeyCode.LeftShift) || accelerationPressed == true)//! ! ! TODO: add a button on the screen
            {
                if (carSpeed <= topSpeed)//100 is the max speed
                    carSpeed += 0.2f;//acceleration
                //tilt
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-10, this.transform.rotation.y, this.transform.rotation.z), 10 * Time.deltaTime);
                //scale of the pedal
                if (acceleration.transform.localScale.y >= 0.8f)
                    acceleration.transform.localScale = new Vector2(acceleration.transform.localScale.x, acceleration.transform.localScale.y - 0.01f);
            }
            //brake / stop / hooo prrrr prrr hard
            else if(Input.GetKey(KeyCode.Space) || brakePressed == true)
            {
                if(carSpeed >= 12f)
                    carSpeed -= .5f;
                if(brake.transform.localScale.y >= 0.5f)
                    brake.transform.localScale = new Vector2(brake.transform.localScale.x, brake.transform.localScale.y - 0.02f);
            }
            else//no buttons pressed
            {
                //scale of the acceleration pedal
                if (acceleration.transform.localScale.y <= 1f)
                    acceleration.transform.localScale = new Vector2(acceleration.transform.localScale.x, acceleration.transform.localScale.y + 0.02f);
                //scale of the brake pedal
                if (brake.transform.localScale.y <= 0.8f)
                    brake.transform.localScale = new Vector2(brake.transform.localScale.x, brake.transform.localScale.y + 0.02f);

                if (carSpeed >= 15f)
                {
                    //TODO: LIGHTS!

                    //deceleration
                    carSpeed -= .05f;
                    //tilt
                    this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(10, 0, 0), 10 * Time.deltaTime);
                }
            }
        }

    }

    //acceleration
    public void AccelerationPressed()
    {
        accelerationPressed = true;
    }
    public void AccelerationReleased()
    {
        accelerationPressed = false;
    }

    //brake
    //TODO: LIGHTS ON BrakePressed
    public void BrakePressed()
    {
        brakePressed = true;
    }
    public void BrakeReleased()
    {
        brakePressed = false;
    }

    //left
    public void LeftPressed()
    {
        leftPressed = true;
    }
    public void LeftReleased()
    {
        leftPressed = false;
    }

    //right
    public void RightPressed()
    {
        rightPressed = true;
    }
    public void RightReleased()
    {
        rightPressed = false;
    }
    public void Death()
    {
        //add a pop-up with score etc. ...
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Debug.Log("Noi codam, dar si cantam");
    }
}