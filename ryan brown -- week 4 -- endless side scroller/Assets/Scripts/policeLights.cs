using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class policeLights : MonoBehaviour
{
    //---------------PRIVATE VARIABLES---------------
    float timer;

    //---------------PUBLIC VARIABLES---------------
    [Header("Blue check")]
    public bool isBlue;


    //---------------START---------------
    void Start()
    {
        //offsets the blue light to have red and blue active at different times
        if (isBlue == true)
        {
            gameObject.GetComponent<Light>().enabled = false;
        }
    }

    //---------------UPDATE---------------
    void Update()
    {
        //increases timer every update
        timer += Time.deltaTime;

        //swaps lights between on and off state every 0.5 seconds
        if (timer > 0.5f)
        {
            gameObject.GetComponent<Light>().enabled = !gameObject.GetComponent<Light>().isActiveAndEnabled;
            timer = 0;
        }
    }
}
