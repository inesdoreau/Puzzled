using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelection;
    [SerializeField] private GameObject settings;

    private void Awake()
    {
        //DontDestroyOnLoad(this); 

        ShowMainMenu();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        levelSelection.SetActive(false);
        settings.SetActive(false);
    }

    public void ShowLevelSelection()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
        settings.SetActive(false);
    }

    public void ShowSettings()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(false);
        settings.SetActive(true);
    }

    public void QuitGame()
    {       
        Application.Quit();
    }
}
