using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public bool shieldStatus = false;
    private float shieldDuration = 10;
    [SerializeField] private float remainingTime = 0;
    private float activationTime;

    public Animator animator;
    public LogicScript logic;

    private void Awake()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }
    void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (!logic.playerScript.inScreen)
        {
            shieldStatus = false;
            gameObject.SetActive(false);
        }
        if (shieldStatus && !logic.isPaused)
        {
            remainingTime = logic.calculateRemainingTime(activationTime, shieldDuration);
            if (remainingTime <= 0)
            {
                deactivateShield();
            }
        }
    }
    public void activateShield()
    {
        shieldStatus = true;
        animator.SetBool("ShieldActivated", true);
        gameObject.SetActive(true);
        activationTime = Time.time;
        logic.musicScript.playShieldSound();
        logic.uiScript.boostText.text = "Shield Activated";
        logic.uiScript.displayBoost();
    }
    private void deactivateShield()
    {
        shieldStatus = false;
        animator.SetBool("ShieldActivated", false);
        StartCoroutine(delayDeactivation());
        logic.musicScript.playBreakShieldSound();
        logic.uiScript.hideBoost();
    }
    public IEnumerator delayDeactivation()
    {
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (shieldStatus)
        {
            if (collision.gameObject.name.Contains("Asteroid"))
            {
                Destroy(collision.gameObject);
                deactivateShield();
            }
            else if (collision.gameObject.name.Contains("Sapphire"))
            {
                activateShield();
            }
        }
    }
}