using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScroller : MonoBehaviour
{
    //---------------PRIVATE VARIABLES---------------
    private float repeatWidth;
    PlayerController _playerScript;

    //---------------PUBLIC VARIABLES---------------
    [Header("Scroll Variables")]
    public float scrollSpeed;
    public Vector3 startPos;

    //---------------START---------------
    void Start()
    {
        transform.position = startPos;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    //---------------UPDATE---------------
    void Update()
    {
        //---------------MOVES THE BACKGROUND---------------
        if (_playerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime, Space.World);

            
        }

        //---------------RESETS BACKGROUND---------------
        if (transform.position[0] <= startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }

    }
}
