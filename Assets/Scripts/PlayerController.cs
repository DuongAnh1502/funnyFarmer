using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody playerRb;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    private AudioSource backGroundAudio;
    public float jumpForce = 10f;
    public float gravityModifier;
    private float jumpedTime = 1;
    private float startSpeed = 9f;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool isDashed = false;
    public bool isStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        backGroundAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        playerAnim.SetFloat("Speed_f", 0.3f);
        backGroundAudio.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= -2 && !isStarted)
        {
            isStarted = true;
            backGroundAudio.Play();
            playerAnim.SetFloat("Speed_f", 1f);
            playerAnim.SetBool("Static_b", false);
        } else if (!isStarted)
        {
            transform.Translate(Vector3.forward*Time.deltaTime*startSpeed);
        }
        dash();
        if(Input.GetKeyDown(KeyCode.Space) && jumpedTime >= 0 && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            if(jumpedTime > 0) {
                playerAnim.SetTrigger("Jump_trig");
            }
            isOnGround = false;
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound,1f);
            jumpedTime--;
        }
    }
    private void dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isStarted)
        {
            isDashed = true;
            playerAnim.SetFloat("Speed_Multiplier", 1.5f);
        } else if (Input.GetKeyUp(KeyCode.LeftShift) && isStarted)
        {
            isDashed = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            if(!gameOver)
            {
                dirtParticle.Play();
                jumpedTime = 1;
            }
            isOnGround = true;
        } else if(collision.gameObject.CompareTag("Obstacles"))
        {
            playerAnim.SetBool("Death_b",true);
            playerAnim.SetInteger("DeathType_int", 1);
            Debug.Log("Game Over!!");
            gameOver = true;
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1f);
        }
    }
}
