using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingHouseScript : MonoBehaviour
{
    // Start is called before the first frame update
    CarMovement player;
    void Start()
    {
        player = FindObjectOfType<CarMovement>();//the player car
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.z > 30f)
            Destroy(this.gameObject);
    }
}
