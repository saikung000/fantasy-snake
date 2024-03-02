using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayPaneView : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI hpText;
    [SerializeField]
    private TextMeshProUGUI atkText;

    private GameManager gameManager;
    private HeroPresenter hero;
    void Awake()
    {
        panel.SetActive(false);
        gameManager = GameManager.Instance;
        GameManager.Instance.onGameStatsChange += (state) =>
        {
            panel.SetActive(state == GameState.Gameplay);
            scoreText.text = "Score : " + gameManager.score;
        };

        gameManager.onScoreUpdate += (score) =>
        {
            scoreText.text = "Score : " + score;
        };

        PlayerPresenter.Instance.onChangeControlHero += (hero) =>
        {
            hero.characterData.OnHpChange -= UpdateHp();
            this.hero = hero;
            hpText.text = "Hp : " + hero.GetHp();
            atkText.text = "Attack : " + hero.GetAttack();
            hero.characterData.OnHpChange += UpdateHp();
        };

    }

    private Action<int> UpdateHp()
    {
        return (hp) =>
        {
            hpText.text = "Hp : " + hp;
        };
    }
}
