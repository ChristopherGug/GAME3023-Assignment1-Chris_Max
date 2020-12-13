using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public static AudioClip playerWalkSound;
    static AudioSource audioSource;
    public static bool moving;

    private int audioCount;

    // Start is called before the first frame update
    void Start()
    {
        playerWalkSound = Resources.Load<AudioClip>("walk");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioCount++;
        if (moving)
        {
            PlaySound();
        }
    }

    public void PlaySound()
    {
        if (audioCount >= 80)
        {
            audioSource.PlayOneShot(playerWalkSound);
            audioCount = 0;
        }
    }
}
