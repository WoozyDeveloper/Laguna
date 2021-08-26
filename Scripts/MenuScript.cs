using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private CarMovement game;
    [SerializeField] private Button start,options,exit,garageButton;
    private const float lastCameraPositionOZ = -29.82f,//last position of the camera on OZ after the intro animation
                        garageCameraPositionOX = 7.07f;//last position of the camera on OX after you press the START button
    private bool clicked,
                buttonVisibility;//see if the user clicked on a button (any button)

    // Start is called before the first frame update
    private void Start()
    {
        game = FindObjectOfType<CarMovement>();

        garageButton.gameObject.SetActive(false);//hide the GO button from the garage
        ShowButtons(false);//hide the buttons bcs of the intro animation
        buttonVisibility = false;//buttons are not visible
        clicked = false;//set the default value
    }

    //back from the garage to the main menu
    public void BackFromGarage()
    {
        garageButton.gameObject.SetActive(false);
        clicked = false;
        buttonVisibility = true;
        ShowButtons(true);
    }


    //start the actual game after you press the 'GO' button
    public void GarageGoButton()
    {
        garageButton.gameObject.SetActive(false);//hide the button
        game.freezeGame = false;//start the game
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
        
    }

    //press on options button
    public void OptionsButton()
    {
        //create an options scene (or credits instead)

        //hide the buttons
        ShowButtons(false);
        buttonVisibility = false;
        clicked = true;
    }

    //press on exit button
    public void ExitButton() => Application.Quit();

    // Update is called once per frame
    private void Update()
    {
        if(game.freezeGame == false)//exit this function if the user left the menu
            return;
        if(transform.position.z == lastCameraPositionOZ && clicked == false)
        {
            if(buttonVisibility == true)//if the user clicked a button do nothing
                Debug.Log("Mesaj strict secret ce nu trebuie vazut de nimeni!");//un mesaj strict secret ce nu trebuie vazut de nimeni
            else
                if(start.gameObject.activeSelf == false)//check if the buttons are already displayed
                {
                    ShowButtons(true);
                    buttonVisibility = true;
                }
        }
        else if(transform.position.x == garageCameraPositionOX)//the position of the camera on the garage
        {
            garageButton.gameObject.SetActive(true);//activate the GO button to start the game
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
