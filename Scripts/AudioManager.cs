using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    #region Singleton

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    void Start()
    {
        DontDestroyOnLoad(this);

        foreach (Sound s in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();

            source.clip = s.clip;
            source.volume = s.volume;
            source.pitch = s.pitch;
            source.loop = s.loop;

            s.source = source;

            if (s.playOnStart)
                source.Play();
        }
    }

    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        if(s == null)
        {
            Debug.LogError("Sound with name " + soundName + " doesn't exist!");
            return;
        }

        s.source.Play();
    }

}

[Serializable]
public class Sound
{

    public string name;

    public AudioClip clip;

    [HideInInspector]
    public AudioSource source;

    public float volume = 1;
    public float pitch = 1;

    public bool loop = false;
    public bool playOnStart = false;

}