using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Button start,options,exit;
    private const float lastCameraPositionOZ = -29.82f;//last position of the camera on OZ after the intro animation
    private bool clicked;//see if the user clicked on a button (any button)

    // Start is called before the first frame update
    private void Start()
    {
        ShowButtons(false);//hide the buttons bcs of the intro animation
        clicked = false;//set the default value
    }

    
    //press on start button
    public void StartButton()
    {
        //activate the animation of the camera and move it to the garage
        //see animator
        
        //hide the buttons
        ShowButtons(false);
        clicked = true;
        
    }

    //press on options button
    public void OptionsButton()
    {
        //create an options scene (or credits instead)

        //hide the buttons
        ShowButtons(false);
        clicked = true;
    }

    //press on exit button
    public void ExitButton() => Application.Quit();

    // Update is called once per frame
    private void Update()
    {
        if(transform.gameObject.transform.position.z == lastCameraPositionOZ)
        {
            if(clicked == true)//if the user clicked a button exit the function
                return;
            if(start.gameObject.active == false)//check if the buttons are already displayed
            {
                Debug.Log("LE PUN");
                ShowButtons(true);
            }
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
