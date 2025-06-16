using UnityEngine;

[CreateAssetMenu(fileName = "EndGameUI", menuName = "Level/EndGameUI")]
public class SO_EndGameUIData : SO_UIData
{
    public EndGameUI endGameUIPrefab;
    public override UIDataResult Init(Transform spawnParentTr)
    {
        EndGameUI endGame = Instantiate(endGameUIPrefab, spawnParentTr);
        return new(endGame.gameObject, endGame);
    }
}
