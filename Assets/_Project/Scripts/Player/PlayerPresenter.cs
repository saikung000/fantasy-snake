using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPresenter : MonoInstance<PlayerPresenter>
{
    [SerializeField] private HeroPresenter currentControlHero;
    [SerializeField] private PlayerInputView playerInputView;
    [SerializeField] private PlayerData playerData;


    void Start()
    {
        playerInputView.onMove = (DirectionType type) => MoveHero(type);
        playerInputView.onNextHero = () => NextHero();
        playerInputView.onPreviousHero = () => PreviousHero();
    }



    private void MoveHero(DirectionType type)
    {
        if (CheckCanMove(type, currentControlHero.currentDirection) && GameManager.Instance.gameState == GameState.Gameplay)
        {
            CheckMoveTo(type);
        }
    }

    private void CheckMoveTo(DirectionType type)
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
            HeroPresenter heroPresenter = hit.GetComponent<HeroPresenter>();
            heroPresenter.Collected();
            Move(type);
            heroPresenter.MoveToFollowTarget(playerData.collectedHero.Last());
            playerData.AddHero(heroPresenter);
            heroPresenter.transform.SetParent(transform);
            MapSpawnerView.Instance.RemoveCollectHero(heroPresenter);

        }
        else if (hit.CompareTag("Enemy"))
        {

        }
        else
        {
            Move(type);
        }
    }

    private void Move(DirectionType type)
    {
        for (int i = playerData.collectedHero.Count - 1; i > 0; i--)
        {
            playerData.collectedHero[i].Move(playerData.collectedHero[i - 1].currentDirection);
        }
        currentControlHero.Move(type);
    }



    private bool CheckCanMove(DirectionType type, DirectionType currentMove)
    {
        switch (type)
        {
            case DirectionType.Up when currentMove == DirectionType.Down:
                return false;
            case DirectionType.Down when currentMove == DirectionType.Up:
                return false;
            case DirectionType.Left when currentMove == DirectionType.Right:
                return false;
            case DirectionType.Right when currentMove == DirectionType.Left:
                return false;
            default:
                return true;
        }
    }

    private void PreviousHero()
    {
        List<HeroPresenter> heroViewList = playerData.collectedHero;
        if (heroViewList.Count <= 1) return;

        currentControlHero.NotControlHero();

        Vector3 tempLastPosition = heroViewList[0].transform.position;
        DirectionType tempLastCurrentMove = heroViewList[0].currentDirection;
        for (int i = 0; i < heroViewList.Count - 1; i++)
        {
            int swapIndex = i + 1;
            Debug.Log(i + ":" + swapIndex);
            heroViewList[i].SwapPosition(heroViewList[swapIndex]);
        }
        heroViewList.Last().ChangePosition(tempLastPosition, tempLastCurrentMove);


        currentControlHero = playerData.SwapPreviousHero();
        currentControlHero.ControlHero();

    }

    private void NextHero()
    {
        List<HeroPresenter> heroViewList = playerData.collectedHero;
        if (heroViewList.Count <= 1) return;

        currentControlHero.NotControlHero();

        Vector3 tempLastPosition = heroViewList.Last().transform.position;
        DirectionType tempLastCurrentMove = heroViewList.Last().currentDirection;
        for (int i = heroViewList.Count - 1; i > 0; i--)
        {
            int swapIndex = i - 1;
            Debug.Log(i + ":" + swapIndex);
            heroViewList[i].SwapPosition(heroViewList[swapIndex]);
        }
        heroViewList[0].ChangePosition(tempLastPosition, tempLastCurrentMove);



        currentControlHero = playerData.SwapNextHero();
        currentControlHero.ControlHero();
    }

    public void AddHero(HeroPresenter heroView)
    {
        currentControlHero = heroView;
        heroView.transform.SetParent(transform);
        playerData.collectedHero.Add(heroView);
        currentControlHero.Collected();
        currentControlHero.ControlHero();
    }

    public void Reset()
    {
        playerData.collectedHero.Clear();
        foreach (GameObject child in transform)
        {
            Destroy(child);
        }
    }
}
