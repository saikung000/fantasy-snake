using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnRate", menuName = "ScriptableObjects/SpawnRate", order = 1)]
public class SpawnRateScriptableObject : ScriptableObject
{

    [Header("Hero")]
    public int minSpawnHero = 1;
    public int maxSpawnHero = 4;
    public float heroSpawnRatePercent = 10;

    [Header("Enemy")]
    public int minSpawnEnemy = 1;
    public int maxSpawnEnemy = 4;
    public float enemySpawnRatePercent = 10;



    public int GetHeroSpawnAmount()
    {
        int amount = minSpawnHero;
        for (int i = 0; i < minSpawnHero - maxSpawnHero; i++)
        {
            if (RandomPercent(heroSpawnRatePercent))
                amount++;
        }
        return amount;
    }

    public int GetEnemySpawnAmount()
    {
        int amount = minSpawnEnemy;
        for (int i = 0; i < minSpawnEnemy- maxSpawnEnemy; i++)
        {
            if (RandomPercent(enemySpawnRatePercent))
                amount++;
        }
        return amount;
    }

    public bool RandomPercent(float percent)
    {
        return Random.Range (0f,100f) < percent;
    }
}


