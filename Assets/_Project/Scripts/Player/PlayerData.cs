using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public List<HeroPresenter> collectedHero;

    public void AddHero(HeroPresenter heroPresenter)
    {
        collectedHero.Add(heroPresenter);
    }

    public HeroPresenter SwapNextHero()
    {
        HeroPresenter current = collectedHero[0];
        collectedHero.RemoveAt(0);
        collectedHero.Add(current);
        return collectedHero[0];
    }

    public HeroPresenter SwapPreviousHero()
    {
        HeroPresenter current = collectedHero[collectedHero.Count - 1];
        collectedHero.Remove(current);
        collectedHero.Insert(0, current);
        return collectedHero[0];
    }



    public HeroPresenter GetCurrentControlHero()
    {
        return collectedHero[0];
    }
}
