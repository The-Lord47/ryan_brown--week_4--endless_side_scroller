using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //---------------PRIVATE VARIABLES---------------
    Rigidbody rb;
    bool isGrounded = false;
    GameObject[] jumpSFXs;
    GameObject damageSFX;
    GameObject taserSFX;
    Animator _animator;
    int lives = 3;
    bool iFrames = false;
    bool allowDoubleJump = true;
    bool doubleJump = false;
    float timer;

    //---------------PUBLIC VARIABLES---------------

    [Header("Player Movement")]
    public float jumpForce = 8;
    public float maxLinearVelocity;

    [Header("GameObject References")]
    public GameObject coinSFX;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject magnet;
    public GameObject magnetParticleEffect;
    public TMP_Text score_txt;

    [Header("Game Systems")]
    public int score = 0;
    public bool gameOver = false;
    public bool magnetActive = false;
    public float magnetTimer;

    [Header("Particle Effects")]
    public ParticleSystem _dirt;
    public ParticleSystem _taser;


    //---------------START---------------
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        taserSFX = GameObject.FindGameObjectWithTag("taserSFX");
        jumpSFXs = GameObject.FindGameObjectsWithTag("jumpSFX");
        damageSFX = GameObject.FindGameObjectWithTag("damageSFX");
        coinSFX = GameObject.FindGameObjectWithTag("coinSFX");
        rb.maxLinearVelocity = maxLinearVelocity;
    }

    //---------------UPDATE---------------
    void Update()
    {
        //---------------MOVEMENT---------------
        //when space is pressed, adds impulse force and plays animations and sound effects
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded == true || doubleJump == true) && gameOver == false)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            _animator.SetBool("Grounded", false);
            _animator.SetTrigger("Jump_trig");
            jumpSFXs[0].GetComponent<AudioSource>().Play();
            doubleJump = false;
            _dirt.Stop();
        }

        //---------------I-FRAMES---------------
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

        //---------------DOUBLE JUMP TRACKING---------------
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
            if (score > PlayerPrefs.GetInt("Highscore_endless_runner"))
            {
                PlayerPrefs.SetInt("Highscore_endless_runner", score);
            }
            heart1.SetActive(false);
            _dirt.Stop();
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
                magnetParticleEffect.SetActive(false);
                magnetTimer = 0;
            }
        }
        //---------------SCORE---------------
        score_txt.text = "Score:" + score;

    }

    //---------------COLLISION DETECTION---------------
    private void OnTriggerEnter(Collider other)
    {
        //---------------OBSTACLE COLLISION---------------
        if (other.tag == "obstacle" && iFrames == false)
        {
            //different for if it isn't the last life being lost
            if (lives > 1) 
            {
                transform.position -= new Vector3(4, 0, 0);
                damageSFX.GetComponent<AudioSource>().Play();
                lives--;
                iFrames = true;
                _animator.SetBool("isDamaged", true);
            }
            //last life being lost
            else
            {
                taserSFX.GetComponent<AudioSource>().Play();
                _taser.Play();
                lives--;
                _animator.SetBool("Death_b", true);
            }
            
        }

        //---------------COIN COLLISION---------------
        if (other.tag == "coin")
        {
            score++;
            coinSFX.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
        }

        //---------------MAGNETBOX COLLISION---------------
        if (other.tag == "magnet")
        {
            magnet.SetActive(true);
            magnetParticleEffect.SetActive(true);
            magnetActive = true;
            Destroy(other.gameObject);
        }
    }

    //---------------GROUND DETECTION---------------
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        _animator.SetBool("Grounded", true);
        _dirt.Play();
    }
}
