using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//This is class used to map the time in beats in a file by pressing SPACE during play mode.
//It should not be part of the final release.
public class Mapper : MonoBehaviour
{
    // output file that will contain the beats mapped by pressing SPACE during play mode
    public string outBeatsFileLocation;
    // The file that contains the original note numbers from MIDI keys.
    public string rawNoteColumnPositionFileLocation;
    // The output file that will contain the column position for our game.
    public string outRemapedNotesColumnPositionFileLocation;

    string beatsData = "";
    int i = 0;

    // Start is called before the first frame update
    void Awake()
    {
        //Uncomment this line if you want to remap the notes position
        //from MIDI keys to the game columns indicators position
        //RemapNotesColumnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        // Sampling beats
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(i + " : " + Conductor.instance.songPositionInBeats);
            i++;
            beatsData += Conductor.instance.songPositionInBeats + "\n";

            // Uncomment this line if you want to generate a file with the sampled beats
            // WriteFile(Application.dataPath + outBeatsFileLocation, beatsData);
        }
    }

    void RemapNotesColumnPosition()
    {
        // We read the file's content
        string[] lines = File.ReadAllLines(rawNoteColumnPositionFileLocation);
        List<int> loadedNoteNumbers = new List<int>();

        foreach (string line in lines)
        {
            loadedNoteNumbers.Add(int.Parse(line));
        }

        // We only use 4 columns in the game from 0 to 3;
        // To illustrate: Values from 50 to 60 refers to the note number in a MIDI keyboard
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

        WriteFile(Application.dataPath + outRemapedNotesColumnPositionFileLocation, lines);
    }

    void WriteFile(string path, string content)
    {
        File.WriteAllText(path, content);
    }

    void WriteFile(string path, string[] textInLines)
    {
        File.WriteAllLines(path, textInLines);
    }
}
