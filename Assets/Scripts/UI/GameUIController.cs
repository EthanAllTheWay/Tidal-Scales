using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{

    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject gamePanel;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    private void init()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            pausePanel.SetActive(!pausePanel.activeInHierarchy);
        }
    }
}
