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
    public string outNotesDataFile;
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

        //Uncomment this line when you want to combine previous generated files
        //CombineFiles();
    }

    // Update is called once per frame
    void Update()
    {
        // Sampling beats
        if (Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log(i + " : " + Conductor.instance.songPositionInBeats);
            i++;
            beatsData += Conductor.instance.songPositionInBeats + "\n";

            // Uncomment this line if you want to generate a file with the sampled beats
            //WriteFile(Application.dataPath + "/" + outBeatsFileLocation, beatsData);
        }
    }

    //Combines the beatsFile and the columnPositionFile.
    void CombineFiles()
    {
        string[] lines = File.ReadAllLines(Application.dataPath + "/" + outBeatsFileLocation);
        List<float> beats = new List<float>();
        List<int> pos = new List<int>();

        foreach (string line in lines)
        {
            beats.Add(float.Parse(line));
        }

        lines = File.ReadAllLines(Application.dataPath + "/" + outRemapedNotesColumnPositionFileLocation);
        foreach (string line in lines)
        {
            pos.Add(int.Parse(line));
        }

        lines = new string[beats.Count];
        for (int index = 0; index < beats.Count; index++)
        {
            lines[index] = beats[index] + "," + pos[index];
        }

        File.WriteAllLines(Application.dataPath + "/" + outNotesDataFile, lines);
    }

    void RemapNotesColumnPosition()
    {
        // We read the file's content
        string[] lines = File.ReadAllLines(Application.dataPath + "/" + rawNoteColumnPositionFileLocation);
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
                lines[i] = "0";
            else if (loadedNoteNumbers[i] < 65)
                lines[i] = "1";
            else if (loadedNoteNumbers[i] < 68)
                lines[i] = "2";
            else
                lines[i] = "3";
        }

        WriteFile(Application.dataPath + "/" + outRemapedNotesColumnPositionFileLocation, lines);
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
