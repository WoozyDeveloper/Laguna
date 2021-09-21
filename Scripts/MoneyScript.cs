using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    private GameObject money;
     private int Aproximate(float oxPosition)
    {
        float minimum_dif = float.MaxValue;
        int good_index = -1;
        float[] diff = new float[4];
        int[] positions = new int[] { -6, -2, 2, 6 };

        for (int index = 0; index < 4; index++)
            diff[index] = Mathf.Abs(oxPosition - positions[index]);

        for (int index = 0; index < 4; index++)
            if (diff[index] < minimum_dif)
            {
                minimum_dif = diff[index];
                good_index = index;
            }
        return positions[good_index];

    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Aproximate(transform.position.x), 0.65f,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("money_key", PlayerPrefs.GetInt("money_key") + 30);
            Instantiate(this,new Vector3(transform.position.x,transform.position.y,transform.position.z + 200f), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
