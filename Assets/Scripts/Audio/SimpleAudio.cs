using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAudio : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip HitClip;
    public AudioClip DieClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnHit()
    {
        audioSource.clip = HitClip;
        audioSource.Play();
    }
}
