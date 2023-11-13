using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelGameOver : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("MenuScene");

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        this.gameObject.SetActive(false);
    }
}
