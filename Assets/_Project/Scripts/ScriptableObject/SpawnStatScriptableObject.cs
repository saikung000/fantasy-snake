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
    public float growingHpHeroStat = 0.5f;
    public float growingAtkHeroStat = 0.5f;

    [Header("Enemy")]
    public int minEnemyHp = 100;
    public int maxEnemyHp = 100;
    public int minEnemyAtk = 20;
    public int maxEnemyAtk = 20;
    public float growingHpEnemyStat = 0.5f;
    public float growingAtkEnemyStat = 0.5f;


    public CharacterData CreateHeroStat()
    {
        return new CharacterData(minHeroHp, minHeroAtk, maxHeroHp, maxHeroAtk, growingHpHeroStat, growingAtkHeroStat);
    }

    public CharacterData CreateEnemyStat()
    {

        return new CharacterData(minEnemyHp, minEnemyAtk, maxEnemyHp, maxEnemyAtk, growingHpEnemyStat, growingAtkEnemyStat);
    }
}


