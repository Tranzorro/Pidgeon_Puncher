using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AudioManager : MonoBehaviour {

   /* public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public VolumeSettings gameSettings;
    public AudioSource Master;

    void Start()
    {
        gameSettings = new VolumeSettings();

        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });

    }

    public void OnMusicVolumeChange()
    {
        Master.volume = gameSettings.musicLvl = musicVolumeSlider.value;
    }
    public void OnSfxVolumeChange()
    {
        Master.volume = gameSettings.sfxLvl = sfxVolumeSlider.value;
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<VolumeSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        musicVolumeSlider.value = gameSettings.musicLvl;
        sfxVolumeSlider.value = gameSettings.sfxLvl;
    }

    */
}
