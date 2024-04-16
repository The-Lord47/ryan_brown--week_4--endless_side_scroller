using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;

    public float jumpForce = 8;
    public float gravityMultiplier;
    bool isGrounded = false;

    GameObject[] jumpSFXs;
    GameObject damageSFX;
    GameObject coinSFX;

    Animator _animator;

    int lives = 3;

    bool iFrames = false;
    bool allowDoubleJump = true;
    bool doubleJump = false;
    float timer;

    public float maxLinearVelocity;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    public TMP_Text score_txt;
    int score = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMultiplier;
        _animator = GetComponent<Animator>();
        jumpSFXs = GameObject.FindGameObjectsWithTag("jumpSFX");
        damageSFX = GameObject.FindGameObjectWithTag("damageSFX");
        coinSFX = GameObject.FindGameObjectWithTag("coinSFX");
        rb.maxLinearVelocity = maxLinearVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        //makes the player jump when space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded == true || doubleJump == true))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            _animator.SetBool("Grounded", false);
            _animator.SetTrigger("Jump_trig");
            jumpSFXs[0].GetComponent<AudioSource>().Play();
            doubleJump = false;
        }

        if (iFrames == true)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                timer = 0;
                iFrames = false;
                _animator.SetBool("isDamaged", false);
            }
        }

        if (lives == 0)
        {
            Time.timeScale = 0f;
        }

        if (allowDoubleJump == true && isGrounded == false)
        {
            doubleJump = true;
            allowDoubleJump = false;
        }
        if (allowDoubleJump == false && isGrounded == true)
        {
            allowDoubleJump = true;
        }



        //---------------HEART MANAGEMENT SYSTEM---------------
        if (lives == 2)
        {
            heart3.SetActive(false);
        }
        if (lives == 1)
        {
            heart2.SetActive(false);
        }
        if (lives == 0)
        {
            heart1.SetActive(false);
        }

        score_txt.text = "Score:" + score;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "obstacle" && iFrames == false)
        {
            transform.position -= new Vector3(4, 0, 0);
            damageSFX.GetComponent<AudioSource>().Play();
            lives--;
            iFrames = true;
            _animator.SetBool("isDamaged", true);
        }

        if(other.tag == "coin")
        {
            score++;
            coinSFX.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        _animator.SetBool("Grounded", true);
    }
}
