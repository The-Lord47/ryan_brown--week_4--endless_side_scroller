using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class policeLights : MonoBehaviour
{

    public bool isBlue;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        if (isBlue == true)
        {
            gameObject.GetComponent<Light>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.5f)
        {
            gameObject.GetComponent<Light>().enabled = !gameObject.GetComponent<Light>().isActiveAndEnabled;
            timer = 0;
        }
    }
}
