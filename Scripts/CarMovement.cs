using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class CarMovement : MonoBehaviour //IUnityAdsInitializationListener,IUnityAdsLoadListener,IUnityAdsShowListener
{
    //right, left OX
    //up, down OY
    //front, back OZ

    [SerializeField] private AudioSource engineSound;
    private int currentGameIndex;
    public Button acceleration, brake, left, right;
    private bool accelerationPressed, brakePressed, leftPressed, rightPressed;
    public bool freezeGame;
    public GameObject redArrow, speedometer;

    const float step = 0.04f;
    private const int tilt_value = 4;
    public float turnSpeed = 10f;
    public float  currentTurnSpeed = 0f;
    private Rigidbody car;
    public float carSpeed = 10, topSpeed;//  10 ->  50[0, 0,  -24]
    //. . . . . . . . . . . . .// 100 -> 220[0, 0, -171]




    void Start()
    {

        engineSound = GetComponent<AudioSource>();
        //Advertisement.Initialize("4881923", true, this);

        #region Init
        PlayerPrefs.SetInt("0", 1);//first car is available

        currentGameIndex = PlayerPrefs.GetInt("game_index") + 1;
        Debug.Log("CURRENT = " + currentGameIndex);
        if (currentGameIndex > 5)
        {
            currentGameIndex = 0;


            /*Advertisement.Load("BetweenLevels", this);
            Advertisement.Show("BetweenLevels");
            if (Advertisement.isShowing)
            {
                PlayerPrefs.SetInt("money_key", PlayerPrefs.GetInt("money_key") + 1000);
            }*/
            
        }
        else
            currentGameIndex++;

        PlayerPrefs.SetInt("game_index", currentGameIndex);

        Time.timeScale = 1f;
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
            if (!engineSound.isPlaying)
                engineSound.Play();
            engineSound.pitch = carSpeed / 10 * 2;


            if (transform.position.z >= -20.0f && transform.position.z <= -15.0f)
            {
                car.transform.position = new Vector3(car.transform.position.x, 1.0f, car.transform.position.z);
                car.constraints = RigidbodyConstraints.FreezePositionY;
            }

            if(transform.position.z >= -20.0f)
                car.transform.position = new Vector3(car.transform.position.x, 1.0f, car.transform.position.z);

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


                if(currentTurnSpeed <= 10f)
                    currentTurnSpeed += step;
                car.AddForce(new Vector3(currentTurnSpeed, 0f, 0f), ForceMode.Impulse);

                #endregion
                #region tilt the car to the left
                
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, tilt_value, tilt_value), 5 * Time.deltaTime);
                
                #endregion
            }
            //for LEFT direction
            else if (Input.GetKey(KeyCode.A) || leftPressed == true)
            {
                #region movement of the car to left

                if(currentTurnSpeed >= -10f)
                    currentTurnSpeed -= step;
                car.AddForce(new Vector3(currentTurnSpeed, 0f, 0f), ForceMode.Impulse);

                #endregion
                #region tilt the car to the right
            
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -tilt_value, -tilt_value), 5 * Time.deltaTime);

                #endregion
            }
            //when there's no action
            else
            {
                car.AddForce(new Vector3(currentTurnSpeed, 0f, 0f), ForceMode.Impulse);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 8 * Time.deltaTime);

                if (currentTurnSpeed <= 0 && currentTurnSpeed >= -10)
                    currentTurnSpeed += step;
                else if (currentTurnSpeed >= 0 && currentTurnSpeed <= 10)
                    currentTurnSpeed -= step;
            }

            //boost / nitro / NOS / TOKYO DRIFT / ETC
            if (Input.GetKey(KeyCode.LeftShift) || accelerationPressed == true)
            {
                if (carSpeed <= topSpeed)//100 is the max speed
                    carSpeed += 0.05f;//acceleration value
                //tilt
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-tilt_value, this.transform.rotation.y, this.transform.rotation.z), 10 * Time.deltaTime);
                
                //acceleration pedal deformation
                if (acceleration.transform.localScale.y >= 0.8f)
                    acceleration.transform.localScale = new Vector2(acceleration.transform.localScale.x, acceleration.transform.localScale.y - 0.01f);
            }
            //brake / stop / hooo prrrr prrr hard
            else if(Input.GetKey(KeyCode.Space) || brakePressed == true)
            {
                if (carSpeed >= 12f)
                {
                    this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler((float)tilt_value / 2, this.transform.rotation.y, this.transform.rotation.z), 10 * Time.deltaTime);
                    carSpeed -= .20f;//brake value
                }

                //brake pedal deformation
                if(brake.transform.localScale.y >= 0.5f)
                    brake.transform.localScale = new Vector2(brake.transform.localScale.x, brake.transform.localScale.y - 0.02f);
            }
            else//no buttons pressed
            {
                //scale of the acceleration pedal
                if (acceleration.transform.localScale.y <= 1.7f)
                    acceleration.transform.localScale = new Vector2(acceleration.transform.localScale.x, acceleration.transform.localScale.y + 0.02f);
                //scale of the brake pedal
                if (brake.transform.localScale.y <= 1.15f)
                    brake.transform.localScale = new Vector2(brake.transform.localScale.x, brake.transform.localScale.y + 0.02f);

                if (carSpeed >= 15f)
                {
                    //TODO: LIGHTS!

                    //deceleration
                    carSpeed -= .05f;
                    //tilt
                    this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 10 * Time.deltaTime);
                }
            }
        }

    }

    #region Trigger Event Buttons
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
    #endregion
    public void Death()
    {
        //add a pop-up with score etc. ...
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

   
}