using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Mapper : MonoBehaviour
{
    string fileLocation;
    string data = "";

    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        fileLocation = Application.dataPath + "/noteBeats";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(i + " : " + Conductor.instance.songPositionInBeats);
            data += Conductor.instance.songPositionInBeats.ToString() + "\n";
            Write(data);
            i++;
        }
    }

    public void Write(string s)
    {
        File.WriteAllText(fileLocation, s);
    }

}
