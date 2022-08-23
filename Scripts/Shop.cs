using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button goButton;
    private int currentMoney;
    private bool buttonPressed;
    private Camera camera;
    private const int numberOfCars = 16, oxRotation = 90;
    public int currentCar;
    public GameObject[] carModels = new GameObject[numberOfCars];
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private TextMeshProUGUI priceText;

    public void refreshMoney()
    {

    }

    public int getCurrentCar()
    {
        return currentCar;
    }

    public void disableUILock()
    {
        lockImage.SetActive(false);
    }

    public bool tryToBuy()
    {
        int price = int.Parse(priceText.text);
        
        if(currentMoney > price)
        {
            Debug.Log("PRICE new = " + (currentMoney - price).ToString());
            PlayerPrefs.SetInt("money_key", currentMoney - price);
            PlayerPrefs.SetInt(currentCar.ToString(), 1);
            return true;
        }
        return false;
    }

    void Start()
    {
        lockImage = GameObject.FindGameObjectWithTag("lockShop");
        camera = GameObject.FindObjectOfType<Camera>();

        currentMoney = PlayerPrefs.GetInt("money_key");
        currentCar = 0;
        buttonPressed = false;
    }

    void Update()
    {
        if (FindObjectOfType<CarMovement>().transform.position.z > 0)
            this.enabled = false;
        if (door.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("open_door_animation") && buttonPressed == true)
        {
            foreach (Transform child in transform) 
            {
                GameObject.Destroy(child.gameObject);
            }
            GameObject obj = Instantiate(carModels[currentCar], transform.position, transform.rotation);
            obj.transform.parent = this.transform;

            obj.GetComponentInParent<Animator>().Play("forward_car_shop");

            buttonPressed = false;
        }
        if(PlayerPrefs.GetInt(currentCar.ToString()) == 0)
        {
            lockImage.SetActive(true);
            Text txt = goButton.GetComponentInChildren(typeof(Text)) as Text;
            txt.text = "BUY & GO";

            //show the price of the car
            priceText.text = (currentCar * 1000 * 2 + 500).ToString();
        }
        else
        {
            lockImage.SetActive(false);
            Text txt = goButton.GetComponentInChildren(typeof(Text)) as Text;
            txt.text = "GO";
        }
    }

    public void RightPress()
    {
        buttonPressed = true;
        if (currentCar < numberOfCars)
        {
            currentCar++;
            currentCar = currentCar % numberOfCars;
            Debug.Log("Current car: " + currentCar.ToString());
        }
        else
            currentCar = numberOfCars - 1;
    }

    public void LeftPress()
    {
       buttonPressed = true;
        if (currentCar > 0)
        {
            currentCar--;
            currentCar = currentCar % numberOfCars;
            Debug.Log("Current car: " + currentCar.ToString());
        }
        else
            currentCar = numberOfCars - 1;
    }
}
