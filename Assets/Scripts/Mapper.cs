using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Mapper : MonoBehaviour
{
    public static Mapper Instance;
    string fileLocation;
    string data = "";
    public List<float> loadedBeats = new List<float>();

    int i = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;

        fileLocation = Application.dataPath + "/noteBeats.txt";
        Read();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(i + " : " + Conductor.instance.songPositionInBeats);
            data += Conductor.instance.songPositionInBeats.ToString() + "\n";
            // Write(data);
            i++;
        }
    }

    public void Write(string s)
    {
        File.WriteAllText(fileLocation, s);
    }

    public void Read()
    {
        string[] lines = File.ReadAllLines(fileLocation);
        Debug.Log(lines.Length);
        foreach (string line in lines)
        {
            loadedBeats.Add(float.Parse(line));
//            Debug.Log((float.Parse(line)));
        }
    }

}
