using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCar : MonoBehaviour
{
    public float[] oxPositions = new float[4];
    private float speedMovement;
    private CarMovement playerCar;
    private Rigidbody currentCar;
    float oxPosition = 100f;
    int direction;
    void Start()
    {
        currentCar = GetComponent<Rigidbody>();
        playerCar = FindObjectOfType<CarMovement>();

        speedMovement = Random.Range(0, 10f);
        direction = Random.Range(0, 2);
    }


    void Update()
    {
        currentCar.velocity = new Vector3(0f, 0f, speedMovement);
        if(currentCar.transform.position.x != oxPosition&& currentCar.transform.position.z - playerCar.transform.position.z < 40f)
        {
            Vector3 newPosition = new Vector3(0f, 0f, 0f);
            
            if (direction == 0)//switch on right
            {
                if (currentCar.transform.position.x >= 2f && currentCar.transform.position.x < 6f)
                    oxPosition = currentCar.transform.position.x + 4f;
                else
                    oxPosition = currentCar.transform.position.x - 4f;
            }
            else//switch on left
            {
                if (currentCar.transform.position.x <= -2f && currentCar.transform.position.x > -6f)
                    oxPosition = currentCar.transform.position.x - 4f;
                else
                    oxPosition = currentCar.transform.position.x + 4f;
            }
            newPosition = new Vector3(oxPosition, currentCar.transform.position.y,
                                              currentCar.transform.position.z);
            currentCar.transform.position = Vector3.MoveTowards(currentCar.transform.position, newPosition, 5 * Time.deltaTime);
        }

        if (playerCar.transform.position.z - currentCar.transform.position.z >= 20f)
        {
            Debug.Log("---CAR DESTROYED---");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            playerCar.Death();

    }
}