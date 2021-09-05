using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private const int numberOfCars = 16, oxRotation = 90;
    private int currentCar;
    public GameObject[] carModels = new GameObject[numberOfCars];
    public GameObject door;

    void Start()
    {
        currentCar = 1;
    }

    public void RightPress()
    {
        if(currentCar < numberOfCars)
            currentCar++;
        StartCoroutine(Coroutine());
        Destroy(this);
        int car_choice = Random.Range(0, numberOfCars - 1);//choose a car mesh
        GameObject obj = Instantiate(carModels[car_choice], transform.position, transform.rotation);
        obj.transform.parent = this.transform;
    }

    public void LeftPress()
    {
        if(currentCar > 0)
            currentCar--;
        StartCoroutine(Coroutine());
        Destroy(this);
        int car_choice = Random.Range(0, numberOfCars - 1);//choose a car mesh
        GameObject obj = Instantiate(carModels[car_choice], transform.position, transform.rotation);
        obj.transform.parent = this.transform;
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(5);
    }
}
