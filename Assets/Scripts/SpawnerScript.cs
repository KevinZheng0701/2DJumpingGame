using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public bool spawner = false;
    public bool spawnMultiplierActive = false;

    public Transform Ores;
    public Transform Gems;
    public GameObject asteroid;

    public float oreSpawnRate = 2;
    public float gemSpawnRate = 15;
    public float asteriodSpawnRate = 5;
    private float spawnRateMultiplier = 2;
    private float spawnMultiplierDuration = 10;
    [SerializeField] private float remainingTime = 0;
    private float activationTime;

    private float oreTimer;
    private float gemTimer;
    private float asteroidTimer;
    private float lastOreSpawnTime;
    private float lastGemSpawnTime;
    private float lastAsteroidSpawnTime;

    public LogicScript logic;

    private void Awake()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }
    void Start()
    {
        logic.uiScript.playButton.onClick.AddListener(playClicked);
        logic.uiScript.replayButton.onClick.AddListener(playClicked);
    }
    void Update()
    {
        if (logic.gameOverTriggered)
        {
            stopSpawning();
        }
        if (spawner)
        {
            spawn();
            if (spawnMultiplierActive && !logic.isPaused)
            {
                remainingTime = logic.calculateRemainingTime(activationTime, spawnMultiplierDuration);
                if (remainingTime <= 0)
                {
                    deactivateSpawnRateMultiplier();
                }
            }
        }   
    }

    private void playClicked()
    {
        lastOreSpawnTime = Time.time;
        lastAsteroidSpawnTime = Time.time;
        lastGemSpawnTime = Time.time;
        spawnOre();
    }
    private void stopSpawning()
    {
        spawner = false;
        deactivateSpawnRateMultiplier();
    }
    private void spawn()
    {
        float currentTime = Time.time;
        oreTimer = currentTime - lastOreSpawnTime;
        gemTimer = currentTime - lastGemSpawnTime;
        asteroidTimer = currentTime - lastAsteroidSpawnTime;
        if (oreTimer >= oreSpawnRate)
        {
            spawnOre();
            oreTimer = 0;
        }
        if (gemTimer >= gemSpawnRate)
        {
            spawnGem();
            gemTimer = 0;
        }
        if (asteroidTimer >= asteriodSpawnRate)
        {
            spawnAsteroid();
            asteroidTimer = 0;
        }
    }
    private void spawnOre()
    {
        if (Ores != null && Ores.childCount > 0)
        {
            int randomNumber = Random.Range(0, Ores.childCount);
            Transform randomOre = Ores.GetChild(randomNumber);
            if (randomOre != null)
            {
                GameObject ore = randomOre.gameObject;
                Vector3 oreScale = createRandomScale();
                Quaternion oreRotation = createRandomRotation();
                Vector3 orePosition = createRandomPosition();
                GameObject spawnedOre = Instantiate(ore, orePosition, oreRotation);
                spawnedOre.transform.localScale = oreScale;
                lastOreSpawnTime = Time.time;
            }
        }
    }
    private void spawnGem()
    {
        if (Gems != null && Gems.childCount > 0)
        {
            int randomNumber = Random.Range(0, Gems.childCount);
            Transform randomGem = Gems.GetChild(randomNumber);
            if (randomGem != null)
            {
                GameObject gem = randomGem.gameObject;
                Vector3 gemScale = createRandomScale();
                Quaternion gemRotation = createRandomRotation();
                Vector3 gemPosition = createRandomPosition();
                GameObject spawnedGem = Instantiate(gem, gemPosition, gemRotation);
                spawnedGem.transform.localScale = gemScale;
                lastGemSpawnTime = Time.time;
            }
        }
    }
    private void spawnAsteroid()
    {
        if (asteroid != null)
        {
            Vector3 asteroidScale = createRandomScale();
            Quaternion asteroidRotation = createRandomRotation();
            Vector3 asteroidPosition = createRandomPosition();
            GameObject spawnedAsteroid = Instantiate(asteroid, asteroidPosition, asteroidRotation);
            spawnedAsteroid.transform.localScale = asteroidScale;
            lastAsteroidSpawnTime = Time.time;
        }
    }
    private Vector3 createRandomScale()
    {
        Vector3 randomScale = new Vector3(Random.Range(0.7f, 1.2f), Random.Range(0.7f, 1.2f), 1.0f);
        return randomScale;
    }
    private Quaternion createRandomRotation()
    {
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        return randomRotation;
    }
    private Vector3 createRandomPosition()
    {
        Vector3 randomPosition = new Vector3(25, Random.Range(-10, 10), 0);
        return randomPosition;
    }
    public void activateSpawnRateMultiplier()
    {
        oreSpawnRate /= spawnRateMultiplier;
        spawnMultiplierActive = true;
        spawnMultiplierDuration = 10;
        activationTime = Time.time;
        logic.uiScript.boostText.text = "Double Spawn Rate";
        logic.uiScript.displayBoost();
    }
    public void deactivateSpawnRateMultiplier()
    {
        oreSpawnRate = 2;
        spawnMultiplierActive = false;
        spawnMultiplierDuration = 0;
        logic.uiScript.hideBoost();
    }
}