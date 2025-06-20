using UnityEngine;
[CreateAssetMenu(fileName = "FrogFarmUI", menuName = "Frogs/FrogFarmUI")]
public class SO_FrogFarmUIData : SO_UIData
{
    public FrogFarmUIMenu frogFarmPrefab;
    public override UIDataResult Init(Transform spawnParentTr)
    {
        FrogFarmUIMenu frogMenu = Object.Instantiate<FrogFarmUIMenu>(frogFarmPrefab, spawnParentTr);
        return new(frogMenu.gameObject, frogMenu);
    }
}
