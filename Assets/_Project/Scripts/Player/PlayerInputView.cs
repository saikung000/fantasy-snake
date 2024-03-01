using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputView : MonoBehaviour
{
    public PlayerInput playerInput;

    public Action<DirectionType> onMove;
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
        DirectionType move = MapInputVectorToMoveType(context.ReadValue<Vector2>());
        if (move == DirectionType.NoInput) return;
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

    private DirectionType MapInputVectorToMoveType(Vector2 inputVector)
    {

        if (inputVector.y == 1f)
            return DirectionType.Up;
        else if (inputVector.y == -1f)
            return DirectionType.Down;
        else if (inputVector.x == 1f)
            return DirectionType.Right;
        else if (inputVector.x == -1f)
            return DirectionType.Left;
        else
        {
            return DirectionType.NoInput;
        }
    }


}
