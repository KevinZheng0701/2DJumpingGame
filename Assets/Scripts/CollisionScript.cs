using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public int scoreValue;
    private LogicScript logic;
    private MusicScript musicScript;
    private ShieldScript shieldScript;
    private SpawnerScript spawnerScript;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        musicScript = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>();
        spawnerScript = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerScript>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        shieldScript = player.GetComponentInChildren<ShieldScript>(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyScoreAndSound();
        ApplySpecialPowers();
        Destroy(gameObject);
    }

    private void ApplyScoreAndSound()
    {
        int score = logic.pointBoostedActivated ? 2 * scoreValue : scoreValue;
        logic.AddScore(score);
        if (gameObject.name.Contains("Ruby") || gameObject.name.Contains("Emerald") || gameObject.name.Contains("Sapphire"))
        {
            musicScript.PlayBoostSound();
        }
        else
        {
            musicScript.PlayPointsSound();
        }
    }

    private void ApplySpecialPowers()
    {
        if (gameObject.name.Contains("Sapphire"))
        {
            shieldScript.ActivateShield();
        }
        if (gameObject.name.Contains("Ruby"))
        {
            logic.ActivatePointsMultiplier();
        }
        if (gameObject.name.Contains("Emerald"))
        {
            spawnerScript.ActivateSpawnRateMultiplier();
        }
    }
}