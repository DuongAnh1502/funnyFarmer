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
    public float jumpForce = 10f;
    public float gravityModifier;
    private float jumpedTime = 1;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool isDashed = false;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isDashed = true;
        } else
        {
            isDashed = false;
        }
        if(isDashed)
        {
            
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
            isOnGround =true;
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
