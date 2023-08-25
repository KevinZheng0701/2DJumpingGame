using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public int scoreValue;
    public LogicScript logic;

    private void Awake()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        applyScoreAndSound();
        applySpecialPowers();
        Destroy(gameObject);
    }
    private void applyScoreAndSound() 
    {
        int score = logic.pointBoostedActivated ? 2 * scoreValue : scoreValue;
        logic.addScore(score);
        if (gameObject.name.Contains("Ruby") || gameObject.name.Contains("Emerald") || gameObject.name.Contains("Sapphire"))
        {
            logic.musicScript.playBoostSound();
        }
        else
        {
            logic.musicScript.playPointsSound();
        }
    }
    private void applySpecialPowers()
    {
        if (gameObject.name.Contains("Sapphire"))
        {
            logic.playerScript.shieldScript.activateShield();
        }
        if (gameObject.name.Contains("Ruby"))
        {
            logic.activatePointsMultiplier();
        }
        if (gameObject.name.Contains("Emerald"))
        {
            logic.spawnerScript.activateSpawnRateMultiplier();
        }
    }
}