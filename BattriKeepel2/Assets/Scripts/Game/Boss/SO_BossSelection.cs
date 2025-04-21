using UnityEngine;

[CreateAssetMenu(fileName = "BossSelectionInfos", menuName = "Boss/BossSelectionInfos")]
public class SO_BossSelectionInfos : SO_UIData
{
    public UIBossSelectionInfo bossSelectionInfoPrefab;
    public Sprite bossMainVisual;

    public override UIDataResult Init(Transform spawnParentTr)
    {
        UIBossSelectionInfo selectionInfo = Object.Instantiate(bossSelectionInfoPrefab, spawnParentTr);
        selectionInfo.Init(this);
        return new(selectionInfo.gameObject);
    }
}
