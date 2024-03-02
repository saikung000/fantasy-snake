using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelView : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Button startButton;
    
    void Awake()
    {
        GameManager.Instance.onGameStatsChange += (state) =>
        {
            panel.SetActive(state == GameState.Menu);
        };

        startButton.onClick.AddListener(() =>
        {
            GameManager.Instance.StartGame();
            panel.SetActive(false);
        });
    }


}
