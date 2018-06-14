using UnityEngine;
using UnityEngine.Audio;


public class VolumeSettings{

    public AudioMixer audioMixer;
    public float musicVolume;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }


}
