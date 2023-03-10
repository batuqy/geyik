using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
           s.source= gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.mute = s.mute;
           // s.source.outputAudioMixerGroup = s.audioMixer;
        }
    }

    public void Play(string name)
    {
     Sound s=Array.Find(sounds, sound => sound.name == name);
        if (s==null)
        {
            Debug.LogWarning("Audio Manager HATASI: "+name+" Kaynağı bulunamadı" );
            return;
        }
     s.source.Play();
    }
    void Start()
    {
        Play("MainMenuMusic");
    }
    
}
