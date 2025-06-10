using UnityEngine;

public class GameManagerLogger : Logger
{
}

public abstract class GameManager : GameEntityMonoBehaviour
{
    [Header("Music")]
    [SerializeField] AudioClip m_backgroundAudio;
    SoundInstance m_backgroundSoundInstance;

    [SerializeField] SO_GameInstance m_gameInstance;

    protected virtual void Awake()
    {
        GameInstance.StartGameInstance(m_gameInstance);
        OnUIManagerCreated();

        m_backgroundSoundInstance = AudioManager.CreateSoundInstance(true, true);
        m_backgroundSoundInstance.PlaySound(m_backgroundAudio);
    }

    protected virtual void Start() {
    }

    protected virtual void Update()
    {
    }

    protected virtual void FixedUpdate() {

    }

    protected abstract void OnUIManagerCreated();
}
