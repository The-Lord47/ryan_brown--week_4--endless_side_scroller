using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class obstactleMovement : MonoBehaviour
{
    //---------------PRIVATE VARIABLES---------------
    GameObject _player;
    PlayerController _playerScript;

    //---------------PUBLIC VARIABLES---------------
    [Header("Obstacle Movement")]
    public float speed;
    public float xThreshold;

    [Header("Coin under magnet movement")]
    public bool magnetTouch = false;
    public float magnetSpeed;


    //---------------START---------------
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    //---------------UPDATE---------------
    void Update()
    {
        //---------------OBJECT MOVEMENT---------------
        if (_playerScript.gameOver == false && magnetTouch == false)
        {
            //moves object left at rate of speed
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        //---------------COIN UNDER MAGNET MOVEMENT---------------
        else if (_playerScript.gameOver == false && magnetTouch == true)
        {
            Vector3 tempUnitVector = (_player.transform.position - transform.position);
            transform.Translate(Vector3.Normalize(tempUnitVector) * magnetSpeed * Time.deltaTime, Space.World);
        }

        //---------------DESTROYS OFF SCREEN---------------
        if (transform.position.x < xThreshold)
        {
            Destroy(gameObject);
        }
    }

    //---------------COIN/MAGNET COLLISION---------------
    private void OnTriggerStay(Collider other)
    {
        if(gameObject.tag == "coin" && other.tag == "magnetRadius")
        {
            magnetTouch = true;
        }
    }
}
