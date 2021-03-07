using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCar : MonoBehaviour
{
    private float speedMovement;
    private CarMovement playerCar;
    private Rigidbody currentCar;
    int num;
    void Start()
    {
        currentCar = GetComponent<Rigidbody>();
        playerCar = FindObjectOfType<CarMovement>();

        speedMovement = Random.Range(0, 10f);
        num = Random.Range(1, 20);
    }


    void Update()
    {
        //TODO: add a method to spawn the cars + switch b 1,2,3 ???
        currentCar.velocity = new Vector3(0f, 0f, speedMovement);
        if(num %2 ==0 && currentCar.transform.position.x < 5.5f && currentCar.transform.position.z - playerCar.transform.position.z < 40f)
        {
            Vector3 newPosition = new Vector3(currentCar.transform.position.x + 1f,
                                          currentCar.transform.position.y,
                                          currentCar.transform.position.z);
            currentCar.transform.position = Vector3.MoveTowards(transform.position, newPosition, 5 * Time.deltaTime);
        }
        if (playerCar.transform.position.z - this.transform.position.z >= 20f)
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