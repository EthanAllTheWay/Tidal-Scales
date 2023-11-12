using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class Conductor : MonoBehaviour
{
    public static Conductor instance;

    [Header("Files location")]
    public string notesDataFile;

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

    // This variable is used to offset the AudioSettings.dspTime variable when the game pauses.
    public float dspTimeOffset = 0f;

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

        // TODO: Change this back to 0. This variable is used to start the level at a certain point.
        SetStartTime(55); // 45
        musicSource.Play();
    }

    private void OnDestroy()
    {
        notes.Clear();
    }

    private void FixedUpdate()
    {
        // We update our variables
        songPosition = (float)AudioSettings.dspTime - dspSongTime - dspTimeOffset;
        songPositionInBeats = songPosition / crotchet;
        Debug.Log("Song Position: " + songPosition + " In beats: " + songPositionInBeats);

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

    // Read data from file.
    private void LoadNotesData()
    {
        string[] lines = File.ReadAllLines(Application.dataPath + "/" + notesDataFile);
        float beat;
        int pos;

        List<string> LineElementsList;
        foreach (string line in lines)
        {
            LineElementsList = line.Split(',').ToList();
            beat = float.Parse(LineElementsList[0]);
            pos = int.Parse(LineElementsList[1]);
            notes.Add(new Note(beat, pos));
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

    public AudioSource GetMusicSource()
    { 
        return musicSource;
    }

    private void SetStartTime(int startTime)
    {
        musicSource.time = startTime;
        dspTimeOffset = -startTime;
    }
}
