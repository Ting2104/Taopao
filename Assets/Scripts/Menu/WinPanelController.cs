using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanelController : MonoBehaviour
{
    public GameMenuController MenuControllerComponent;
    public GameObject WinMenuPanel;


    // Start is called before the first frame update
    void Start()
    {
        MenuControllerComponent = FindAnyObjectByType<GameMenuController>();
    }
    private void Update()
    {
        WinMenuPanel.SetActive(MenuControllerComponent.GameIsWon);
    }
}
