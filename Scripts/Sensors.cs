using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensors : MonoBehaviour
{
    // Start is called before the first frame update
    Collider currentCollider;
    public bool inside;
    void Start()
    {
        currentCollider = GetComponent<Collider>();
        inside = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        inside = true;
    }
    private void OnTriggerExit(Collider other)
    {
        inside = false;
    }
}
