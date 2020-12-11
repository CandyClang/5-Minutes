using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    public void Play() {
        if (audioSource.clip != null)
            if(!audioSource.isPlaying)
                audioSource.Play();
    }

    public void Stop() {
        if (audioSource.clip != null)
            if (audioSource.isPlaying)
                audioSource.Stop();
    }
}
