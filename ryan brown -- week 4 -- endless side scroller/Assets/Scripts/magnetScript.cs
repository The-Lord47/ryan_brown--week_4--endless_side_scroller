using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class magnetScript : MonoBehaviour
{

    PlayerController _playerScript;
    GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position + new Vector3(0,2,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "coin")
        {
            _playerScript.score++;
            _playerScript.coinSFX.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
        }
    }
}
