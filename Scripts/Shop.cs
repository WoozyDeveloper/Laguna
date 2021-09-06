using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private bool buttonPressed;
    private const int numberOfCars = 16, oxRotation = 90;
    [SerializeField] private int currentCar;
    public GameObject[] carModels = new GameObject[numberOfCars];
    public GameObject door;

    void Start()
    {
        currentCar = 0;
        buttonPressed = false;
    }

    void Update()
    {
        if(FindObjectOfType<CarMovement>().transform.position.z > 0)
            return;
        if (door.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("open_door_animation") && buttonPressed == true)
        {
            foreach (Transform child in transform) 
            {
                GameObject.Destroy(child.gameObject);
            }
            GameObject obj = Instantiate(carModels[currentCar], transform.position, transform.rotation);
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
