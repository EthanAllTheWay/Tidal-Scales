using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    [SerializeField]private float baseScore = 100; //Editable base Score 
    public float showedScore;
    public float multiplier = 1; //When you don't fail a fish capture, you get a bonus
    private float totalScore; // The Final score 
    [SerializeField] private TextMeshProUGUI gameOverScore;
    [SerializeField] private AudioSource audio; 
    // Start is called before the first frame update
    void Start()
    {
        //We clear scores
        PlayerPrefs.DeleteKey("showScore"); 
        PlayerPrefs.DeleteKey("TotalScore");
        StartCoroutine(FinishPanel()); //When the music starts, we get a timer to show the end panel
    }
    
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "¡Score+  " + PlayerPrefs.GetFloat("showScore")+"!";

    }

    private IEnumerator FinishPanel() {
        //Debug.Log("Audio duration:" + audio.clip.length);
        yield return new WaitForSeconds(audio.clip.length - 3.5f); //Here we wait to call the finish panel
        gameOverPanel.SetActive(true);
        scoreText.enabled = false;
        gameOverScore.text = "Your final score: " + PlayerPrefs.GetFloat("TotalScore");
    }

    public void addScore() {
        showedScore = baseScore * multiplier;
        PlayerPrefs.SetFloat("showScore", showedScore);
       // StartCoroutine(scoreFlash());
        if (multiplier < 5) { multiplier += 1; } //x5 is the max multiplier
        totalScore = totalScore + showedScore;
        PlayerPrefs.SetFloat("TotalScore", totalScore);
        //Debug.Log("Scored Point " + PlayerPrefs.GetFloat("showScore") + " Multiplier is: " + multiplier + " And Total Score is: " + PlayerPrefs.GetFloat("TotalScore"));
       
    }
    /*
    private IEnumerator scoreFlash()
    {
        
        scoreText.enabled = true;
        yield return new WaitForSeconds(1);
        scoreText.enabled = false;

    }
    */
}
