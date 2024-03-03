using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{

    public HpAtkTextView hpAtkTextView;
    public CharacterData characterData = new CharacterData();
    public DirectionType currentDirection = DirectionType.Up;

    private Dictionary<DirectionType, Quaternion> rotateDict = new Dictionary<DirectionType, Quaternion>()
    {
        {DirectionType.Up,Quaternion.Euler(0, 0, 0)},
        {DirectionType.Down, Quaternion.Euler(0, 180, 0)},
        {DirectionType.Left, Quaternion.Euler(0, 270, 0)},
        {DirectionType.Right, Quaternion.Euler(0, 90, 0)},
    };



    public void Init(CharacterData characterData)
    {
        this.characterData = characterData;
        hpAtkTextView.UpdateHpText(characterData.hp);
        hpAtkTextView.UpdateAtkText(characterData.attack);
        characterData.OnHpChange += (hp) => hpAtkTextView.UpdateHpText(hp);
        characterData.OnAtkChange += (atk) => hpAtkTextView.UpdateAtkText(atk);
        PlayerPresenter.Instance.onPlayerMove += () => characterData.GrowingStat();
        hpAtkTextView.SetActive(true);
    }

    public void ChangeDirection(DirectionType type)
    {
        currentDirection = type;
        Quaternion rotateTo = rotateDict[type];
        transform.rotation = rotateTo;
    }

    public void TakeDamage(int damage)
    {
        characterData.TakeDamage(damage);
        if (characterData.hp <= 0)
        {
            MapSpawnerManager.Instance.RemoveEnemy(this);
            GameManager.Instance.AddScore(1);
            Destroy(this.gameObject);
            MapSpawnerManager.Instance.SpawnNewEnemy();
        }
    }

    public int GetAttack()
    {
        return characterData.attack;
    }

    public void RotateTo(DirectionType type)
    {
        DirectionType resultType = DirectionType.Up;
        switch (type)
        {
            case DirectionType.Up:
                resultType = DirectionType.Down;
                break;
            case DirectionType.Down:
                resultType = DirectionType.Up;
                break;
            case DirectionType.Left:
                resultType = DirectionType.Right;
                break;
            case DirectionType.Right:
                resultType = DirectionType.Left;
                break;
        }
        Quaternion rotateTo = rotateDict[resultType];
        transform.rotation = rotateTo;
        currentDirection = resultType;
    }

}

