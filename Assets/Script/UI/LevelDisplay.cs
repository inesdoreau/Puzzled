using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private TextMeshProUGUI levelDescription;
    [SerializeField] private Image levelImage;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject lockIcon;

    public void DisplayLevel(Level _level)
    {
        levelName.text = _level.name;
        levelDescription.text = _level.levelDescription;
        levelImage.sprite = _level.levelImage;

        bool mapUnlocked = PlayerPrefs.GetInt("currentScene", 0) >= _level.levelIndex;
        lockIcon.SetActive(!mapUnlocked);
        playButton.interactable = mapUnlocked;

        if(mapUnlocked)
        {
            levelImage.color = Color.white;
        }
        else
        {
            levelImage.color = Color.gray;
        }

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => SceneManager.LoadScene(_level.sceneToLoad.name));
    }
}
