using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCar : MonoBehaviour
{
    private float speedMovement;
    public CarMovement playerCar;
    private Rigidbody currentCar;
    void Start()
    {
        currentCar = GetComponent<Rigidbody>();
        playerCar = FindObjectOfType<CarMovement>();

        speedMovement = Random.Range(0, 10f);
    }


    void Update()
    {
        currentCar.velocity = new Vector3(0f, 0f, speedMovement);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Player")
        //    playerCar.Death();

    }
}