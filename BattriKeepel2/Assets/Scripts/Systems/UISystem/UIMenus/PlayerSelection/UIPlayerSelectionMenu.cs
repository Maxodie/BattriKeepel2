using UnityEngine;
using UnityEngine.Events;

public class UIPlayerSelectionMenu : UIMenuBase
{
    [SerializeField] Transform m_playerSelectionInfoContent;
    [SerializeField] Transform m_playerSelectionNavigationContent;

    UIPlayerSelectionInfo[] navigationsPanels;
    int currentMenuID = -1;

    UnityEvent m_onPlayerSelectionEnded = new();

    SO_UIPlayerSelectionMenu m_data;
    SoundInstance m_soundInstance;

    public void Init(SO_UIPlayerSelectionMenu data)
    {
        m_data = data;
        m_soundInstance = AudioManager.CreateSoundInstance(false, false);

        navigationsPanels = new UIPlayerSelectionInfo[data.playerDatas.Length];
        for(int i=0; i < data.playerDatas.Length; i++)
        {
            int iCopy = i;
            UIPlayerSelectionInfo result = Instantiate(data.playerSelectionInfos, m_playerSelectionInfoContent);
            result.Init(data.playerDatas[i]);
            navigationsPanels[i] = result;

            UIButton buttonGo = Object.Instantiate(data.playerSelectionNavigation, m_playerSelectionNavigationContent);
            buttonGo.Button.onClick.AddListener(() => { ChangeNavigationMenu(iCopy, true); });
            buttonGo.Title = data.playerDatas[i].playerName;
        }

        ChangeNavigationMenu(0, false);
        SetActive(false);
    }

    public void BindMainMenu(UIMainMenu mainMenu)
    {
        for(int i=0; i < navigationsPanels.Length; i++)
        {
            navigationsPanels[i].LinkPlayerSelectionMenu(this);
        }
    }

    public void BindOnPlayerSelectionEnded(UnityAction action)
    {
        m_onPlayerSelectionEnded.AddListener(action);
    }

    public void OnPlayerSelectionEnd()
    {
        m_soundInstance.PlaySound(m_data.selectSound);
        m_onPlayerSelectionEnded.Invoke();
    }

    void ChangeNavigationMenu(int id, bool playSound)
    {
        if(playSound)
        {
            m_soundInstance.PlaySound(m_data.selectSound);
        }

        if(id == currentMenuID)
        {
            return;
        }

        for(int i=0; i < navigationsPanels.Length; i++)
        {
            navigationsPanels[i].SetActive(i == id);
        }
        Log.Success<MainMenuLogger>("Boss selection menu id : " + id);
    }

    void OnDestroy()
    {
        AudioManager.DestroySoundInstance(m_soundInstance);
    }
}
