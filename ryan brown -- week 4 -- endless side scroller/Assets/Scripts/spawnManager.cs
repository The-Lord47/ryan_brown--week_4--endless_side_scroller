using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    //---------------PRIVATE VARIABLES---------------
    PlayerController _playerScript;

    //---------------PUBLIC VARIABLES
    [Header("GameObject References")]
    public GameObject[] obstacles;
    public GameObject coin;
    public GameObject magnetbox;

    [Header("Spawn Variables")]
    public Vector3 spawnPos;



    //---------------START---------------
    void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //---------------SPAWNS OBJECTS, COINS, AND POWERUPS---------------
        if (_playerScript.gameOver == false)
        {
            if (gameObject.tag == "obstacleSpawner")
            {
                InvokeRepeating("spawnObstacles", 0, 2);
            }
            if (gameObject.tag == "coinSpawner")
            {
                InvokeRepeating("spawnCoins", 0, 1);
            }
            if (gameObject.tag == "magnetboxSpawner")
            {
                InvokeRepeating("spawnMagnetbox", Random.Range(5f, 20f), Random.Range(20f, 40f));
            }
        }
    }

    //---------------SPAWNS OBSTACLES---------------
    void spawnObstacles()
    {
        int obstacleIndex = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[obstacleIndex], spawnPos, obstacles[obstacleIndex].transform.rotation, transform);
    }

    //---------------SPAWNS COINS---------------
    void spawnCoins()
    {
        Vector3 tempSpawnPos = spawnPos + new Vector3(Random.Range(-1f,1f), Random.Range(2f, 8f), 0); 
        Instantiate(coin, tempSpawnPos, coin.transform.rotation, transform);
    }

    //---------------SPAWNS MAGNETBOXES---------------
    void spawnMagnetbox()
    {
        Vector3 tempSpawnPos = spawnPos + new Vector3(Random.Range(-1f, 1f), Random.Range(2f, 8f), 0);
        Instantiate(magnetbox, tempSpawnPos, magnetbox.transform.rotation, transform);
    }
}
