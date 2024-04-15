using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstactleMovement : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //moves object left at rate of speed
        transform.Translate(Vector3.left *  speed * Time.deltaTime);
    }
}
