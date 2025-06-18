using UnityEngine;

public class BossNuisance
{
    SO_BossScriptableObject m_data;

    DialogComponent m_dialogComponent;

    Awaitable nuisanceLoop;

    bool m_isActive = true;

    public BossNuisance(SO_BossScriptableObject m_data)
    {
        if(m_data.dialogData)
        {
            m_dialogComponent = new DialogComponent();
            m_dialogComponent.StartDialog(m_data.dialogData);
        }

        nuisanceLoop = NuisanceLoop();
    }

    public void OnTakeDammage()
    {

    }

    public void OnBossSetActive(bool state)
    {
        m_isActive = state;

        if(state)
        {
            nuisanceLoop = NuisanceLoop();
        }
    }

    void DialogNuisance()
    {
        m_dialogComponent = new DialogComponent();
        m_dialogComponent.StartDialog(m_data.dialogData);
    }

    async Awaitable LightNuisance()
    {
        for(int i = 0; i < 5; i++)
        {
            MobileEffect.SetOnFlashlight(true, 0.2f);
            await Awaitable.WaitForSecondsAsync(0.2f);
        }
    }

    async Awaitable NuisanceLoop()
    {
        while(m_isActive)
        {
            await Awaitable.WaitForSecondsAsync(m_data.nuisanceLoopTime);
            int number = Random.Range(0, 2);
            switch(number)
            {
                case 0:
                    DialogNuisance();
                    break;
                case 1:
                    await LightNuisance();
                    break;

                default:
                    break;
            }
        }
    }
}
