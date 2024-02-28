using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputView : MonoBehaviour
{
    public PlayerInput playerInput;

    public Action<MoveType> onMove;
    public Action onNextHero;
    public Action onPreviousHero;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.gameplay.move.performed += Move;
        playerInput.gameplay.nextHero.performed += NextHero;
        playerInput.gameplay.previousHero.performed += PreviousHero;
    }

    void OnEnable()
    {
        playerInput.gameplay.Enable();
    }

    void OnDisable()
    {
        playerInput.gameplay.Disable();
    }


    private void Move(InputAction.CallbackContext context)
    {
        MoveType move = MapInputVectorToMoveType(context.ReadValue<Vector2>());
        if (move == MoveType.NoInput) return;
        Debug.Log("move : " + move);
        onMove?.Invoke(move);
    }

    private void NextHero(InputAction.CallbackContext context)
    {
        Debug.Log("next Hero");
        onNextHero?.Invoke();
    }

    private void PreviousHero(InputAction.CallbackContext context)
    {
        Debug.Log("previous Hero");
        onPreviousHero?.Invoke();
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
