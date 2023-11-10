using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//This is class used to map time in beats in a file by pressing SPACE during play mode.
//It should not be part of the final release.
public class Mapper : MonoBehaviour
{
    public static Mapper Instance;
    string beatsFileLocation;
    string noteNumbersFileLocation;
    string data = "";
    public List<float> loadedBeats = new List<float>();
    public List<int> loadedNoteNumbers = new List<int>();

    int i = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;

        beatsFileLocation = Application.dataPath + "/noteBeats.txt";
        noteNumbersFileLocation = Application.dataPath + "/notesNumbers.txt";
        Read();
        RemapNoteNumber();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(i + " : " + Conductor.instance.songPositionInBeats);
        }
    }

    public void Write(string s)
    {
        File.WriteAllText(beatsFileLocation, s);
    }

    public void Read()
    {
        string[] lines = File.ReadAllLines(beatsFileLocation);
        foreach (string line in lines)
        {
            loadedBeats.Add(float.Parse(line));
        }
        lines = File.ReadAllLines(noteNumbersFileLocation);

        foreach (string line in lines)
        {
            loadedNoteNumbers.Add(int.Parse(line));
        }
    }

    void RemapNoteNumber()
    {
        for (int i = 0; i < loadedNoteNumbers.Count; i++)
        {
            if (loadedNoteNumbers[i] < 60)
                loadedNoteNumbers[i] = 0;
            else if (loadedNoteNumbers[i] < 64)
                loadedNoteNumbers[i] = 1;
            else if (loadedNoteNumbers[i] < 67)
                loadedNoteNumbers[i] = 2;
            else
                loadedNoteNumbers[i] = 3;
        }
    }

}
