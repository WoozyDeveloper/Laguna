using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkerScript : MonoBehaviour
{
    float countdown;
    private bool startBlink;
    bool change;
    Renderer blinkerRenderer;
    // Start is called before the first frame update
    void Start()
    {
        blinkerRenderer = GetComponent<Renderer>();
        startBlink = true;
        change = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            countdown = .2f;
            if (change == false)
                blinkerRenderer.material.SetColor("_Color", Color.yellow);
            else
                blinkerRenderer.material.SetColor("_Color", Color.black);
        }
        change = !change;
    }

    void changeState()
    {
        startBlink = !startBlink;
    }
}
