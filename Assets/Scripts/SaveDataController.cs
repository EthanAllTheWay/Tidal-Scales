using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SaveDataController : MonoBehaviour
{
    public GameObject audioVolume;
    public SettingsMenu startAudioController;
    public string savedDataFile;
    public GameData savedData = new GameData();
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        savedDataFile = Application.dataPath + "/savedPrefs.json";
        //audioVolume = GameObject.FindGameObjectWithTag("VolumeSlider");
        LoadData();
       
    }
    private void Start()
    {
        SavedVolume(savedData.GlobalVolume);
    }

    public void LoadData() 
    {
        if (File.Exists(savedDataFile))
        {
            string content = File.ReadAllText(savedDataFile);
            savedData = JsonUtility.FromJson<GameData>(content);
            Debug.Log("Volume value" + savedData.GlobalVolume);
            audioVolume.GetComponent<Slider>().value = savedData.GlobalVolume;
            
        }
        else {
            Debug.Log("There is no jsons");
        }

    }

    public void SaveData()
    {
        GameData newData = new GameData()
        {
            GlobalVolume = audioVolume.GetComponent<Slider>().value
             
        };
        Debug.Log("audioValue" + audioVolume.GetComponent<Slider>().value);
        string JSONstring = JsonUtility.ToJson(newData);

        File.WriteAllText(savedDataFile, JSONstring);
        Debug.Log("Archivo guardado");
    }

    public void SavedVolume(float loadedVolume)
    {
        //Debug.Log(loadedVolume);
        audioMixer.SetFloat("Volume", loadedVolume);
    }
}
