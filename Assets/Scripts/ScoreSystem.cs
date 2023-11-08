using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static float score;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("Score");
        Debug.Log("Score" + PlayerPrefs.GetFloat("Score"));
    }
    
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + PlayerPrefs.GetFloat("Score");       
    }
    
}
