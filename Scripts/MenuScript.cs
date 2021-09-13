using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private CarMovement game;
    [SerializeField] private Button start, options, exit, garageButton, backFromGarage, leftGarage, rightGarage;
    [SerializeField] private GameObject startObj, optionsObj, exitObj;

    //the UI for the gameplay
    public Button left, right, acceleration, brake;
    private const float lastCameraPositionOZ = -27.16f,//last position of the camera on OZ after the intro animation
                        garageCameraPositionOX = 4.14f;//last position of the camera on OX after you press the START button
    private bool clicked,
                buttonVisibility;//see if the user clicked on a button (any button)

    // Start is called before the first frame update
    private void Start()
    {
        garageButton.gameObject.SetActive(false);//hide the GO button from the garage
        backFromGarage.gameObject.SetActive(false);//back button
        leftGarage.gameObject.SetActive(false);//left arrow from the 'shop'
        rightGarage.gameObject.SetActive(false);//right arrow from the 'shop'

        ShowButtons(false);//hide the buttons bcs of the intro animation
        buttonVisibility = false;//buttons are not visible
        clicked = false;//set the default value
    }

    //back from the garage to the main menu
    public void BackFromGarage()
    {
        Debug.Log("INTRA");
    
        Start();
    }


    //start the actual game after you press the 'GO' button
    public void GarageGoButton()
    {
        garageButton.gameObject.SetActive(false);
        backFromGarage.gameObject.SetActive(false);
        leftGarage.gameObject.SetActive(false);
        rightGarage.gameObject.SetActive(false);

        game.GetComponent<Animator>().enabled = false;

        game.freezeGame = false;//start the game
        this.enabled = false;//disable this script

        //animation for the UI
    }

    //press on start button
    public void StartButton()
    {
        //activate the animation of the camera and move it to the garage
        //see animator
        
        //hide the buttons
        ShowButtons(false);
        buttonVisibility = false;
        clicked = true;
        
        //transform the button
        transform.localScale = Vector3.Lerp (transform.localScale, transform.localScale / 2, Time.deltaTime * 10);
        //color the start write on the sign
        startObj.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
    }

    //press on options button
    public void OptionsButton()
    {

        //hide the buttons
        ShowButtons(false);
        buttonVisibility = false;
        clicked = true;

        optionsObj.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    }

    //press on exit button
    public void ExitButton()
    {
        exitObj.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        Application.Quit();
    } 

    // Update is called once per frame
    private void Update()
    {
        if(transform.position.z == lastCameraPositionOZ && clicked == false)
        {
            if(buttonVisibility == true)//if the user clicked a button do nothing
                Debug.Log("Mesaj strict secret ce nu trebuie vazut de nimeni!");//un mesaj strict secret ce nu trebuie vazut de nimeni
            else   
                if(start.gameObject.activeSelf == false)//check if the buttons are already displayed
                {
                    Debug.Log("CE DOAMNE FRATE MAI AI ACUM");
                    ShowButtons(true);
                    buttonVisibility = true;
                }
        }
        else if(transform.position.x == garageCameraPositionOX)//the position of the camera on the garage
        {
            garageButton.gameObject.SetActive(true);//activate the GO button to start the game
            backFromGarage.gameObject.SetActive(true);
            leftGarage.gameObject.SetActive(true);
            rightGarage.gameObject.SetActive(true);
        }
        else if(transform.position.x != garageCameraPositionOX)
        {
            garageButton.gameObject.SetActive(false);//disable the go button if you press back
            backFromGarage.gameObject.SetActive(false);
            leftGarage.gameObject.SetActive(false);
            rightGarage.gameObject.SetActive(false);
        }
    }

    //functions made to set the visibility for all the buttons (start, options and exit)
    private void ShowButtons(bool x)
    {
        start.gameObject.SetActive(x);
        options.gameObject.SetActive(x);
        exit.gameObject.SetActive(x);
    }
}
