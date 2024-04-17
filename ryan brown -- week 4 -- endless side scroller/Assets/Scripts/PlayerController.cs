using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;

    public float jumpForce = 8;
    bool isGrounded = false;

    GameObject[] jumpSFXs;
    GameObject damageSFX;
    public GameObject coinSFX;

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
    public int score = 0;

    public bool gameOver = false;

    public bool magnetActive = false;
    public GameObject magnet;
    public float magnetTimer;


    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded == true || doubleJump == true) && gameOver == false)
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
        if (lives <= 0)
        {
            heart1.SetActive(false);
            gameOver = true;
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        //---------------MAGNET TIMER SYSTEM ---------------
        if (magnetActive == true)
        {
            magnetTimer += Time.deltaTime;
            if(magnetTimer > 10)
            {
                magnetActive = false;
                magnet.SetActive(false);
                magnetTimer = 0;
            }
        }

        score_txt.text = "Score:" + score;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "obstacle" && iFrames == false)
        {
            if(lives > 1) 
            {
                transform.position -= new Vector3(4, 0, 0);
                damageSFX.GetComponent<AudioSource>().Play();
                lives--;
                iFrames = true;
                _animator.SetBool("isDamaged", true);
            }
            else
            {
                damageSFX.GetComponent<AudioSource>().Play();
                lives--;
                _animator.SetBool("Death_b", true);
            }
            
        }

        if(other.tag == "coin")
        {
            score++;
            coinSFX.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
        }

        if(other.tag == "magnet")
        {
            magnet.SetActive(true);
            magnetActive = true;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        _animator.SetBool("Grounded", true);
    }
}
