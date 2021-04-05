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
    public float spawnDistance;//spawning distance of the cars
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

        //orientation of the cars
        if (transform.position.x < 0f)
            transform.rotation = new Quaternion(-1, 0, 0, 0);

        //can go only to the right lane
        if (transform.position.x == 2f || transform.position.x == -6f)
            changedToRight = true;

        //can go only to the left lane
        else if (transform.position.x == 6f || transform.position.x == -2f)
            changedToLeft = true;
        #endregion
        //for P = 1/5
        wannaChangeTheLane = Random.Range(-1, 4);
    }

    void Update()
    {
        if(currentCar.transform.position.x >= 0f)
            currentCar.velocity = new Vector3(0, 0, speedMovement);
        else
            currentCar.velocity = new Vector3(0, 0, -speedMovement);
        if (wannaChangeTheLane < 0 && currentCar.transform.position.z - playerCar.transform.position.z <= 20f)
            ChangeLane();

        RespawnCar();
    }

    private void RespawnCar()
    {
        Vector3 oldPosition = currentCar.transform.position;
        if (playerCar.transform.position.z - currentCar.transform.position.z >= 20f)
        {
            Instantiate(currentCar, new Vector3(oldPosition.x, oldPosition.y, oldPosition.z + spawnDistance), Quaternion.Euler(0f,0f,0f));
            Destroy(gameObject);
        }
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
        if (collision.gameObject.tag == "OtherCar")
        {
            if(currentCar.velocity.z > collision.rigidbody.velocity.z)
                currentCar.velocity = new Vector3(currentCar.velocity.x, currentCar.velocity.y, currentCar.velocity.z - 5f);
            else
                currentCar.velocity = new Vector3(currentCar.velocity.x, currentCar.velocity.y, currentCar.velocity.z + 5f);
        }
        else if (collision.gameObject.tag == "Player")
            playerCar.Death();
        
    }
}