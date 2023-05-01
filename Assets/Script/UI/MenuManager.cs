using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelection;
    [SerializeField] private GameObject settings;

    [SerializeField] private GridLayoutGroup levelGrid;
    [SerializeField] private ScriptableObject[] scriptableObjects;
    [SerializeField] private LevelDisplay levelDisplayPrefab;

    private void Awake()
    {
        //DontDestroyOnLoad(this); 

        ShowMainMenu();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        PopulateLevels();
    }

    private void PopulateLevels()
    {
        foreach (Level level in scriptableObjects)
        {
            Instantiate(levelDisplayPrefab, levelGrid.transform).SetLevelInformation(level);
        }
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
