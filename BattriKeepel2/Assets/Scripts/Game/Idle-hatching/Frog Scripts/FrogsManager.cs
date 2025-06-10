using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class FrogsManager : MonoBehaviour
{
    [SerializeField] private List<Frog> frogList = new();
    [SerializeField] private SO_EggRarityRate SO_EggRarityRate;

    [SerializeField] private TMP_InputField frogNameInputField;

    [SerializeField] private SO_FrogLevelData commonFrogLevelData;
    [SerializeField] private SO_FrogLevelData uncommonFrogLevelData;
    [SerializeField] private SO_FrogLevelData rareFrogLevelData;
    [SerializeField] private SO_FrogLevelData epicFrogLevelData;
    [SerializeField] private SO_FrogLevelData keepelFrogLevelData;

    [SerializeField] private Frog frogPrefab;

    private string frogName;
    public void CreateNewFrog()
    {
        EN_FrogRarity rarity = ProcessFrogRarity();
        frogName = frogNameInputField.text.ToString();
        Frog frog = Instantiate(frogPrefab);
        frog.Init(frogName, EN_FrogColors.RED, rarity, QueryRarityLevel(rarity));
        Log.Success("WOW, FROG GENERATED ! :)");
        
        frogList.Add(frog);
    }

    public void GiveEXPToFrog() //M�thode test du syst�me d'EXP, � commenter � un moment
    {
        Frog frog = frogList[0];
        frog.AddExpAmount(EN_FrogLevels.RUN, 60);
    }

    private EN_FrogRarity ProcessFrogRarity()
    {
        int totalRate = SO_EggRarityRate.TotalRate();

        int number = Random.Range(1, totalRate+1); //C'est pour avoir sur 100 les neuils

        EN_FrogRarity rarity = SO_EggRarityRate.CompareNumberToRate(number);
        Log.Info( $"Number generated and rarity : {number}, {rarity.ToString()}");

        return rarity;
    }

    private SO_FrogLevelData QueryRarityLevel(EN_FrogRarity rarity)
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
