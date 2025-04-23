using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public List<AudioClip> musicClips;
    public List<AudioClip> sfxClips;

    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> musicDict = new Dictionary<string, AudioClip>();

    void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load clips into dictionaries
        foreach (var clip in sfxClips)
            if (!sfxDict.ContainsKey(clip.name))
                sfxDict.Add(clip.name, clip);

        foreach (var clip in musicClips)
            if (!musicDict.ContainsKey(clip.name))
                musicDict.Add(clip.name, clip);
    }

    // -------- Public Methods --------

    public void PlaySFX(string name)
    {
        if (sfxDict.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX '{name}' not found.");
        }
    }

    public void PlayMusic(string name, bool loop = true)
    {
        if (musicDict.TryGetValue(name, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music '{name}' not found.");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }
}
