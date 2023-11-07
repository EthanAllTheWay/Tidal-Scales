using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    private float score;
    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(Conductor.instance.songPositionInBeats);
    }*/
    
    
    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fish") {

            score = PlayerPrefs.GetFloat("Score");
            score += 1;
            PlayerPrefs.SetFloat("Score", score);
            Debug.Log("Score: " + score);
            
        }
    }
   
}
