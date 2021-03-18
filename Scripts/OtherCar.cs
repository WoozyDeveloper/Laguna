using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCar : MonoBehaviour
{
    bool changed = false;
    public float[] oxPositions = new float[4];
    private float speedMovement;
    private CarMovement playerCar;
    private Rigidbody currentCar;
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
        if (currentCar.velocity.x > 0f)
            currentCar.velocity = new Vector3(0f, 0f, speedMovement);
        else
            currentCar.velocity = new Vector3(0f, 0f, -speedMovement);
        if (currentCar.transform.position.x < 0f) //left side
        {
            float oxPosition;
            Vector3 newPosition = new Vector3(0f, 0f, 0f);
            if(currentCar.transform.position.x == -6f && changed == false) //edge
            {
                changed = true;
                oxPosition = -4f;
                newPosition = new Vector3(oxPosition, currentCar.transform.position.y,
                                            currentCar.transform.position.z);
                currentCar.transform.position = Vector3.MoveTowards(currentCar.transform.position, newPosition, 1 * Time.deltaTime);
            }
            else if(currentCar.transform.position.x == -2f && changed == false)//inside
            {
                changed = true;
                oxPosition = -6f;
                newPosition = new Vector3(oxPosition, currentCar.transform.position.y,
                                            currentCar.transform.position.z);
                currentCar.transform.position = Vector3.MoveTowards(currentCar.transform.position, newPosition, 1 * Time.deltaTime);
            }
           
        }
        else //right side
        {

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