using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject GameplayUI;
    public GameObject StartScreen;
    public GameObject EndScreen;
    public GameObject magnetboxUI;
    public TMP_Text magnetboxTimer_txt;

    PlayerController _playerController;
    GameObject _bgMusic;
    

    bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        GameplayUI.SetActive(false);
        EndScreen.SetActive(false);
        StartScreen.SetActive(true);
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _bgMusic = GameObject.FindGameObjectWithTag("bgMusic");
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (start == false)
        {
            if (Input.anyKeyDown)
            {
                start = true;
                StartScreen.SetActive(false);
                GameplayUI.SetActive(true);
                _bgMusic.GetComponent<AudioSource>().Play();
                Time.timeScale = 1f;
            }
        }
        
        if(_playerController.gameOver == true)
        {
            _bgMusic.GetComponent<AudioSource>().Stop();
            GameplayUI.SetActive(false);
            EndScreen.SetActive(true);
        }

        if(_playerController.magnetActive == true)
        {
            magnetboxUI.SetActive(true);
            magnetboxTimer_txt.text = (10 - Mathf.Round(_playerController.magnetTimer)).ToString();
        }

        if(_playerController.magnetActive == false)
        {
            magnetboxUI.SetActive(false);
        }

    }
}
