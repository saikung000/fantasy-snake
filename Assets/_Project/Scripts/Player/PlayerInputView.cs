using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputView : MonoBehaviour
{
    public PlayerInput playerInput;



    void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.gameplay.move.performed += OnMove;
        playerInput.gameplay.nextHero.performed += OnNextHero;
        playerInput.gameplay.previousHero.performed += OnPreviousHero;
    }

    void OnEnable()
    {
        playerInput.gameplay.Enable();
    }

    void OnDisable()
    {
        playerInput.gameplay.Disable();
    }


    private void OnMove(InputAction.CallbackContext context)
    {
        MoveType move = MapInputVectorToMoveType(context.ReadValue<Vector2>());
        if (move == MoveType.NoInput) return;
        Debug.Log("move : " + move);
    }

    private void OnNextHero(InputAction.CallbackContext context)
    {
        Debug.Log("next Hero");
    }

    private void OnPreviousHero(InputAction.CallbackContext context)
    {
        Debug.Log("previous Hero");
    }

    private MoveType MapInputVectorToMoveType(Vector2 inputVector)
    {

        if (inputVector.y == 1f)
            return MoveType.Up;
        else if (inputVector.y == -1f)
            return MoveType.Down;
        else if (inputVector.x == 1f)
            return MoveType.Right;
        else if (inputVector.x == -1f)
            return MoveType.Left;
        else
        {
            return MoveType.NoInput;
        }
    }


}

public enum MoveType
{
    Up,
    Down,
    Left,
    Right,
    NoInput
}
