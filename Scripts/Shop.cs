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

    // Update is called once per frame
    void Update()
    {
        Debug.LogError(door.transform.position.x);
        if((int)door.transform.rotation.x + 1 == oxRotation)
        {
            Debug.LogError("AM AJUNS LA 90");
            currentCar++;
            Destroy(this);
            int car_choice = Random.Range(0, numberOfCars - 1);//choose a car mesh
            GameObject obj = Instantiate(carModels[car_choice], transform.position, transform.rotation);
            obj.transform.parent = this.transform;
        }
    }
}
