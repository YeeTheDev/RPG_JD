using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] sfx;
    public AudioSource[] bgm;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    public void PlaySFX(int soundIndex)
    {
        if (soundIndex < sfx.Length) { sfx[soundIndex].Play(); }
    }

    public void PlayBGM(int musicIndex)
    {
        StopMusic();

        if (!bgm[musicIndex].isPlaying)
        {
            if (musicIndex < bgm.Length)
            {
                bgm[musicIndex].Play();
            }
        }
    }

    public void StopMusic()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
}
