using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject coin;
    public Vector3 spawnPos;


    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "obstacleSpawner")
        {
            InvokeRepeating("spawnObstacles", 0, 2);
        }
        if (gameObject.tag == "coinSpawner")
        {
            InvokeRepeating("spawnCoins", 0, 1);
        }
        
    }

    void spawnObstacles()
    {
        int obstacleIndex = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[obstacleIndex], spawnPos, obstacles[obstacleIndex].transform.rotation, transform);
    }

    void spawnCoins()
    {
        Vector3 tempSpawnPos = spawnPos + new Vector3(Random.Range(-1f,1f), Random.Range(2f, 8f), 0); 
        Instantiate(coin, tempSpawnPos, coin.transform.rotation, transform);
    }
}
