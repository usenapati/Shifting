using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip past;
    public AudioClip future;
    public AudioManager pastManager;
    public AudioManager futureManager;

    private AudioSource src;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        futureMusic();
    }

    public void pastMusic()
    {
        src.clip = past;
        src.Play();
    }

    public void futureMusic()
    {
        src.clip = future;
        src.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
