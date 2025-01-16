using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musics;
    [Space]
    public Sound[] sounds;
    private int currentBGMusic = 0;

    int poolSize = 5;
    Dictionary<Sound, Stack<AudioSource>> soundPoolDic;
    private void Awake()
    {
        soundPoolDic = new Dictionary<Sound, Stack<AudioSource>>();
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

        foreach (Sound s in musics)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    public void MenuMusic()
    {
        StopMusic("IngameBG");
        PlayMusic("Menu");
    }

    public void IngameMusic()
    {
        StopMusic("Menu");
        PlayMusic("IngameBG");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musics, music => music.name == name);
        if (s == null)
            return;
        s.source.mute = !GameConfig.Instance.IsMusicOn;
        //s.source.Play();
        if (!s.source.isPlaying)
        {
            s.source.Play();
        }
    }

    public void PlayLoopingSFX(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.mute = !GameConfig.Instance.IsSFXOn;
        if (s.source.isPlaying) return;
        else
        {
            s.source.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.mute = !GameConfig.Instance.IsSFXOn;

        s.source.Play();
    }
    public void PlaySFXOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.mute = !GameConfig.Instance.IsSFXOn;


        s.source.PlayOneShot(s.clip);
    }

    public void StopMusic(string name)
    {
        Sound s = Array.Find(musics, music => music.name == name);
        if (s == null)
            s.source.mute = !s.source.mute;

        if (s.source.isPlaying)
            s.source.Stop();
    }

    public void StopSFX(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        if (s.source.isPlaying)
            s.source.Stop();
    }

    public void ToggleMusic()
    {
        foreach (Sound s in musics)
        {
            s.source.mute = !s.source.mute;
        }
        GameEvents.ToggleMusic();
    }

    public void ToggleSFX()
    {
        foreach (Sound s in sounds)
        {
            s.source.mute = !s.source.mute;
        }
    }
}
