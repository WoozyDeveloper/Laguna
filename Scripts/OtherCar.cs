using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OtherCar : MonoBehaviour
{
    #region variables
    public BlinkerScript leftBlinker,rightBlinker;//for the blinker

    bool changedToLeft = false, changedToRight = false;//used in change lane to see if the car already switched a lane or not
    public float spawnDistance;//spawning distance of the cars
    private float speedMovement;//movement of the cars with random value (see Start())
    private CarMovement playerCar;
    private Rigidbody currentCar;
    private int wannaChangeTheLane;

    public Sensors rightSensor, leftSensor;//sensors for switching lanes vrum vrum!!!
    #endregion
    void Start()
    {
        #region Basic Inits

        spawnDistance = 250f;//behind your car <--250f--> in front unseen

        currentCar = GetComponent<Rigidbody>();
        playerCar = FindObjectOfType<CarMovement>();

        speedMovement = Random.Range(5f, 15f);

        //orientation of the cars
        if (transform.position.x < 0f)
            transform.eulerAngles = new Vector3(0, -180, 0);

        //can go only to the right lane
        if (transform.position.x == 2f || transform.position.x == -6f)
            changedToRight = true;

        //can go only to the left lane
        else if (transform.position.x == 6f || transform.position.x == -2f)
            changedToLeft = true;
        #endregion

        //for P = 1/5
        wannaChangeTheLane = -1;// cu P = 1/6
    }

    void Update()
    {
        #region Car positioning
        if (transform.position.x == 2f || transform.position.x == -6f)
            changedToRight = true;
        if (transform.position.x == 6f || transform.position.x == -2f)
            changedToLeft = true;
        #endregion

        #region Movement of the car
        if (currentCar.transform.position.x >= 0f)
            currentCar.velocity = new Vector3(0, 0, speedMovement);
        else
            currentCar.velocity = new Vector3(0, 0, -speedMovement);
        #endregion

        if (wannaChangeTheLane < 0 && Mathf.Abs(currentCar.transform.position.z - playerCar.transform.position.z) <= 20f)
        {
            ChangeLane();
            Debug.Log("Caram cantam! Noi caram, dar si cantam!");
            // leftBlinker.stopBlinking();
            // rightBlinker.stopBlinking();
        }

        RespawnCar();
    }

    private int Aproximate(float oxPosition)
    {
        float[] diff = new float[4];
        int[] positions = new int[] { -6, -2, 2, 6 };

        for (int index = 0; index < 4; index++)
            diff[index] = Mathf.Abs(oxPosition - positions[index]);

        float minimum_dif = float.MaxValue;
        int good_index = -1;

        //search the minimum difference and save the closest index in good_index
        for (int index = 0; index < 4; index++)
            if (diff[index] < minimum_dif)
            {
                minimum_dif = diff[index];
                good_index = index;
            }
        return positions[good_index];
    }

    private void RespawnCar()//cars are placed on OX at [-6, -2, 2, 6].
    {
        Vector3 oldPosition = currentCar.transform.position;
        if (playerCar.transform.position.z - currentCar.transform.position.z >= 5f)
        {
            Instantiate(currentCar, new Vector3(Aproximate(oldPosition.x), oldPosition.y, oldPosition.z + spawnDistance), Quaternion.Euler(0f, 0f, 0f)); ;
            Destroy(gameObject);
        }
    }

    private void ChangeLane()
    {
        if (currentCar.transform.position.x >= 0f)//right side of the road
        {
            if (currentCar.transform.position.x <= 6f && changedToRight == true)
            {
                rightBlinker.changeState();
                float oxPosition = 6f;
                Vector3 newPosition = new Vector3(oxPosition, this.transform.position.y, this.transform.position.z);
                this.transform.position = Vector3.MoveTowards(this.transform.position, newPosition, 2 * Time.deltaTime);
            }
            else if (currentCar.transform.position.x >= 2f && changedToLeft == true)
            {
                leftBlinker.changeState();
                float oxPosition = 2f;
                Vector3 newPosition = new Vector3(oxPosition, this.transform.position.y, transform.position.z);
                this.transform.position = Vector3.MoveTowards(this.transform.position, newPosition, 2 * Time.deltaTime);
            }
        }
        else//left side of the road
        {
            if (currentCar.transform.position.x <= -2f && changedToRight == true)
            {
                rightBlinker.changeState();
                float oxPosition = -2f;
                Vector3 newPosition = new Vector3(oxPosition, this.transform.position.y, this.transform.position.z);
                this.transform.position = Vector3.MoveTowards(this.transform.position, newPosition, 2 * Time.deltaTime);
            }
            else if (currentCar.transform.position.x >= -6f && changedToLeft == true)
            {
                leftBlinker.changeState();
                float oxPosition = -6f;
                Vector3 newPosition = new Vector3(oxPosition, this.transform.position.y, this.transform.position.z);
                this.transform.position = Vector3.MoveTowards(this.transform.position, newPosition, 2 * Time.deltaTime);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "OtherCar" && collision.gameObject.transform.position.z > this.transform.position.z)
        {
            wannaChangeTheLane = -1;
            ChangeLane();
            
        }
        else if (collision.gameObject.tag == "Player")
            playerCar.Death();
    }
}