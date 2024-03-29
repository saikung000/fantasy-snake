using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuPanelPresenter : MonoBehaviour
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
            if (state == GameState.Menu)
            {
                EventSystem.current.SetSelectedGameObject(this.startButton.gameObject);
            }
        };


        startButton.onClick.AddListener(() =>
        {
            GameManager.Instance.StartGame();
            panel.SetActive(false);
        });
    }


}
