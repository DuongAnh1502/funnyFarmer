using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController player;
    public PlayerController playerController;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}
