using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioClip[] SoundClips;
    AudioSource _audioSource;


    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play_Sound(int ID)
    {
        _audioSource.PlayOneShot(SoundClips[ID]);
    }
}
