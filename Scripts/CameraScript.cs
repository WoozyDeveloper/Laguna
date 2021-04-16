using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public int oxDirection;//<0->left, >0->right
    const float oyCameraPosition = 2, ozCameraPosition = 7;//distance camera -> car
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
        transform.position = new Vector3(carMovement.transform.position.x, transform.position.y, carMovement.transform.position.z - 5);
        //transform.position = Vector3.MoveTowards(transform.position, newPosition, 20 * Time.deltaTime);

        
    }
}