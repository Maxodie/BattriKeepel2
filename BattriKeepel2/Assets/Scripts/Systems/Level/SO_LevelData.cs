using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/LevelData")]
public class SO_LevelData : ScriptableObject
{
    [HideInInspector] public string path;
}
