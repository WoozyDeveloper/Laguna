using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OtherCar : MonoBehaviour
{
    #region variables
    Scene currentScene;
    bool changedToLeft = false, changedToRight = false;//used in change lane to see if the car already switched a lane or not
    public float[] oxPositions = new float[4];//main positions on ox for the cars
    private float speedMovement;//movement of the cars with random value (see Start())
    private CarMovement playerCar;
    private Rigidbody currentCar;
    private int wannaChangeTheLane;
    #endregion
    void Start()
    {
        #region Basic Inits
        currentCar = GetComponent<Rigidbody>();
        playerCar = FindObjectOfType<CarMovement>();

        speedMovement = Random.Range(0, 10f);

        if (transform.position.x < 0f)
            transform.rotation = new Quaternion(-1, 0, 0, 0);

        if (transform.position.x == 2f || transform.position.x == -6f)
            changedToRight = true;//can go only to the right lane
        else if (transform.position.x == 6f || transform.position.x == -2f)
            changedToLeft = true;//can go only to the left lane
        #endregion
        wannaChangeTheLane = Random.Range(-1, 4);//for P = 1/5
    }

    void Update()
    {
        currentCar.velocity = new Vector3(0, 0, speedMovement);
        if(wannaChangeTheLane < 0 && currentCar.transform.position.z - playerCar.transform.position.z <= 20f)
            ChangeLane();

        if (playerCar.transform.position.z - currentCar.transform.position.z >= 20f)
            Destroy(gameObject);
    }

    private void ChangeLane()
    {
        if (currentCar.transform.position.x >= 0f)//right side of the road
        {
            if (currentCar.transform.position.x <= 6f && changedToRight == true)
            {
                float oxPosition = 6f;
                Vector3 newPosition = new Vector3(oxPosition, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, newPosition, 2 * Time.deltaTime);
            }
            else if (currentCar.transform.position.x >= 2f && changedToLeft == true)
            {
                float oxPosition = 2f;
                Vector3 newPosition = new Vector3(oxPosition, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, newPosition, 2 * Time.deltaTime);
            }
        }
        else//left side of the road
        {
            if (currentCar.transform.position.x <= -2f && changedToRight == true)
            {
                float oxPosition = -2f;
                Vector3 newPosition = new Vector3(oxPosition, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, newPosition, 2 * Time.deltaTime);
            }
            else if (currentCar.transform.position.x >= -6f && changedToLeft == true)
            {
                float oxPosition = -6f;
                Vector3 newPosition = new Vector3(oxPosition, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, newPosition, 2 * Time.deltaTime);
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            playerCar.Death();
    }
}