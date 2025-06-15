using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;
    public Button continueButton;

    private void Awake()
    {
        continueButton.interactable = PlayerPrefs.HasKey("Current_Scene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    { }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
