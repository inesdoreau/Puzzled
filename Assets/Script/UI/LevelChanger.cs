using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] scriptableObjects;
    [SerializeField] private LevelDisplay levelDisplay;

    private int currentIndex;

    private void Awake()
    {
        ChangeLevel(0);
    }

    public void ChangeLevel(int _change)
    {
        currentIndex += _change;

        if(currentIndex < 0)
        {
            currentIndex = scriptableObjects.Length - 1;
        }
        else if (currentIndex > scriptableObjects.Length - 1)
        {   
            currentIndex= 0;
        }

        if(levelDisplay != null)
        {
            levelDisplay.SetLevelInformation((Level)scriptableObjects[currentIndex]);
        }
    }
}
