using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    static BGM AU;
    public AudioClip Oriaudio;
    public AudioClip Bossaudio;
    private void Awake()
    {
        if(AU != null)
            Destroy(gameObject);
        AU = this;
        DontDestroyOnLoad(gameObject);
    }
    public void setBossAudio()
    {
        AudioSource ass = GameObject.FindGameObjectWithTag("bgm").GetComponent<AudioSource>();
        ass.Stop();
        ass.clip = Bossaudio;
        ass.Play();
    }
    public void setOriAudio()
    {
        AudioSource ass = GameObject.FindGameObjectWithTag("bgm").GetComponent<AudioSource>();  
        ass.Stop();
        ass.clip = Oriaudio;
        ass.Play();
    }
}
