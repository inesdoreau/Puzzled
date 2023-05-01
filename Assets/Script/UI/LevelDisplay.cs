using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private Image levelImage;
    [SerializeField] private Button playButton;

    private string levelToLoad;
    //[SerializeField] private GameObject lockIcon;

    public void SetLevelInformation(Level _level)
    {
        levelName.text = _level.name;
        levelNumber.text = _level.levelIndex.ToString();
        levelImage.sprite = _level.levelImage;

        playButton.onClick.AddListener(() => Debug.Log(_level));
        playButton.onClick.AddListener(() => SceneManager.LoadScene(_level.sceneToLoad.name));
    }

    private void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter");



        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1);
    }

    private void OnMouseExit()
    {
        transform.DOScale(new Vector3(1, 1, 1), 1);

    }
}
