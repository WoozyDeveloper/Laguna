using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OtherCar : MonoBehaviour
{
    Scene currentScene;
    private float additionalSpeed;
    bool changedToLeft = false;
    bool changedToRight = false;
    public float[] oxPositions = new float[4];
    private float speedMovement;
    private CarMovement playerCar;
    private Rigidbody currentCar;
    int direction;
    void Start()
    {
        additionalSpeed = 0f;

        currentCar = GetComponent<Rigidbody>();
        playerCar = FindObjectOfType<CarMovement>();

        speedMovement = Random.Range(0, 10f);
        direction = Random.Range(0, 2);

        if (transform.position.x == 2f)
            changedToRight = true;
        else if (transform.position.x == 6f)
            changedToLeft = true;
    }

    void Update()
    {
        currentCar.velocity = new Vector3(0, 0, speedMovement);
        if(currentCar.transform.position.z - playerCar.transform.position.z <= 20f)
            ChangeLane();

        if (playerCar.transform.position.z - currentCar.transform.position.z >= 20f)
        {
            Debug.Log("---CAR DESTROYED---");
            Destroy(gameObject);
        }
    }

    private void ChangeLane()
    {
        if (currentCar.transform.position.x >= 0f)
            if (currentCar.transform.position.x <= 6f && changedToRight == true)
            {
                float oxPosition = 6f;
                Vector3 newPosition = new Vector3(oxPosition, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, newPosition, 2 * Time.deltaTime);
            }
            else if(currentCar.transform.position.x >= 2f && changedToLeft == true)
            {
                float oxPosition = 2f;
                Vector3 newPosition = new Vector3(oxPosition, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, newPosition, 2 * Time.deltaTime);
            }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            playerCar.Death();

    }
}