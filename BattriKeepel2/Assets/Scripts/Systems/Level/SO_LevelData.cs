using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/LevelData")]
public class SO_LevelData : ScriptableObject
{
    [SerializeField] public Sprite levelImage;
    [HideInInspector] public string path;
}
