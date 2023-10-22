using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstacles;
    private Vector3 spawnPosition = new Vector3(25,0,0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObstacle()
    {
        GameObject obstacle = obstacles[Random.Range(0,obstacles.Length)];
        if(!playerController.gameOver)
        {
            Instantiate(obstacle, spawnPosition, obstacle.transform.rotation);
        }
    }
}
