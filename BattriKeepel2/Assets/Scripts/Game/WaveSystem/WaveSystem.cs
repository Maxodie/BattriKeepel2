using UnityEngine;
using System.Collections;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] SO_WaveData m_waveData;

    public void StartWave()
    {
        StartCoroutine(StartWaveCoroutine());
    }

    bool CanGoToNextWave()
    {
        return true;
    }

    IEnumerator StartWaveCoroutine()
    {
        yield return new WaitForSeconds(m_waveData.waveWaitDuration);
        foreach(SingleWaveData wave in m_waveData.singleWaveData)
        {
            for(int i = 0; i < wave.enemyAmount; i++)
            {
                WaveEnemy enemy = Instantiate(wave.enemyPrefab);
                enemy.Init();
                yield return new WaitForSeconds(m_waveData.enemySpawnWaitDuration);
            }

            CanGoToNextWave();
        }
    }
}
