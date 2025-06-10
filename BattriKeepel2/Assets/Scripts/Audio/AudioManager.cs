using UnityEngine;

public class AudioManager
{
    public static SoundInstance CreateSoundInstance(bool playOnAwake, bool loop)
    {
        SoundInstance instance = new SoundInstance(playOnAwake, loop);
        return instance;
    }

    public static void DestroySoundInstance(SoundInstance instance)
    {
        instance.Destroy();
    }
}

public class SoundInstance
{
    AudioSource source;
    public SoundInstance(bool playOnAwake, bool loop)
    {
        GameObject go = new GameObject();
        source = go.AddComponent<AudioSource>();
        source.playOnAwake = playOnAwake;
        source.loop = loop;
    }

    public void Destroy()
    {
        if(source && source.gameObject)
        {
            Object.Destroy(source.gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void Pause()
    {
        source.Pause();
    }

    public void Resume()
    {
        source.Play();
    }
}
