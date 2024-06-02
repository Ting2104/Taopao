using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool GameIsOver = false;
    public static bool GameIsWin = false;

    //Observer to handle reloading text in hud
    public event Action<bool> OnPauseGameChanged;

    [SerializeField]
    KeyCode PauseMenuKey = KeyCode.Escape;
    [SerializeField]
    KeyCode OverMenuKey = KeyCode.R;

    public Combat combatP, combatE;
    public GameObject GameOverPanel;
    public GameObject WinPanel;
    void Star()
    {
        combatP = FindObjectOfType<Combat>();
        combatE = FindObjectOfType<Combat>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PauseMenuKey))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (combatP.deathPlayer || Input.GetKeyDown(OverMenuKey))
        {
            GameOver();
        }
        else
            GameIsOver = false;
        if (combatE.deathEnemy)
        {
            YouWin();
        }
        else
            GameIsWin = false;
    }
    public void Resume()
    {
        GameIsPaused = false;
        Time.timeScale = 1.0f;

        OnPauseGameChanged?.Invoke(GameIsPaused);
    }
    public void Pause()
    {
        GameIsPaused = true;
        Time.timeScale = 0.0f;

        OnPauseGameChanged?.Invoke(GameIsPaused);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Resume();
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void GameOver()
    {
        GameIsOver = true;
        GameOverPanel.SetActive(GameIsOver);
    }
    public void YouWin()
    {
        GameIsWin = true;
        WinPanel.SetActive(GameIsWin);
    }
}
