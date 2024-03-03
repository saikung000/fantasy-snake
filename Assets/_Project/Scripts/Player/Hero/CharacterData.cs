using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData
{
    public int hp = 100;
    public int attack = 10;
    public int maxHpStat = 100;
    public int maxAttackStat = 100;
    public float hpStatGrow = 0.1f;
    public float atkStatGrow = 0.1f;
    public Action<int> OnHpChange;
    public Action<int> OnAtkChange;

    public CharacterData()
    {
    }

    public CharacterData(int hp, int attack, int maxHpStat, int maxAttackStat, float hpStatGrow, float atkStatGrow)
    {
        this.hp = hp;
        this.attack = attack;
        this.maxHpStat = maxHpStat;
        this.maxAttackStat = maxAttackStat;
        this.hpStatGrow = hpStatGrow;
        this.atkStatGrow = atkStatGrow;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        OnHpChange?.Invoke(hp);
    }
    public void GrowingStat()
    {
        hp = Mathf.Clamp(Mathf.CeilToInt(hp + (hp * hpStatGrow)), 0, maxHpStat);
        attack = Mathf.Clamp(Mathf.CeilToInt(attack + (attack * atkStatGrow)), 0, maxAttackStat);
        OnHpChange?.Invoke(hp);
        OnAtkChange?.Invoke(attack);
    }
}
