using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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
    private GameObject continueButton;

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
        continueButton = pausePanel.transform.Find("Continue Button").gameObject; 
    }

    //  The OnEnable and OnDisable methods are reqired for the InputActionAsset to work.
    private void OnEnable()
    {
        primaryInputs.Enable();
    }

    private void OnDisable()
    {
        pauseInputAction.performed -= context => pauseGame();
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
            EventSystem.current.SetSelectedGameObject(continueButton);
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
