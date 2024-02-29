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
            CheckMoveTo(type);
        }
    }

    private void CheckMoveTo(MoveType type)
    {
        GameObject hit = currentControlHero.CheckMove(type);
        if (hit == null)
        {
            Move(type);
        }
        else if (hit.CompareTag("Obstacle") || hit.CompareTag("Hero"))
        {
            GameManager.Instance.CheckToGameOver();
        }
        else if (hit.CompareTag("CollectHero"))
        {
            HeroView heroView = hit.GetComponent<HeroView>();
            heroView.Collected();
            Move(type);
            heroView.MoveFollow(playerData.collectedHero.Last());
            playerData.AddHero(heroView);

        }
        else if (hit.CompareTag("Enemy"))
        {

        }
        else
        {
            Move(type);
        }
    }

    private void Move(MoveType type)
    {
        for (int i = playerData.collectedHero.Count - 1; i > 0; i--)
        {
            playerData.collectedHero[i].Move(playerData.collectedHero[i - 1].currentMove);
        }
        currentControlHero.Move(type);
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

        currentControlHero.OnNotControlHero();

        Vector3 tempLastPosition = heroViewList[0].transform.position;
        MoveType tempLastCurrentMove = heroViewList[0].currentMove;
        for (int i = 0; i < heroViewList.Count - 1; i++)
        {
            int swapIndex = i + 1;
            Debug.Log(i + ":" + swapIndex);
            heroViewList[i].SwapPosition(heroViewList[swapIndex]);
        }
        heroViewList.Last().ChangePosition(tempLastPosition, tempLastCurrentMove);


        currentControlHero = playerData.SwapPreviousHero();
        currentControlHero.OnControlHero();

    }

    private void NextHero()
    {
        List<HeroView> heroViewList = playerData.collectedHero;
        if (heroViewList.Count <= 1) return;

        currentControlHero.OnNotControlHero();

        Vector3 tempLastPosition = heroViewList.Last().transform.position;
        MoveType tempLastCurrentMove = heroViewList.Last().currentMove;
        for (int i = heroViewList.Count - 1; i > 0; i--)
        {
            int swapIndex = i - 1;
            Debug.Log(i + ":" + swapIndex);
            heroViewList[i].SwapPosition(heroViewList[swapIndex]);
        }
        heroViewList[0].ChangePosition(tempLastPosition, tempLastCurrentMove);



        currentControlHero = playerData.SwapNextHero();
        currentControlHero.OnControlHero();
    }

}
