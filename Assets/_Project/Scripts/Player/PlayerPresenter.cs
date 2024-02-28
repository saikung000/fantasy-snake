using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPresenter : MonoInstance<PlayerPresenter>
{
    [SerializeField] private HeroView currentControlHero;
    [SerializeField] private PlayerInputView playerInputView;
    [SerializeField] private PlayerData playerData;


    void Start()
    {
        playerInputView.onMove = (MoveType type) => MoveHero(type);
        playerInputView.onNextHero = () => NextHero();
        playerInputView.onPreviousHero = () => PreviousHero();
        currentControlHero.OnControlHero();
    }



    private void MoveHero(MoveType type)
    {
        if (CheckCanMove(type, currentControlHero.currentMove))
        {
            for (int i = playerData.collectedHero.Count - 1; i > 0; i--)
            {
                playerData.collectedHero[i].Move(playerData.collectedHero[i - 1].currentMove);
                Debug.Log(i);
            }
            currentControlHero.Move(type);
        }
        else
        {

        }
    }

    private bool CheckCanMove(MoveType type, MoveType currentMove)
    {
        switch (type)
        {
            case MoveType.Up when currentMove == MoveType.Down:
                return false;
            case MoveType.Down when currentMove == MoveType.Up:
                return false;
            case MoveType.Left when currentMove == MoveType.Right:
                return false;
            case MoveType.Right when currentMove == MoveType.Left:
                return false;
            default:
                return true;
        }
    }

    private void PreviousHero()
    {
        List<HeroView> heroViewList = playerData.collectedHero;
        if (heroViewList.Count <= 1) return;

        Vector3 tempLastPosition = heroViewList[0].transform.position;
        MoveType tempLastCurrentMove = heroViewList[0].currentMove;
        for (int i = 0; i < heroViewList.Count -1; i++)
        {
            int swapIndex = i + 1;
            Debug.Log(i + ":" + swapIndex);
            heroViewList[i].SwapPosition(heroViewList[swapIndex]);
        }
        heroViewList.Last().ChangePosition(tempLastPosition, tempLastCurrentMove);




        currentControlHero.OnNotControlHero();
        HeroView current = heroViewList[heroViewList.Count - 1];
        heroViewList.Remove(current);
        heroViewList.Insert(0, current);
        currentControlHero = heroViewList[0];
        current.OnControlHero();

    }

    private void NextHero()
    {
        List<HeroView> heroViewList = playerData.collectedHero;
        if (heroViewList.Count <= 1) return;

        Vector3 tempLastPosition = heroViewList.Last().transform.position;
        MoveType tempLastCurrentMove = heroViewList.Last().currentMove;
        for (int i = playerData.collectedHero.Count - 1; i > 0; i--)
        {
            int swapIndex = i - 1;
            Debug.Log(i + ":" + swapIndex);
            heroViewList[i].SwapPosition(heroViewList[swapIndex]);
        }
        heroViewList[0].ChangePosition(tempLastPosition, tempLastCurrentMove);



        currentControlHero = heroViewList[1];
        currentControlHero.OnControlHero();
        HeroView current = heroViewList[0];
        heroViewList.RemoveAt(0);
        current.OnNotControlHero();
        playerData.collectedHero.Add(current);

    }

}
