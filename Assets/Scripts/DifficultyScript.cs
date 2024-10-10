using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScript : MonoBehaviour
{
    public enum DifficultyLevel
    {
        Easy,
        Normal,
        Hard,
        Extreme,
        Impossible
    }
    public DifficultyLevel level = DifficultyLevel.Easy;
    private int[] difficultyThresholds = { 100, 300, 600, 1000 };
    private float[] asteriodSpawnRates = { 4f, 3f, 2f, 1.5f };
    [SerializeField] LogicScript logic;
    [SerializeField] SpawnerScript spawnerScript;

    void Update()
    {
        if (CheckPoints())
        {
            ChangeDifficult();
        }
    }

    private bool CheckPoints()
    {
        for (int i = difficultyThresholds.Length - 1; i >= 0; i--)
        {
            if (logic.playerScore > difficultyThresholds[i])
            {
                level = (DifficultyLevel)i;
                return true;
            }
        }
        return false;
    }

    private void ChangeDifficult()
    {
        int levelIndex = (int)level;
        if (levelIndex >= 0)
        {
            spawnerScript.asteriodSpawnRate = asteriodSpawnRates[levelIndex];
        }
    }
}
