using UnityEngine;

public class WaveEnemy : MonoBehaviour
{
    public void Init(Vector3 spawnPos)
    {
        transform.position = spawnPos;
    }
}
