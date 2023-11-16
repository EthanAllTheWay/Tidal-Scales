using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{

    public AudioSource missSound;

    public AudioSource catchSound;

    public static SoundEffects instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        missSound.volume = 0.5f;
        catchSound.volume = 0.5f;
    }
}
