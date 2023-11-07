using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapper : MonoBehaviour
{
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(i+" : "+  Conductor.instance.songPositionInBeats);
            i++;
        }   
    }
}
