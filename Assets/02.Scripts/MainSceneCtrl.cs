using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Unity.VisualScripting;

public class MainSceneCtrl : MonoBehaviour
{
    public Button startButton;
    public Button contButton;
    public Button optionButton;

    private UnityAction action;

    void Start()
    {
        action = () => OnButtonClick(startButton.name);
        startButton.onClick.AddListener(action); ;
        contButton.onClick.AddListener(delegate { OnButtonClick(contButton.name); });
        optionButton.onClick.AddListener(() => OnButtonClick(optionButton.name));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick(string msg)
    {
        switch(msg){
            case "Button_NewGame":
                SceneManager.LoadScene(2);
                break;
            }
    }
}
