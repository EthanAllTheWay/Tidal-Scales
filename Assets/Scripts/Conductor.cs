using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Conductor : MonoBehaviour
{
    //Song beats per minute
    public float songBpm;

    //The number of seconds for each song beat
    //It is the crotchet (I think...)
    public float crotchet;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    AudioSource musicSource;

    //Notes that will be spawned throughout the song.
    public Note[] notes;

    public static Conductor instance;
    private int notesIndex = 0;
    public GameObject notePrefab;

    //It controls how many beats are before the note's beat target.
    //More prespawn beats means that the note will spawn earlier.
    public float prespawnBeats;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        // We initialize our variables and play the music.
        musicSource = GetComponent<AudioSource>();
        crotchet = 60f / songBpm;
        dspSongTime = (float)AudioSettings.dspTime;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // We update our variables
        songPosition = (float)AudioSettings.dspTime - dspSongTime;
        songPositionInBeats = songPosition / crotchet;

        //This checks if it is time to spawn a note
        if (notesIndex < notes.Length && notes[notesIndex].targetBeat < songPositionInBeats + prespawnBeats)
        {
            Instantiate(notePrefab).GetComponent<Fish>().InitializeValues(notes[notesIndex].spawnPoint.position, notes[notesIndex].targetPoint.position, notes[notesIndex].targetBeat);
            //We move to the following note
            notesIndex++;
        }
    }

    [Serializable]
    public enum Row
    {
        Left,
        MiddleLeft,
        MiddleRight,
        Right
    }

    [Serializable]
    public class Note
    {
        public float targetBeat;
        public Transform spawnPoint;
        public Transform targetPoint;
        public Row row;
    }

}
