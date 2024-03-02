using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnStat", menuName = "ScriptableObjects/SpawnStat", order = 1)]
public class SpawnStatScriptableObject : ScriptableObject
{

    [Header("Hero")]
    public int minHeroHp = 100;
    public int maxHeroHp = 100;
    public int minHeroAtk = 20;
    public int maxHeroAtk = 20;
    public float growingCoefficientHeroStats = 0.5f;

    [Header("Enemy")]
    public int minEnemyHp = 100;
    public int maxEnemyHp = 100;
    public int minEnemyAtk = 20;
    public int maxEnemyAtk = 20;
    public float growingCoefficientEnemyStats = 0.5f;


    public (int hp, int atk) RandomHeroStat(int level)
    {
        float growingStats = 1 + growingCoefficientHeroStats * (level - 1);
        int hp = Mathf.Clamp(Mathf.CeilToInt(minHeroHp * growingStats), minHeroHp, maxHeroHp);
        int atk = Mathf.Clamp(Mathf.CeilToInt(minHeroAtk * growingStats), minHeroAtk, maxHeroAtk);
        return (hp, atk);
    }

    public (int hp, int atk) RandomEnemyStat(int level)
    {

        float growingStats = 1 + growingCoefficientEnemyStats * (level - 1);
        int hp = Mathf.Clamp(Mathf.CeilToInt(minEnemyHp * growingStats), minEnemyHp, maxEnemyHp);
        int atk = Mathf.Clamp(Mathf.CeilToInt(minEnemyAtk * growingStats), minEnemyAtk, maxEnemyAtk);
        return (hp, atk);
    }
}


