using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;

    public float jumpForce = 8;
    public float gravityMultiplier;
    bool isGrounded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        //makes the player jump when space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}
