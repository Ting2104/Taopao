using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverPanelController : MonoBehaviour
{
    public GameMenuController MenuControllerComponent;
    public GameObject OverMenuPanel;


    // Start is called before the first frame update
    void Start()
    {
        MenuControllerComponent = FindAnyObjectByType<GameMenuController>();
    }
    private void Update()
    {
        OverMenuPanel.SetActive(MenuControllerComponent.GameIsOver);
    }
}
