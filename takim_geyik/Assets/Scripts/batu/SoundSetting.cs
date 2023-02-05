using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static Slider OptionsValue;
    private GameObject Slider;
    void Start()
    {
        Slider = GameObject.FindWithTag("Slider");
        if (Slider!=null)
        {
            OptionsValue = Slider.GetComponent<Slider>();
        }
        if(OptionsValue==null)
        {
            Debug.LogWarning("Options Ayarı bulunamadı");
            return;
        }

        
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume",volume);
        OptionsValue.value = volume;
        Debug.Log(OptionsValue.value);
    }

}
