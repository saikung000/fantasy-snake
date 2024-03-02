using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPaneView : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private Button restartButton;
    private GameManager gameManager;
    void Awake()
    {
        gameManager = GameManager.Instance;
        GameManager.Instance.onGameStatsChange += (state) =>
       {
           panel.SetActive(state == GameState.GameOver);
           scoreText.text = "Score : " + gameManager.score;
       };

        restartButton.onClick.AddListener(() =>
        {
            gameManager.StartGame();
            panel.SetActive(false);
        });
    }


}
