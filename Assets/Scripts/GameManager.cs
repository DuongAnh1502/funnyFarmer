using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int point = 0;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if(!playerController.gameOver && playerController.isStarted)
        {
            Debug.Log("Score : "+point);
            if (playerController.isDashed)
            {
                point += 2;
            }
            else
            {
                point += 1;
            }
        }
    }
}
