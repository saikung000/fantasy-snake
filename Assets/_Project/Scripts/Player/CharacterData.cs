using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public int hp;
    public int attack;

    public Action<int> OnHpChange;
    public void TakeDamage(int damage)
    {
        hp -= damage;
        OnHpChange?.Invoke(hp);
    }
}
