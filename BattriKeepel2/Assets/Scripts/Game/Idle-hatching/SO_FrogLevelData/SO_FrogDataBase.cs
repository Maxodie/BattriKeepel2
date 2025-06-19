using UnityEngine;

[CreateAssetMenu(fileName = "FrogDataBase", menuName = "Frog/FrogDataBase")]
public class SO_FrogDataBase : ScriptableObject
{
    public SO_EggRarityRate SO_EggRarityRate;

    public SO_FrogLevelData commonFrogLevelData;
    public SO_FrogLevelData uncommonFrogLevelData;
    public SO_FrogLevelData rareFrogLevelData;
    public SO_FrogLevelData epicFrogLevelData;
    public SO_FrogLevelData keepelFrogLevelData;

    public Sprite commonFrogVisual;
    public Sprite uncommonFrogVisual;
    public Sprite rareFrogVisual;
    public Sprite epicFrogVisual;
    public Sprite keepelFrogVisual;

    public Material commonFrogMaterial;
    public Material uncommonFrogMaterial;
    public Material rareFrogMaterial;
    public Material epicFrogMaterial;
    public Material keepelFrogMaterial;
}
