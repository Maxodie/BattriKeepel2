using UnityEngine;

public class BossPhase : LevelPhase
{
    Awaitable m_someWait;
    public override void OnStart()
    {
        Log.Info<LevelPhaseLogger>("Start Boss Phase");
        m_someWait = WaitEnd();
    }

    public override void OnEnd()
    {
        Log.Info<LevelPhaseLogger>("End Boss Phase");
    }

    public override void Update()
    {

    }

    async Awaitable WaitEnd()
    {
        await Awaitable.WaitForSecondsAsync(5.0f);
        EndPhase();
    }
}
