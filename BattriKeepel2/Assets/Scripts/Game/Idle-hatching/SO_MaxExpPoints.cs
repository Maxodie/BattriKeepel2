using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_MaxExpPoints", menuName = "Scriptable Objects/SO_MaxExpPoints")]
public class SO_MaxExpPoints : ScriptableObject
{
    public List<int> EXPPointsToGainLevel = new List<int>();
}
