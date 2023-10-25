using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuCtrl : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUI;
    public Button resumeButton;
    public Button optionsButton;
    public Button exitButton;

    private void Start()
    {
        resumeButton.onClick.AddListener(() => ClickResume());
        optionsButton.onClick.AddListener(() => ClickOptions());
        exitButton.onClick.AddListener(() => ClickExit());
    }
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.P)))
        {
            if (!GameManager.isPause)
                CallMenu();
            else
                CloseMenu();
        }
    }

    private void CallMenu()
    {
        GameManager.isPause = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;   
        go_BaseUI.SetActive(true);
    }

    private void CloseMenu()
    {
        GameManager.isPause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        go_BaseUI.SetActive(false);
    }

    public void ClickResume()
    {
        Debug.Log("sex");
        CloseMenu();
    }

    public void ClickOptions() 
    { 

    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
