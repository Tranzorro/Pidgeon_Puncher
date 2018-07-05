using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{

    public AudioMixer masterMixer;

    [Header("Sound Archives")]
    public AudioClip[] poop;

    public void SetSFXlvl(float sfxLvl)
    {
        // the sfxVol is the name of the exposed parameter, and the sfxLvl is the name of the value
        masterMixer.SetFloat("sfxVol", sfxLvl);
        PlayerSave.SetFloat("sfxVol", sfxLvl);
    }
    public void SetMusiclvl(float musicLvl)
    {
        masterMixer.SetFloat("musicVol", musicLvl);
        PlayerSave.SetFloat("musicVol", musicLvl);
    }



}
