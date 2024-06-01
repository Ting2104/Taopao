using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameMenuController MenuControllerComponent;
    public GameObject PauseMenuPanel;


    // Start is called before the first frame update
    void Start()
    {
        MenuControllerComponent.OnPauseGameChanged += TogglePauseMenu;
    }

    void TogglePauseMenu(bool isGamePaused)
    {
        PauseMenuPanel.SetActive(isGamePaused);
    }
}
