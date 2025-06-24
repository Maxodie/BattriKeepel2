using UnityEngine;
using UnityEngine.Events;

public class UIBossMenu : UIMenuBase
{
    [SerializeField] Transform m_bossSelectionInfoContent;
    [SerializeField] Transform m_bossSelectionNavigationContent;

    UnityEvent m_onBossSelectionEnded = new();

    UIBossSelectionInfo[] navigationsPanels;
    UIButton[] buttons;
    int currentMenuID = -1;

    SO_UIBossMenu m_data;
    SoundInstance soundInstance;

    public void Init(SO_UIBossMenu data)
    {
        soundInstance = AudioManager.CreateSoundInstance(false, false);
        m_data = data; navigationsPanels = new UIBossSelectionInfo[data.bossSelectionInfos.Length];
        buttons = new UIButton[data.bossSelectionInfos.Length];
        for(int i=0; i < data.bossSelectionInfos.Length; i++)
        {
            int iCopy = i;
            UIDataResult result = UIManager.GenerateUIData(data.bossSelectionInfos[i], m_bossSelectionInfoContent);
            navigationsPanels[i] = (UIBossSelectionInfo)result.Menu;

            UIButton buttonGo = Object.Instantiate(data.bossSelectionNavigation, m_bossSelectionNavigationContent);
            buttonGo.Button.onClick.AddListener(() => { ChangeNavigationMenu(iCopy, true); });
            buttonGo.Title = data.bossSelectionInfos[i].bossName;
            buttons[i] = buttonGo;
        }

        ChangeNavigationMenu(0, false);
        SetActive(false);
    }

    public void BindMainMenu(UIMainMenu mainMenu)
    {
        for(int i=0; i < navigationsPanels.Length; i++)
        {
            navigationsPanels[i].LinkBossMenu(this);
        }
    }

    public void InvokeSelectionEnded()
    {
        soundInstance.PlaySound(m_data.selectSound);
        m_onBossSelectionEnded.Invoke();
    }

    public void BindOnBossSelectionEnded(UnityAction action)
    {
        m_onBossSelectionEnded.AddListener(action);
    }

    void ChangeNavigationMenu(int id, bool playSound)
    {
        if(playSound)
        {
            soundInstance.PlaySound(m_data.selectSound);
        }

        if(id == currentMenuID)
        {
            return;
        }

        for(int i=0; i < navigationsPanels.Length; i++)
        {
            navigationsPanels[i].SetActive(i == id);
            buttons[i].Button.interactable = i != id;
        }
        Log.Success<MainMenuLogger>("Boss selection menu id : " + id);
    }

    void OnDestroy()
    {
        AudioManager.DestroySoundInstance(soundInstance);
    }
}
