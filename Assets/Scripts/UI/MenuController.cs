using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject startMenu;
    
    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject settingsMenu;

    [SerializeField]
    private GameObject quitConfirmationMenu;

    [SerializeField]
    private GameObject levelSelectionMenu;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    public void QuitFunction()
    {
        Debug.Log("Quit Function Called.");
        Application.Quit();
    }

    public void StartFunction()
    {
        LoadMenu(startMenu, mainMenu);
    }

    public void SettingsFunction()
    {
        LoadMenu(mainMenu, settingsMenu);
    }

    public void LevelSelectionFunction()
    {
        LoadMenu(mainMenu, levelSelectionMenu);
    }

    public void QuitConfirmationFunction()
    {
        LoadMenu(mainMenu, quitConfirmationMenu);
    }

    public void BackFunction()
    {
        if (levelSelectionMenu.activeInHierarchy)
        {
            LoadMenu(levelSelectionMenu, mainMenu);
        }
        else if (settingsMenu.activeInHierarchy)
        {
            LoadMenu(settingsMenu, mainMenu);
        }
        else if (quitConfirmationMenu.activeInHierarchy)
        {
            LoadMenu(quitConfirmationMenu, mainMenu);
        }
    }

    public void LoadMenu(GameObject menuToDeactivate, GameObject menuToActivate)
    { 
        menuToDeactivate.SetActive(false);
        menuToActivate.SetActive(true);
    }

    private void init()
    {
        startMenu.SetActive(true);
        settingsMenu.SetActive(false);
        quitConfirmationMenu.SetActive(false);
        mainMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
    }
}
