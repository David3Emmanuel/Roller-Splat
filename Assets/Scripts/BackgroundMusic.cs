using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    private AudioSource audioSource;
    public bool shouldReload;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Awake()
    {
        if (shouldReload)
        {
            if (instance) Destroy(instance.gameObject);
            instance = null;
        }
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

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
