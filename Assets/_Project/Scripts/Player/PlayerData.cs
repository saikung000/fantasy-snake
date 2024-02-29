using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public List<HeroView> collectedHero;

    public void AddHero(HeroView heroView)
    {
        collectedHero.Add(heroView);
    }

    public HeroView SwapNextHero()
    {
        HeroView current = collectedHero[0];
        collectedHero.RemoveAt(0);
        collectedHero.Add(current);
        return collectedHero[0];
    }

    public HeroView SwapPreviousHero()
    {
        HeroView current = collectedHero[collectedHero.Count - 1];
        collectedHero.Remove(current);
        collectedHero.Insert(0, current);
        return collectedHero[0];
    }



    public HeroView GetCurrentControlHero()
    {
        return collectedHero[0];
    }
}
