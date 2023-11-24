using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SaveDataController : MonoBehaviour
{
    //calling the slider to get values and searching for the json
    public GameObject audioVolume;
    public SettingsMenu startAudioController;
    public string savedDataFile;
    public GameData savedData = new GameData();
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        //We search for the JSON
        savedDataFile = Application.dataPath + "/savedPrefs.json";
        LoadData();
       
    }
    private void Start()
    {
        //We set the volume to our audiomixer
        SavedVolume(savedData.GlobalVolume);
    }

    public void LoadData() 
    {
        //If we have a JSON, we read the data, and we add this value to Global volume
        if (File.Exists(savedDataFile))
        {
            string content = File.ReadAllText(savedDataFile);
            savedData = JsonUtility.FromJson<GameData>(content);
            Debug.Log("Volume value" + savedData.GlobalVolume);
            audioVolume.GetComponent<Slider>().value = savedData.GlobalVolume;
            
        }
        else {
            //Just a debbuging option
            Debug.Log("There is no jsons");
        }

    }

    public void SaveData()
    {
        //We create new data, we are going to use the variable "Global volume" and we gave it the value of the slider, then, we use JSON functions to write
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
        //We set audioMixer the value
        audioMixer.SetFloat("Volume", loadedVolume);
    }
}
