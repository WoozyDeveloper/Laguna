using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    const float oyCameraPosition = 4, ozCameraPosition = 7;//distance camera -> car
    const float xQuat = 0.1f, wQuat = 0.9f;
    private Camera camera;
    private CarMovement carMovement;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        carMovement = FindObjectOfType<CarMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = new Vector3(carMovement.gameObject.transform.position.x,
                                                carMovement.gameObject.transform.position.y + oyCameraPosition,
                                                carMovement.gameObject.transform.position.z - ozCameraPosition);
        camera.transform.rotation = new Quaternion(xQuat, 0, 0, wQuat);
    }
}
