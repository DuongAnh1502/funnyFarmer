using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController player;
    public float speed;
    private float leftBound = -15;
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
        if (player.isDashed)
        {
            speed = 60;
        } else
        {
            speed = 40;
        }
        if (gameObject.CompareTag("Obstacles") && transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
