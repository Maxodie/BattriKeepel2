using UnityEngine;

[CreateAssetMenu(fileName = "WaveDataScriptableObject", menuName = "Scriptable Objects/WaveDataScriptableObject")]
public class SO_WaveData
{
    public SingleWaveData[] singleWaveData;
    public float waveWaitDuration = 1;
    public float enemySpawnWaitDuration = 1;

}

[SerializeField]
public class SingleWaveData
{
    public int enemyAmount = 10;
    public WaveEnemy enemyPrefab;

    public Vector3 GetPaternPos(int i)
    {

    }
}

public class WavePaterns
{

}
