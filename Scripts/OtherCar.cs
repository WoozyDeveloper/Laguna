using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCar : MonoBehaviour
{
    private float speedMovement;
    private CarMovement playerCar;
    private Rigidbody currentCar;
    void Start()
    {
        currentCar = GetComponent<Rigidbody>();
        playerCar = FindObjectOfType<CarMovement>();

        speedMovement = Random.Range(0, 10f);
    }


    void Update()
    {
        //TODO: add a method to spawn the cars
        currentCar.velocity = new Vector3(0f, 0f, speedMovement);
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