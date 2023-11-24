using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset primaryInputs;
    private InputActionMap InputActionMap;
    private InputAction pauseInputAction;

    [SerializeField]
    private GameObject pausePanel;
    public static bool gamePaused = false;
    public GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverScore;

    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    private void Start()
    {
        StartCoroutine(FinishPanel()); //When the sccene starts, we get a timer to show the end panel
    }

    private void init()
    {
        gamePaused = false;
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
        InputActionMap = primaryInputs.FindActionMap("Gameplay");
        pauseInputAction = InputActionMap.FindAction("Pause");
        pauseInputAction.performed += pauseGame;
    }

    //  The OnEnable and OnDisable methods are reqired for the InputActionAsset to work.
    private void OnEnable()
    {
        primaryInputs.Enable();
    }

    private void OnDisable()
    {
        pauseInputAction.performed -= pauseGame;
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
        gamePaused = false;
        primaryInputs.Disable();
    }

    public void pauseGame(CallbackContext ctx)
    {
        pauseGame();
    }

    public void pauseGame()
    {
        pausePanel.SetActive(!pausePanel.activeInHierarchy);

        if (pausePanel.activeInHierarchy)
        {
            // Pause game and music.
            Time.timeScale = 0;
            gamePaused = true;
            Conductor.instance.GetMusicSource().Pause();
        }
        else
        {
            // Unpause game and music.
            Time.timeScale = 1;
            gamePaused = false;
            Conductor.instance.GetMusicSource().Play();
            Conductor.instance.dspTimeOffset = (float)AudioSettings.dspTime - Conductor.instance.dspSongTime - Conductor.instance.songPosition;
        }
    }

    private IEnumerator FinishPanel()
    {
        yield return new WaitForSeconds(Conductor.instance.GetMusicSource().clip.length); //Here we wait to call the finish panel
        Score.Instance.SaveScore();
        gameOverPanel.SetActive(true);
        gameOverScore.text = "Your final score: " + PlayerPrefs.GetFloat("TotalScore");
        this.enabled = false;
    }
}
