using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPresenter : MonoBehaviour
{
    public HeroView heroView;
    public HpAtkTextView hpAtkTextView;
    public CharacterData characterData;
    public DirectionType currentDirection = DirectionType.Up;

    void Awake()
    {
        NotControlHero();
        Init();
    }

    private void Init()
    {
        hpAtkTextView.UpdateHpText(characterData.hp);
        hpAtkTextView.UpdateAtkText(characterData.attack);
        characterData.OnHpChange += (hp) => hpAtkTextView.UpdateHpText(hp);
    }

    public void Collected()
    {
        heroView.Collected();
    }

    public void ControlHero()
    {
        heroView.ControlHero();
        hpAtkTextView.SetActive(true);
    }
    public void NotControlHero()
    {
        heroView.NotControlHero();
        hpAtkTextView.SetActive(false);
    }

    public GameObject CheckMove(DirectionType type)
    {
        return heroView.CheckMove(type);
    }

    public void Move(DirectionType type)
    {
        ChangeDirection(type);
        heroView.Move(type);
    }

    public void ChangeDirection(DirectionType type)
    {
        currentDirection = type;
        heroView.Rotate(type);
    }

    public void SwapPosition(HeroPresenter anotherHero)
    {
        ChangeDirection(anotherHero.currentDirection);
        transform.position = anotherHero.transform.position;
    }

    public void ChangePosition(Vector3 tempLastPosition, DirectionType tempLastCurrentMove)
    {
        ChangeDirection(tempLastCurrentMove);
        transform.position = tempLastPosition;
    }

    public void MoveToFollowTarget(HeroPresenter targetHero)
    {
        ChangeDirection(targetHero.currentDirection);
        heroView.MoveToFollowTarget(targetHero);
    }

    public void TakeDamage(int damage)
    {
        characterData.TakeDamage(damage);
    }

    public int GetAttack()
    {
        return characterData.attack;
    }
}
