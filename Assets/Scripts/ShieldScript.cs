using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public bool shieldStatus = false;
    private float shieldDuration = 10;
    private float timer;
    [SerializeField] Animator animator;
    [SerializeField] LogicScript logic;
    [SerializeField] MusicScript musicScript;
    [SerializeField] UIScript uiScript;
    [SerializeField] PlayerScript playerScript;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!playerScript.inScreen)
        {
            shieldStatus = false;
            gameObject.SetActive(false);
        }
        if (shieldStatus && !logic.isPaused)
        {
            timer += Time.deltaTime;
            if (timer >= shieldDuration)
            {
                DeactivateShield();
            }
        }
    }

    public void ActivateShield()
    {
        shieldStatus = true;
        animator.SetBool("ShieldActivated", true);
        gameObject.SetActive(true);
        musicScript.PlayShieldSound();
        uiScript.DisplayBoost("Shield Activated");
    }

    private void DeactivateShield()
    {
        shieldStatus = false;
        animator.SetBool("ShieldActivated", false);
        StartCoroutine(delayDeactivation());
        musicScript.PlayBreakShieldSound();
        uiScript.HideBoost();
        timer = 0;
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
                DeactivateShield();
            }
            else if (collision.gameObject.name.Contains("Sapphire"))
            {
                ActivateShield();
            }
        }
    }
}