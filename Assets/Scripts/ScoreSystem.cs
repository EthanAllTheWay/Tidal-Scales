using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierTxt;
    public GameObject gameOverPanel;
    [SerializeField] private float baseScore = 100; //Editable base Score 
    public float showedScore;
    private float accuracy;
    public float multiplier = 1; //When you don't fail a fish capture, you get a bonus
    private float totalScore; // The Final score 
    [SerializeField] private TextMeshProUGUI gameOverScore;
    [SerializeField] private AudioSource audio;
    [SerializeField] private Conductor conductor; // We need data from this to make the perfect detection
    private Fish fish;           // We need data from this to make the perfect detection
    // Start is called before the first frame update
    void Start()
    {
        //We clear scores
        PlayerPrefs.DeleteKey("showScore");
        PlayerPrefs.DeleteKey("TotalScore");
        StartCoroutine(FinishPanel()); //When the sccene starts, we get a timer to show the end panel
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + totalScore;
        multiplierTxt.text = "X" + multiplier;
    }

    private IEnumerator FinishPanel()
    {
        yield return new WaitForSeconds(audio.clip.length - 3.5f); //Here we wait to call the finish panel
        PlayerPrefs.SetFloat("TotalScore", totalScore);
        gameOverPanel.SetActive(true);
        scoreText.enabled = false;
        multiplierTxt.enabled = false;
        gameOverScore.text = "Your final score: " + PlayerPrefs.GetFloat("TotalScore");
    }

    public void addScore(float targetBeat)
    {
        //If the diference between the song beat and the fish beat is smaller than 3 miliseconds, we got a perfect that multiplies the score x1.5
        if (Mathf.Abs(conductor.songPositionInBeats - targetBeat) < 0.3f)
        {
            accuracy = 1.5f;
            Debug.Log("Perfect! " + Mathf.Abs(conductor.songPositionInBeats - targetBeat));
        }
        else
        {
            accuracy = 1;
            Debug.Log("Good" + Mathf.Abs(conductor.songPositionInBeats - targetBeat));
        }
        //showed score is to save the value of each note, and can be used later to show user each one
        showedScore = (baseScore * accuracy) * multiplier;
        if (multiplier < 5) { multiplier += 1; } //x5 is the max multiplier
        totalScore = totalScore + showedScore;
        Debug.Log(conductor.songPositionInBeats);
        Debug.Log(targetBeat);
    }

}