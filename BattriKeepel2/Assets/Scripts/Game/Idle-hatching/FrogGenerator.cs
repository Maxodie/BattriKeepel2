using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class FrogGenerator
{
    [SerializeField] private TMP_InputField frogNameInputField;

    [SerializeField] private SO_FrogLevelData commonFrogLevelData;
    [SerializeField] private SO_FrogLevelData uncommonFrogLevelData;
    [SerializeField] private SO_FrogLevelData rareFrogLevelData;
    [SerializeField] private SO_FrogLevelData epicFrogLevelData;
    [SerializeField] private SO_FrogLevelData keepelFrogLevelData;

    private string frogName;

    public Frog GenerateFrog(EN_FrogRarity rarity)
    {
        frogName = frogNameInputField.text.ToString();
        Frog frog = new Frog(frogName, EN_FrogColors.RED, rarity, QueryRaritySO(rarity));

        Log.Succes("WOW, FROG GENERATED ! :)");
        return frog;
    }

    private SO_FrogLevelData QueryRaritySO(EN_FrogRarity rarity)
    {
        switch (rarity)
        {
            case EN_FrogRarity.COMMON:
                return commonFrogLevelData;
            case EN_FrogRarity.UNCOMMUN:
                return uncommonFrogLevelData;
            case EN_FrogRarity.RARE:
                return rareFrogLevelData;
            case EN_FrogRarity.EPIC:
                return epicFrogLevelData;
            case EN_FrogRarity.KEEPEL:
                return keepelFrogLevelData;
            default:
                Log.Error("Rarity not found, bip boup");
                break;
        }
        return null;
    }
}
