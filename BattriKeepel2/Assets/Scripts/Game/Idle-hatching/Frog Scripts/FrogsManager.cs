using Inputs;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrogGenerator : IGameEntity
{
    static FrogGenerator s_instance;
    SO_FrogDataBase m_frogDataBase;
    List<FrogDynamicData> frogDatas;

    public FrogGenerator()
    {
        m_frogDataBase = Resources.Load<SO_FrogDataBase>("ScriptableObjects/Frogs/FrogDataBase");
        Log.Info(m_frogDataBase);
        LoadFrog();
        Log.Info(frogDatas?.Count);
    }

    public static FrogGenerator Get()
    {
        if(s_instance == null)
        {
            s_instance = new();
        }

        return s_instance;
    }

    public List<FrogDynamicData> LoadFrog()
    {
        return frogDatas = SaveSystem.Load<FrogDynamicData>(this);
    }

    public FrogDynamicData GenerateFrog()
    {
        EN_FrogRarity rarity = ProcessFrogRarity();
        FrogDynamicData frogData = new FrogDynamicData(rarity, "CROA-gurl");

        if(frogDatas == null)
        {
            frogDatas = new();
        }
        frogDatas.Add(frogData);

        for(int i = 0; i < frogDatas.Count; i++)
        {
            SaveSystem.Save<FrogDynamicData>(frogDatas[i], this, i);
        }

        return frogData;
    }

    public EN_FrogRarity ProcessFrogRarity()
    {
        int totalRate = m_frogDataBase.SO_EggRarityRate.TotalRate();

        int number = Random.Range(1, totalRate+1); //C'est pour avoir sur 100 les neuils

        EN_FrogRarity rarity = m_frogDataBase.SO_EggRarityRate.CompareNumberToRate(number);
        Log.Info( $"Number generated and rarity : {number}, {rarity.ToString()}");

        return rarity;
    }

    public Sprite QueryVisual(EN_FrogRarity rarity)
    {
        switch (rarity)
        {
            case EN_FrogRarity.COMMON:
                return m_frogDataBase.commonFrogVisual;
            case EN_FrogRarity.UNCOMMUN:
                return m_frogDataBase.uncommonFrogVisual;
            case EN_FrogRarity.RARE:
                return m_frogDataBase.rareFrogVisual;
            case EN_FrogRarity.EPIC:
                return m_frogDataBase.epicFrogVisual;
            case EN_FrogRarity.KEEPEL:
                return m_frogDataBase.keepelFrogVisual;
            default:
                Log.Error("Rarity not found, bip boup");
                break;
        }
        return null;
    }

    public Material QueryMaterial(EN_FrogRarity rarity)
    {
        switch (rarity)
        {
            case EN_FrogRarity.COMMON:
                return m_frogDataBase.commonFrogMaterial;
            case EN_FrogRarity.UNCOMMUN:
                return m_frogDataBase.uncommonFrogMaterial;
            case EN_FrogRarity.RARE:
                return m_frogDataBase.rareFrogMaterial;
            case EN_FrogRarity.EPIC:
                return m_frogDataBase.epicFrogMaterial;
            case EN_FrogRarity.KEEPEL:
                return m_frogDataBase.keepelFrogMaterial;
            default:
                Log.Error("Rarity not found, bip boup");
                break;
        }
        return null;
    }

    public SO_FrogLevelData QueryRarityLevel(EN_FrogRarity rarity)
    {
        switch (rarity)
        {
            case EN_FrogRarity.COMMON:
                return m_frogDataBase.commonFrogLevelData;
            case EN_FrogRarity.UNCOMMUN:
                return m_frogDataBase.uncommonFrogLevelData;
            case EN_FrogRarity.RARE:
                return m_frogDataBase.rareFrogLevelData;
            case EN_FrogRarity.EPIC:
                return m_frogDataBase.epicFrogLevelData;
            case EN_FrogRarity.KEEPEL:
                return m_frogDataBase.keepelFrogLevelData;
            default:
                Log.Error("Rarity not found, bip boup");
                break;
        }
        return null;
    }
}

[System.Serializable]
public class FrogsManager : MonoBehaviour
{
    [SerializeField] private List<Frog> frogList = new();

    [SerializeField] private TMP_InputField frogNameInputField;

    [SerializeField] private Frog frogPrefab;

    [SerializeField] private InputManager inputManager;

    private string frogName;

    void Start()
    {
        List<FrogDynamicData> frogDatas = FrogGenerator.Get().LoadFrog();
        if(frogDatas != null)
        {
            foreach(FrogDynamicData frogData in frogDatas)
            {
                CreateNewFrog(frogData);
            }
        }
    }

    public void CreateNewFrog(FrogDynamicData data)
    {
        Frog frog = new Frog(data, inputManager);
        frogList.Add(frog);
    }

    public void Update()
    {
        foreach(Frog frog in frogList)
        {
            frog.Update();
        }

        foreach(Frog frog in frogList)
        {
            if(frog.HandleMovement())
            {
                break;
            }
        }
    }

    public void GiveEXPToFrog() //M�thode test du syst�me d'EXP, � commenter � un moment
    {
        Frog frog = frogList[0];
        frog.AddExpAmount(EN_FrogLevels.RUN, 60);
    }
}
