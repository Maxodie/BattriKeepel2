using UnityEngine;

[CreateAssetMenu(fileName = "FrogLevelData", menuName = "Frog/SO_FrogLevelData")]
public class SO_FrogLevelData : ScriptableObject
{
    public FrogGraphics graphics;
    public float leapCooldown;

    public int m_minimumRunLevel;
    public int m_maximumRunLevel;

    public int m_minimumFlyLevel;
    public int m_maximumFlyLevel;

    public int m_minimumSwimLevel;
    public int m_maximumSwimLevel;
}
