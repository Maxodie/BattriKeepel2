using Inputs;
using System.Collections.Generic;
using UnityEngine;

public class FrogGenerator : IGameEntity
{
    static FrogGenerator s_instance;
    SO_FrogDataBase m_frogDataBase;
    List<FrogDynamicData> frogDatas;

    public FrogGenerator()
    {
        m_frogDataBase = Resources.Load<SO_FrogDataBase>("ScriptableObjects/Frogs/FrogDataBase");
        LoadFrog();
    }

    public static FrogGenerator Get()
    {
        if(s_instance == null)
        {
            s_instance = new();
        }

        return s_instance;
    }

    public SO_FrogDataBase GetDataBase()
    {
        return m_frogDataBase;
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
public class FrogsManager : GameManager
{
    [SerializeField] private List<Frog> frogList = new();

    [SerializeField] private Frog frogPrefab;

    [SerializeField] private InputManager inputManager;

    private string frogName;
    FrogFarm m_frogFarm;

    [Header("Frog")]
    [SerializeField] SO_FrogFarmUIData m_frogFarmUIData;
    [SerializeField] Transform canvasContentTransform;

    FrogBuild m_flyBuild;
    [SerializeField] FrogBuildGraphics runGraphicsPrefab;
    [SerializeField] Transform m_runBuildTr;
    FrogBuild m_runBuild;
    [SerializeField] FrogBuildGraphics flyGraphicsPrefab;
    [SerializeField] Transform m_flyBuildTr;
    FrogBuild m_swimBuild;
    [SerializeField] FrogBuildGraphics swimGraphicsPrefab;
    [SerializeField] Transform m_swimBuildTr;

    protected override void OnUIManagerCreated()
    {

    }

    protected override void Awake()
    {
        base.Awake();

        m_flyBuildTr.position = GraphicsManager.Get().GetCameraLocation((int)SpawnDir.North) - new Vector2(0, 2);
        m_runBuildTr.position = GraphicsManager.Get().GetCameraLocation((int)SpawnDir.West | (int)SpawnDir.North) - Vector2.one;
        m_swimBuildTr.position = GraphicsManager.Get().GetCameraLocation((int)SpawnDir.South) + new Vector2(1, 2);

        List<FrogDynamicData> frogDatas = FrogGenerator.Get().LoadFrog();

        UIDataResult result = UIManager.GenerateUIData(m_frogFarmUIData, canvasContentTransform);
        m_frogFarm = new((FrogFarmUIMenu)result.Menu, FrogGenerator.Get().GetDataBase().startFrogFarmXpThreashold);

        m_flyBuild = new(EN_BuildType.FLY_BUILD, flyGraphicsPrefab, m_frogFarm, m_flyBuildTr);
        m_runBuild = new(EN_BuildType.RUN_BUILD, runGraphicsPrefab, m_frogFarm, m_runBuildTr);
        m_swimBuild = new(EN_BuildType.SWIM_BUILD, swimGraphicsPrefab, m_frogFarm, m_swimBuildTr);


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

    protected override void Update()
    {
        base.Update();

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

        m_runBuild.Update();
        m_flyBuild.Update();
        m_swimBuild.Update();
    }
}
