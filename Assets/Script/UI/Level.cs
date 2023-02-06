using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="New Level", menuName ="Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    public int levelIndex;
    public string levelName;
    public string levelDescription;
    public Sprite levelImage;
    public Object sceneToLoad;

}
