using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    const float oyCameraPosition = 4, ozCameraPosition = 7;//distance camera -> car
    const float xQuat = 0.1f, wQuat = 0.9f;
    private Camera cam;
    private CarMovement carMovement;

    void Start()
    {
        cam = GetComponent<Camera>();
        carMovement = FindObjectOfType<CarMovement>();
    }

    void Update()
    {
        cam.transform.position = new Vector3(carMovement.gameObject.transform.position.x,
                                                carMovement.gameObject.transform.position.y + oyCameraPosition,
                                                carMovement.gameObject.transform.position.z - ozCameraPosition);
        cam.transform.rotation = new Quaternion(xQuat, 0, 0, wQuat);
    }
}
