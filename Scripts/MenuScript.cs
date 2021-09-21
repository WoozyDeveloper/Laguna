using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private CarMovement game;
    [SerializeField] private Button start, options, exit, garageButton, backFromGarage, leftGarage, rightGarage, pause;
    [SerializeField] private GameObject startObj, optionsObj, exitObj, panel, spiral;

    //the UI for the gameplay
    public Button left, right, acceleration, brake;
    private const float lastCameraPositionOZ = -27.16f,//last position of the camera on OZ after the intro animation
                        garageCameraPositionOX = 4.975f;//last position of the camera on OX after you press the START button
    private bool clicked,
                buttonVisibility;//see if the user clicked on a button (any button)

    // Start is called before the first frame update
    private void Start()
    {
        garageButton.gameObject.SetActive(false);//hide the GO button from the garage
        backFromGarage.gameObject.SetActive(false);//back button
        leftGarage.gameObject.SetActive(false);//left arrow from the 'shop'
        rightGarage.gameObject.SetActive(false);//right arrow from the 'shop'
        pause.gameObject.SetActive(false);//pause button right top corner

        ShowButtons(false);//hide the buttons bcs of the intro animation
        buttonVisibility = false;//buttons are not visible
        clicked = false;//set the default value

        panel.gameObject.SetActive(false);
    }

    //press on resume
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        panel.gameObject.SetActive(false);
    }

    //return to menu from pause menu
    public void ReturnToMenu()
    {
        FindObjectOfType<CarMovement>().Death();
    }

    //pause the game
    public void PauseGame()
    {

        Time.timeScale = .0f;//
        panel.gameObject.SetActive(true);
    }

    //back from the garage to the main menu
    public void BackFromGarage()
    {
        startObj.transform.localScale = new Vector3(startObj.transform.localScale.x,startObj.transform.localScale.y, 100f);
        Start();
    }


    //start the actual game after you press the 'GO' button
    public void GarageGoButton()
    {
        garageButton.gameObject.SetActive(false);
        backFromGarage.gameObject.SetActive(false);
        leftGarage.gameObject.SetActive(false);
        rightGarage.gameObject.SetActive(false);
        pause.gameObject.SetActive(true);

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
        Vector3 pos = startObj.transform.localScale;
        startObj.transform.localScale = new Vector3(startObj.transform.localScale.x,startObj.transform.localScale.y, 10f);
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
            //if(buttonVisibility == true)//if the user clicked a button do nothing
            //    Debug.Log("Mesaj strict secret ce nu trebuie vazut de nimeni!");//un mesaj strict secret ce nu trebuie vazut de nimeni
            if(buttonVisibility == false)   
                if(start.gameObject.activeSelf == false)//check if the buttons are already displayed
                {
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
