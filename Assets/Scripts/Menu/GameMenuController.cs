using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool GameIsOver = false;

    //Observer to handle reloading text in hud
    public event Action<bool> OnPauseGameChanged;
    public event Action<bool> IsGameOver;

    [SerializeField]
    KeyCode PauseMenuKey = KeyCode.Escape;

    public GameObject gameOver;
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
        if(GameIsOver)
        {
            GameOver();
        }
        else
        {
            Resume();
        }
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
    public void GameOver()
    {
        GameIsOver = false;
        IsGameOver?.Invoke(GameIsOver);
    }
}
