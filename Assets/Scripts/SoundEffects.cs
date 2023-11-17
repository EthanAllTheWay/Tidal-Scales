using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This class is to store all audio clips and utility method for audio clips.
/// </summary>
public class SoundEffects : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] missClipArray;

    public AudioClip[] catchClipArray;

    public static SoundEffects instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays a selected audio clip once from an array.
    /// </summary>
    /// <param name="audioSource">The audio source class to play the clip.</param>
    /// <param name="audioClipArray">The array of audio clips to choose from.</param>
    /// <param name="arrayIndex">The index of the array to choose an audio clip.</param>
    public static void PlayAudioClip(AudioSource audioSource, AudioClip[] audioClipArray, int arrayIndex)
    {
        if (audioSource != null 
            && audioClipArray != null 
            && Enumerable.Range(0, audioClipArray.Length).Contains(arrayIndex))
        {
            audioSource.PlayOneShot(audioClipArray[arrayIndex]);
        }
    }

    /// <summary>
    /// Plays a single audio clip once at random from a list of audio clips.
    /// </summary>
    /// <param name="audioSource">The audio source class to play the clip.</param>
    /// <param name="audioClipArray">The array of audio clips to choose from.</param>
    public static void PlayAudioClipAtRandom(AudioSource audioSource, AudioClip[] audioClipArray)
    {
        if (audioSource != null && audioClipArray != null)
        {
            audioSource.PlayOneShot(audioClipArray[Random.Range(0, audioClipArray.Length)]);
        }
    }


}
