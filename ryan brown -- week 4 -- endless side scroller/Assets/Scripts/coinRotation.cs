using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinRotation : MonoBehaviour
{
    //---------------PUBLIC VARIABLES---------------
    [Header("Coin Rotation")]
    public float rotationRate;

    //---------------UPDATE---------------
    void Update()
    {
        //rotates the coin
        transform.Rotate(Vector3.up * rotationRate * Time.deltaTime);
    }
}
