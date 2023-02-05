using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundSetting : MonoBehaviour
{
    public AudioMixer audioMixer;
    public bool SoundOpen = true;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume",volume);
    }
    public void SoundOpenClose()
    {

    }

}
