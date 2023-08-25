using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool alienStatus = true;
    public bool inScreen;
    public float velocitySpeed = 10;
    public Rigidbody2D rigidBody;
    public Animator animator;

    public GameObject shield;

    public ShieldScript shieldScript;
    public LogicScript logic;
    private void Awake()
    {
        shieldScript = gameObject.GetComponentInChildren<ShieldScript>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }
    void Start()
    {
        pausePlayer();
    }
    void Update()
    {
        inScreen = transform.position.y < 12 && transform.position.y > -12;
        if (Input.GetKeyDown(KeyCode.Space) && alienStatus)
        {
            rigidBody.velocity = Vector2.up * velocitySpeed;
            logic.musicScript.playJumpSound();
        }
        if (!inScreen && !logic.gameOverTriggered)
        {
            logic.gameOver();
            pausePlayer();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!logic.gameOverTriggered)
        {
            logic.gameOver();
            rigidBody.freezeRotation = true;
            pausePlayer();
        }
    }
    public void pausePlayer()
    {
        alienStatus = false;
        rigidBody.gravityScale = 0;
        rigidBody.velocity = Vector2.zero;
        animator.SetBool("Move", false);
    }
    public void unPausePlayer()
    {
        alienStatus = true;
        rigidBody.gravityScale = 2;
        animator.SetBool("Move", true);
    }
    public void resetPlayer()
    {
        transform.position = new Vector3(-10, 0, 0);
        unPausePlayer();
    }
}
