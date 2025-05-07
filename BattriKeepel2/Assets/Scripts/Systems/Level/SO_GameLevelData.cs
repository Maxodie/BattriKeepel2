using UnityEngine;

[CreateAssetMenu(fileName = "GameLevelData", menuName = "Level/GameLevelData")]
public class SO_GameLevelData : SO_LevelData
{
    public SO_LevelPhase[] levelPhases;
    public SO_LevelArtData levelArtData;
}
