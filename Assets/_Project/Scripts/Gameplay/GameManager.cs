using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoInstance<GameManager>
{
    public GameState gameState = GameState.Menu;
    public int score = 0;
    public Action onGameOver;
    public Action onMenu;
    public Action onPlayerTurn;
    public Action onEnemyTurn;

    void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        PlayerPresenter.Instance.Reset();
        score = 0;
        MapSpawnerView.Instance.SetupGame();
    }

    public void CheckToGameOver()
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