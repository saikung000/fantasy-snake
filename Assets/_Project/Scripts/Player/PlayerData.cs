using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public List<HeroPresenter> collectedHeroList;

    public void AddHero(HeroPresenter heroPresenter)
    {
        collectedHeroList.Add(heroPresenter);
    }

    public HeroPresenter RemoveFirstHero()
    {
        collectedHeroList.RemoveAt(0);
        return collectedHeroList[0];
    }

    public HeroPresenter RemoveLastHero()
    {
        HeroPresenter last = collectedHeroList.Last();
        collectedHeroList.Remove(last);
        return last;
    }

    public HeroPresenter SwapNextHero()
    {
        HeroPresenter current = collectedHeroList[0];
        collectedHeroList.RemoveAt(0);
        collectedHeroList.Add(current);
        return collectedHeroList[0];
    }

    public HeroPresenter SwapPreviousHero()
    {
        HeroPresenter current = collectedHeroList[collectedHeroList.Count - 1];
        collectedHeroList.Remove(current);
        collectedHeroList.Insert(0, current);
        return collectedHeroList[0];
    }



    public HeroPresenter GetCurrentControlHero()
    {
        return collectedHeroList[0];
    }
}
