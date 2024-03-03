using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoInstance<GameManager>
{
    public GameState gameState = GameState.Menu;
    public int score = 0;
    public Action<GameState> onGameStatsChange;
    public Action<int> onScoreUpdate;

    void Start()
    {
        gameState = GameState.Menu;
        onGameStatsChange?.Invoke(gameState);
    }

    public void StartGame()
    {
        gameState = GameState.Gameplay;
        score = 0;
        PlayerPresenter.Instance.Reset();
        MapSpawnerManager.Instance.SetupGame();
        onGameStatsChange?.Invoke(gameState);
    }

    public void GameOver()
    {
        gameState = GameState.GameOver;
        onGameStatsChange?.Invoke(gameState);
    }

    public void AddScore(int score)
    {
        this.score += score;
        onScoreUpdate?.Invoke( this.score);
    }
}

public enum GameState
{
    Menu,
    Gameplay,
    GameOver,
}