using System;
using UnityEngine;
using UnityEngine.UI;

public enum SoundTypes
{
    GAMEMUSIC,
    BUTTONCLICK,
    GAMESTART,
    SNAKEDEATH,
    POSITIVEFOOD,
    NEGATIVEFOOD,
    SPEEDUP,
    SHIELD,
    SCOREBOOST
}

[Serializable]
public class Sounds
{
    public SoundTypes soundType;
    public AudioClip audioClip;
}


public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    public AudioSource soundEffect;
    public AudioSource music;
    public Toggle MusicToggle;
    public Toggle SFXToggle;
    public Sounds[] sounds;

    private void Start()
    {
        PlayMusic(SoundTypes.GAMEMUSIC);
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {//mute music with space for testing
        if (Input.GetKeyDown(KeyCode.M))
            music.mute = !music.mute;
        if (Input.GetKeyDown(KeyCode.N))
            soundEffect.mute = !soundEffect.mute;
        MuteSounds();
    }

    private void MuteSounds()
    {
        if (MusicToggle.isOn)
        {
            music.mute = false;
        }
        else
        {
            music.mute = true;
        }
        if (SFXToggle.isOn)
        {
            soundEffect.mute = false;
        }
        else
        {
            soundEffect.mute = true;
        }
    }
    public void PlayMusic(SoundTypes soundType)
    {
        if (music.isPlaying)
            return;

        AudioClip clip = GetAudioClip(soundType);
        if (clip != null)
        {
            music.clip = clip;
            music.Play();
        }
    }
    public void Play(SoundTypes soundType)
    {
        if (soundEffect.isPlaying)
        {
            soundEffect.Stop();
        }

        AudioClip clip = GetAudioClip(soundType);
        if (clip != null)
        {
            soundEffect.PlayOneShot(clip);
        }
    }

    public void PlayContinuous(SoundTypes soundType)
    {
        if (soundEffect.isPlaying)
            return;

        AudioClip clip = GetAudioClip(soundType);
        if (clip != null)
        {
            soundEffect.PlayOneShot(clip);
        }
    }
    public AudioClip GetAudioClip(SoundTypes soundType)
    {
        Sounds sound = Array.Find(sounds, i => i.soundType == soundType);
        if (sound != null)
        {
            return sound.audioClip;
        }
        return null;
    }
}