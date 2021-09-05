using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private bool buttonPressed;
    private const int numberOfCars = 16, oxRotation = 90;
    private int currentCar;
    public GameObject[] carModels = new GameObject[numberOfCars];
    public GameObject door;

    void Start()
    {
        currentCar = 1;
        buttonPressed = false;
    }

    void Update()
    {

        if (door.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("open_door_animation") && buttonPressed == true)
        {
            foreach (Transform child in transform) 
            {
                GameObject.Destroy(child.gameObject);
            }
            int car_choice = Random.Range(0, numberOfCars - 1);//choose a car mesh
            GameObject obj = Instantiate(carModels[car_choice], transform.position, transform.rotation);
            obj.transform.parent = this.transform;
            buttonPressed = false;
        }
    }

    public void RightPress()
    {
        buttonPressed = true;
        //door.transform.rotation = Quaternion.Euler(90f,-90f,90f);
        if(currentCar < numberOfCars)
            currentCar++;
    }

    public void LeftPress()
    {
       buttonPressed = true;
        if(currentCar > 0)
            currentCar--;
    }

}
