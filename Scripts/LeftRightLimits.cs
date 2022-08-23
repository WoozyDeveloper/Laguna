using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightLimits : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
            FindObjectOfType<CarMovement>().Death();
    }
}
