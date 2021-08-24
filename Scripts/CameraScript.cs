using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    const float oyCameraPosition = 6, ozCameraPosition = 8;//distance camera -> car
    const float xQuat = 0.1f, wQuat = 0.9f;
    private Camera cam;
    private CarMovement carMovement;

    void Start()
    {
        cam = GetComponent<Camera>();
        carMovement = FindObjectOfType<CarMovement>();
        cam.transform.rotation = new Quaternion(xQuat, 0, 0, wQuat);
    }

    void Update()
    {
        transform.position = new Vector3(carMovement.transform.position.x, transform.position.y, carMovement.transform.position.z - ozCameraPosition);
        //transform.position = Vector3.MoveTowards(transform.position, newPosition, 20 * Time.deltaTime);

        
    }
}