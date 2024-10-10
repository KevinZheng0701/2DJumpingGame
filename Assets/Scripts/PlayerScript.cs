using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool playerStatus = true;
    public bool inScreen;
    public float velocitySpeed = 10;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Animator animator;
    [SerializeField] GameObject shield;
    [SerializeField] ShieldScript shieldScript;
    [SerializeField] LogicScript logic;
    [SerializeField] MusicScript musicScript;

    void Start()
    {
        PausePlayer();
    }

    void Update()
    {
        inScreen = transform.position.y < 12 && transform.position.y > -12;
        if (Input.GetKeyDown(KeyCode.Space) && playerStatus)
        {
            rigidBody.velocity = Vector2.up * velocitySpeed;
            musicScript.PlayJumpSound();
        }
        if (!inScreen && !logic.gameOverTriggered)
        {
            logic.GameOver();
            PausePlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!logic.gameOverTriggered)
        {
            logic.GameOver();
            rigidBody.freezeRotation = true;
            PausePlayer();
        }
    }

    public void PausePlayer()
    {
        playerStatus = false;
        rigidBody.gravityScale = 0;
        rigidBody.velocity = Vector2.zero;
        animator.SetBool("Move", false);
    }

    public void UnPausePlayer()
    {
        playerStatus = true;
        rigidBody.gravityScale = 2;
        animator.SetBool("Move", true);
    }

    public void ResetPlayer()
    {
        transform.position = new Vector3(-10, 0, 0);
        UnPausePlayer();
    }
}
