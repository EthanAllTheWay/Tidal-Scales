using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{

    [SerializeField] 
    private AudioSource audioSource;

    public void playSound()
    {
        audioSource.Play();
    }
}
