using System.Collections.Generic;
using UnityEngine;
using caca = System.Nullable;

[System.Serializable]
public class FrogsManager : MonoBehaviour
{
    [SerializeField] private List<Frog> frogList = new();
    [SerializeField] private FrogGenerator m_Generator;
    [SerializeField] private SO_EggRarityRate SO_EggRarityRate;

    [SerializeField] private GameObject frogPrefab;

    public void CreateNewFrog()
    {
        Frog newFrog = m_Generator.GenerateFrog(ProcessFrogRarity());
        AddFrog(newFrog);
    }

    private void AddFrog(Frog frog)
    {
        frogList.Add(frog);
    }

    private EN_FrogRarity ProcessFrogRarity()
    {
        int totalRate = SO_EggRarityRate.TotalRate();

        int number = Random.Range(1, totalRate+1); //C'est pour avoir sur 100 les neuils

        EN_FrogRarity rarity = SO_EggRarityRate.CompareNumberToRate(number);
        Log.Info( $"Number generated and rarity : {number}, {rarity.ToString()}");

        return rarity;
    }

    public void SpawnAllFrogs() 
    {
        FrogGraphics frogGraphics = null;
        GameObject gameObject = null;
        int caca = 1;
        foreach( Frog f in frogList)
        {
            gameObject = Instantiate( frogPrefab );
            frogGraphics = gameObject.GetComponent<FrogGraphics>();
            frogGraphics.SetFrogData(f);
            frogGraphics.ComputeColor();
            gameObject.transform.position = new Vector3(caca * 2, 0);
            caca += 1;
        }
    }
}
