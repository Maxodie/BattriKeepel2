using UnityEngine;

[CreateAssetMenu(fileName = "GameLevelData", menuName = "Level/GameLevelData")]
public class SO_GameLevelData : SO_LevelData
{
    public float phaseTransitionDelay = 1.5f;
    public SO_LevelPhase[] levelPhases;
    public SO_LevelArtData levelArtData;

    [Header("Specifics")]
    public SO_UIGameTransitionData transitionPrefab;
}
