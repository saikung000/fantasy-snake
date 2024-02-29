using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoInstance<GameManager>
{
    public GameState gameState = GameState.Menu;
    public Action onGameOver;
    public Action onMenu;
    public Action onPlayerTurn;
    public Action onEnemyTurn;

    internal void CheckToGameOver()
    {
        gameState = GameState.GameOver;
        onGameOver?.Invoke();
    }
}

public enum GameState
{
    Menu,
    PlayerTurn,
    EnemyTurn,
    GameOver,
}