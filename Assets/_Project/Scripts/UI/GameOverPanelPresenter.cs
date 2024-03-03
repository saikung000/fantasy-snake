using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverPanelPresenter : MonoBehaviour
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
           if (state == GameState.GameOver)
           {
               scoreText.text = "Score : " + gameManager.score;
               EventSystem.current.SetSelectedGameObject(this.restartButton.gameObject);
           }
       };

        restartButton.onClick.AddListener(() =>
        {
            gameManager.StartGame();
            panel.SetActive(false);
        });
    }


}
