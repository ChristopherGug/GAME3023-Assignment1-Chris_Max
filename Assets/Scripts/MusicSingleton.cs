using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSingleton : MonoBehaviour
{
    private static MusicSingleton instance;

    public AudioClip[] audioClips;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(this.gameObject);
    }

    public static MusicSingleton GetInstance()
    {
        return instance;
    }

    public void PlayBattleSong()
    {
        int random = Random.Range(1, 4);
        if (audioSource.clip != audioClips[random])
        {
            audioSource.clip = audioClips[random];
            audioSource.Play();
        }
    }

    public void PlayOverworldSong()
    {
        if (audioSource.clip != audioClips[0])
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
    }
}
