using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject[] obstacles;
    public Vector3 spawnPos;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn", 2, 2);
    }

    void spawn()
    {
        int obstacleIndex = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[obstacleIndex], spawnPos, obstacles[obstacleIndex].transform.rotation, transform);
    }
}
