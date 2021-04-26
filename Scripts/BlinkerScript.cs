using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkerScript : MonoBehaviour
{
    private bool startBlink;
    Renderer blinkerRenderer;
    // Start is called before the first frame update
    void Start()
    {
        blinkerRenderer = GetComponent<Renderer>();
        startBlink = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(startBlink == true)
        {
            blinkerRenderer.material.SetColor("active_blink", Color.yellow);
            StartCoroutine(ExampleCoroutine());
            blinkerRenderer.material.SetColor("_Color", Color.black);
            //StartCoroutine(ExampleCoroutine());
        }
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 3 seconds.
        yield return new WaitForSeconds(1);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    void changeState()
    {
        startBlink = !startBlink;
    }
}
