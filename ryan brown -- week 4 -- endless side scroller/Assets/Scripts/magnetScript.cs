using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class magnetScript : MonoBehaviour
{
    //---------------PRIVATE VARIABLES---------------
    PlayerController _playerScript;
    GameObject _player;

    //---------------START---------------
    void Start()
    {
        //gets references
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    //---------------UPDATE---------------
    void Update()
    {
        //keeps the magnet bubble attached to the player
        transform.position = _player.transform.position + new Vector3(0,2,0);
    }
}
