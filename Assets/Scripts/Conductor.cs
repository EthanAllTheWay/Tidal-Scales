using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(AudioSource))]
public class Conductor : MonoBehaviour
{
    public static Conductor instance;

    [Header("Files location")]
    public string notesPositionFileLocation;
    public string notesBeatsFileLocation;
    private List<int> notesColumnPosition = new List<int>();
    private List<float> notesBeats = new List<float>();

    [Header("Conductor's control variables")]
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
    //It controls how many beats are before the note's beat target.
    //More prespawn beats means that the note will spawn earlier.
    public float prespawnBeats;

    [Header("Audio components")]
    //an AudioSource attached to this GameObject that will play the music.
    AudioSource musicSource;

    [Header("Notes elements")]
    // They need to contain indicators and spawn points in the following order:
    // Left, Middle left, Middle right and right
    public GameObject notePrefab;
    public Transform[] spawnPoints;
    public Transform[] indicatorPoints;

    // Make private after debugging.
    [Header("Notes spawned during the level")]
    //Notes that will be spawned throughout the song.
    public List<Note> notes;

    private int notesIndex = 0;

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
        LoadNotesData();
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
        if (notesIndex < notes.Count && notes[notesIndex].targetBeat < songPositionInBeats + prespawnBeats)
        {
            Note spawnedNote = notes[notesIndex];
            Instantiate(notePrefab).GetComponent<Fish>().InitializeValues(
                spawnPoints[spawnedNote.column].position,
                indicatorPoints[spawnedNote.column].position,
                spawnedNote.targetBeat, notesIndex + 1);
            //We move to the following note
            notesIndex++;
        }
    }

    // Read data from files.
    private void LoadNotesData()
    {
        // We read values from the files.
        string[] lines = File.ReadAllLines(Application.dataPath + notesBeatsFileLocation);
        foreach (string line in lines)
        {
            notesBeats.Add(float.Parse(line));
        }

        lines = File.ReadAllLines(Application.dataPath + notesPositionFileLocation);
        foreach (string line in lines)
        {
            notesColumnPosition.Add(int.Parse(line));
        }

        // We create our notes and set the values.
        for (int i = 0; i < notesBeats.Count; i++)
        {
            float beat = notesBeats[i];
            int number = notesColumnPosition[i];
            notes.Add(new Note(beat, number));
        }
    }

    //Structure containing data about notes that will be spawned
    [Serializable]
    public class Note
    {
        public Note(float beat, int n)
        {
            targetBeat = beat;
            column = n;
        }

        public float targetBeat;
        public int column;
    }

}
