using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class Score : MonoBehaviour
{
    public static Score Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierTxt;
    [SerializeField] private float baseScore = 100; //Editable base Score 
    public float showedScore;
    private float accuracy;
    private string accuracyTxt;
    public float multiplier = 1; //When you don't fail a fish capture, you get a bonus
    private float totalScore; // The Final score 
    public GameObject FloatingScore;
    [SerializeField] private AudioSource audio;
    [SerializeField] private Conductor conductor; // We need data from this to make the perfect detection
    private Fish fish;           // We need data from this to make the perfect detection
    
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //We clear scores
        PlayerPrefs.DeleteKey("showScore");
        PlayerPrefs.DeleteKey("TotalScore");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + totalScore;
        multiplierTxt.text = "X" + multiplier;
    }

    public void addScore(float targetBeat)
    {
        //If the diference between the song beat and the fish beat is smaller than 2 miliseconds, we got a perfect that multiplies the score x1.5
        if (Mathf.Abs(conductor.songPositionInBeats - targetBeat) < 0.2f)
        {
            accuracy = 1.5f;
            accuracyTxt = "Perfect!";


        }
        else
        {
            accuracy = 1;
            accuracyTxt = "Good!";

        }
        //showed score is to save the value of each note, and can be used later to show user each one
        showedScore = (baseScore * accuracy) * multiplier;
        if (multiplier < 5) { multiplier += 1; } //x5 is the max multiplier
        totalScore = totalScore + showedScore;

    }

    public void ShowFloatingScore(Vector3 v)
    {
        var fs = Instantiate(FloatingScore, v, Quaternion.Euler(0, -180, 0), transform);
        fs.GetComponent<TextMeshPro>().text = accuracyTxt;
    }

    public void ShowMissMessage(Vector3 v)
    {
        var Mmsj = Instantiate(FloatingScore, v, Quaternion.Euler(0, -180, 0), transform);
        Mmsj.GetComponent<TextMeshPro>().text = "Miss!";
    }

    public void SaveScore()
    {
        PlayerPrefs.SetFloat("TotalScore", totalScore);
    }
}