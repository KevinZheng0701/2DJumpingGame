using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public bool spawner = false;
    public bool spawnMultiplierActive = false;
    [SerializeField] Transform Ores;
    [SerializeField] Transform Gems;
    [SerializeField] GameObject asteroid;
    public float oreSpawnRate = 2;
    public float gemSpawnRate = 15;
    public float asteriodSpawnRate = 5;
    private float timer;
    private float oreTimer;
    private float gemTimer;
    private float asteroidTimer;
    private float spawnRateMultiplier = 2;
    private float spawnMultiplierDuration = 10;
    [SerializeField] LogicScript logic;
    [SerializeField] UIScript uiScript;

    void Start()
    {
        uiScript.playButton.onClick.AddListener(PlayClicked);
        uiScript.replayButton.onClick.AddListener(PlayClicked);
    }

    void Update()
    {
        if (spawner)
        {
            Spawn();
            if (spawnMultiplierActive)
            {
                timer += Time.deltaTime;
                if (timer >= spawnMultiplierDuration)
                {
                    DeactivateSpawnRateMultiplier();
                }
            }
        }
    }

    private void PlayClicked()
    {
        SpawnOre();
    }

    public void StopSpawning()
    {
        spawner = false;
        DeactivateSpawnRateMultiplier();
    }

    public void ResetSpawnTime()
    {
        oreTimer = 0;
        gemTimer = 0;
        asteroidTimer = 0;
    }

    private void Spawn()
    {
        oreTimer += Time.deltaTime;
        gemTimer += Time.deltaTime;
        asteroidTimer += Time.deltaTime;
        if (oreTimer >= oreSpawnRate)
        {
            SpawnOre();
            oreTimer = 0;
        }
        if (gemTimer >= gemSpawnRate)
        {
            SpawnGem();
            gemTimer = 0;
        }
        if (asteroidTimer >= asteriodSpawnRate)
        {
            SpawnAsteroid();
            asteroidTimer = 0;
        }
    }

    private void SpawnOre()
    {
        int randomNumber = Random.Range(0, Ores.childCount);
        Transform randomOre = Ores.GetChild(randomNumber);
        GameObject ore = randomOre.gameObject;
        Vector3 oreScale = CreateRandomScale();
        Quaternion oreRotation = CreateRandomRotation();
        Vector3 orePosition = CreateRandomPosition();
        GameObject spawnedOre = Instantiate(ore, orePosition, oreRotation);
        spawnedOre.transform.localScale = oreScale;
    }

    private void SpawnGem()
    {
        int randomNumber = Random.Range(0, Gems.childCount);
        Transform randomGem = Gems.GetChild(randomNumber);
        GameObject gem = randomGem.gameObject;
        Vector3 gemScale = CreateRandomScale();
        Quaternion gemRotation = CreateRandomRotation();
        Vector3 gemPosition = CreateRandomPosition();
        GameObject spawnedGem = Instantiate(gem, gemPosition, gemRotation);
        spawnedGem.transform.localScale = gemScale;
    }

    private void SpawnAsteroid()
    {
        Vector3 asteroidScale = CreateRandomScale();
        Quaternion asteroidRotation = CreateRandomRotation();
        Vector3 asteroidPosition = CreateRandomPosition();
        GameObject spawnedAsteroid = Instantiate(asteroid, asteroidPosition, asteroidRotation);
        spawnedAsteroid.transform.localScale = asteroidScale;
    }

    private Vector3 CreateRandomScale()
    {
        Vector3 randomScale = new Vector3(Random.Range(0.7f, 1.2f), Random.Range(0.7f, 1.2f), 1.0f);
        return randomScale;
    }

    private Quaternion CreateRandomRotation()
    {
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        return randomRotation;
    }

    private Vector3 CreateRandomPosition()
    {
        Vector3 randomPosition = new Vector3(25, Random.Range(-10, 10), 0);
        return randomPosition;
    }

    public void ActivateSpawnRateMultiplier()
    {
        oreSpawnRate /= spawnRateMultiplier;
        spawnMultiplierActive = true;
        uiScript.DisplayBoost("Double Spawn Rate");
    }

    public void DeactivateSpawnRateMultiplier()
    {
        oreSpawnRate = 2;
        spawnMultiplierActive = false;
        uiScript.HideBoost();
        timer = 0;
    }
}