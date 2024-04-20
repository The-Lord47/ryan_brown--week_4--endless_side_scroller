using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    //---------------PRIVATE VARIABLES---------------
    PlayerController _playerController;
    GameObject _bgMusic;
    GameObject _sirenSFX;
    bool start = false;

    //---------------PUBLIC VARIABLES---------------
    [Header("UI Elements")]
    public GameObject GameplayUI;
    public GameObject StartScreen;
    public GameObject EndScreen;
    public GameObject magnetboxUI;
    public TMP_Text magnetboxTimer_txt;
    public TMP_Text highscore_txt;

    //---------------START---------------
    void Start()
    {
        //sets the highscore
        highscore_txt.text = "Highscore:" + PlayerPrefs.GetInt("Highscore_endless_runner").ToString();
        //starts the game with game and end ui off and start ui on
        GameplayUI.SetActive(false);
        EndScreen.SetActive(false);
        StartScreen.SetActive(true);
        //gets references to some useful objects and scripts
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _bgMusic = GameObject.FindGameObjectWithTag("bgMusic");
        _sirenSFX = GameObject.FindGameObjectWithTag("sirenSFX");
        //sets the time scale to 0 at start
        Time.timeScale = 0f;
    }

    //---------------UPDATE---------------
    void Update()
    {
        //---------------ANY KEY TO START---------------
        if (start == false)
        {
            //waits for any key press to begin the game
            if (Input.anyKeyDown)
            {
                start = true;
                StartScreen.SetActive(false);
                GameplayUI.SetActive(true);
                _bgMusic.GetComponent<AudioSource>().Play();
                _sirenSFX.GetComponent<AudioSource>().Play();
                Time.timeScale = 1f;
            }
        }

        //---------------GAMEOVER---------------
        if (_playerController.gameOver == true)
        {
            _bgMusic.GetComponent<AudioSource>().Stop();
            GameplayUI.SetActive(false);
            EndScreen.SetActive(true);
        }

        //---------------MAGNET UI---------------

        //shows up if the magnet is active
        if (_playerController.magnetActive == true)
        {
            magnetboxUI.SetActive(true);
            magnetboxTimer_txt.text = (10 - Mathf.Round(_playerController.magnetTimer)).ToString();
        }
        //disables if the magnet is inactive
        if(_playerController.magnetActive == false)
        {
            magnetboxUI.SetActive(false);
        }

    }
}
