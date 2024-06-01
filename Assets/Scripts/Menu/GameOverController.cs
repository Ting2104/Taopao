using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public GameMenuController MenuControllerComponent;
    public GameObject GameOverPanel;
    bool gameOver;
    private void Start()
    {
        //MenuControllerComponent.IsGameOver += ToggleOverMenu;
    }

    void ToggleOverMenu(bool isGameOver)
    {
        GameOverPanel.SetActive(isGameOver);
    }
}
