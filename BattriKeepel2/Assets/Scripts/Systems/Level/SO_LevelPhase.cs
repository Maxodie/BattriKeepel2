using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelPhase", menuName = "Level/Phase/LevelPhase")]
public class SO_LevelPhase : ScriptableObject {
    public List<Tuple<GameObject, int>> enemyList;
}

[CreateAssetMenu(fileName = "BossPhase", menuName = "Level/Phase/BossPhase")]
public class SO_BossPhase : SO_LevelPhase {

}

[CreateAssetMenu(fileName = "WavePhase", menuName = "Level/Phase/WavePhase")]
public class SO_WavePhase : SO_LevelPhase {

}
