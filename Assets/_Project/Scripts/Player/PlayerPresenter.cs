using System;
using System.Collections;
using System.Collections.Generic;
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
    }



    private void MoveHero(MoveType type)
    {
        if (CheckCanMove(type, currentControlHero.currentMove))
            currentControlHero.Move(type);
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
        throw new NotImplementedException();
    }

    private void NextHero()
    {
        throw new NotImplementedException();
    }

}
