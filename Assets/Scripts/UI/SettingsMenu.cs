using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
   public void AudioVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    

}
