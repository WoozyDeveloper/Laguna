using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
    private int[] positions = new int[] { -6, -2, 2, 6 };
    private GameObject money;
    [SerializeField] private CarMovement car;
    public Text moneyText;


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
        /*
            Instantiate  all the money random on each platform

        */
        money = GetComponent<GameObject>();
        car = FindObjectOfType<CarMovement>();

        moneyText = GameObject.FindGameObjectWithTag("MoneyTag").GetComponent<Text>();
        moneyText.text = PlayerPrefs.GetInt("money_key").ToString() + " $";

        transform.position = new Vector3(Aproximate(transform.position.x), 0.65f,transform.position.z);
        //Physics.IgnoreCollision(GetComponent<Collider>(),FindObjectOfType<CarMovement>().GetComponent<Collider>());
        Physics.IgnoreCollision(GetComponent<Collider>(),FindObjectOfType<OtherCar>().GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.z < 0f)
        {
            moneyText.text = PlayerPrefs.GetInt("money_key").ToString() + " $";
        }
        if(this.transform.position.z < car.transform.position.z - 5f)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        float oz_position = transform.position.z;
        const int step = 200;

        int choice = Random.Range(0,4);
        Instantiate(gameObject, new Vector3(Aproximate(positions[choice]),transform.position.y,transform.position.z + step), Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("money_key", PlayerPrefs.GetInt("money_key") + (int) other.GetComponent<CarMovement>().carSpeed / 2 + 20);
            //Debug.Log("Money Spawn! ---> " + PlayerPrefs.GetInt("money_key"));

            moneyText.text = PlayerPrefs.GetInt("money_key").ToString() + " $";
            Respawn();
        }
    }
}
