using UnityEngine;
using System.Collections.Generic;

public class AudioManager
{
    static List<SoundInstance> sources = new();
    public static SoundInstance CreateSoundInstance(bool playOnAwake, bool loop)
    {
        SoundInstance instance = new SoundInstance(playOnAwake, loop);
        sources.Add(instance);
        return instance;
    }

    public static void DestroySoundInstance(SoundInstance instance)
    {
        sources.Remove(instance);
    }
}

public class SoundInstance
{
    AudioSource source;
    public SoundInstance(bool playOnAwake, bool loop)
    {
        source = Object.Instantiate(new AudioSource());
        source.playOnAwake = playOnAwake;
        source.loop = loop;
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
