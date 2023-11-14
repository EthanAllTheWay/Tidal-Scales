using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset primaryInputs;
    private InputActionMap InputActionMap;
    private InputAction pauseInputAction;

    [SerializeField]
    private GameObject pausePanel;
    public static bool gamePaused = false;

    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    private void init()
    {
        gamePaused = false;
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
        InputActionMap = primaryInputs.FindActionMap("Gameplay");
        pauseInputAction = InputActionMap.FindAction("Pause");
        pauseInputAction.performed += context => pauseGame();
    }

    //  The OnEnable and OnDisable methods are reqired for the InputActionAsset to work.
    private void OnEnable()
    {
        primaryInputs.Enable();
    }

    private void OnDisable()
    {
        primaryInputs.Disable();
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
}
