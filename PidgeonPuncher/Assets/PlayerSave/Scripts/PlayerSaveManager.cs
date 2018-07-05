using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 *  PlayerSaveManager Component.
 *  Description:
 *   This component will automaticly save, load and reconfigure teh PlayerSave when the game compiles or the game quits.
 *   Without this component in the scene, the settings will be deserilized on recompile and the settings will not save at end of application.
 * 
 *  Usage:
 *   Place one instance of this component on any object in a scene. 
 *  
 */
[AddComponentMenu("Miscellaneous/PlayerSave Manager")]
public class PlayerSaveManager : MonoBehaviour
{
    //This is a default save path. Set it in the inspector for easy configuration.
    public string defaultSave = "{data_path}/saveData";
    
    //The loadedSave file and loadedCrypt method. Stored in these fields during compile.
    public string _loadedSave = "";
    public SaveEncryptionMethod _loadedCrypt;

    void Awake() {
        //Set the default file
        PlayerSave.SetSaveFile(defaultSave);

        //Set the loaded methods
        _loadedSave = PlayerSave.GetSaveFile();
        _loadedCrypt = PlayerSave.GetEncryptionMethod();

        //Load the default save
        PlayerSave.Load();
    }
    void OnDisable() { 
         //We have to make sure the data isn't kept in the editor as it gets really confusing.
        if (!Application.isPlaying)
        {
            PlayerSave.DeleteAll();
            return;
        }
        
        //Pull out the settings and save the information before we loose it all during Serialization.
        _loadedCrypt = PlayerSave.GetEncryptionMethod(); 
        _loadedSave = PlayerSave.GetSaveFile(); 
        PlayerSave.Save();
    }
    void OnEnable() { 
        //We have to make sure the data isn't kept in the editor as it gets really confusing.
        if (!Application.isPlaying)
        {
            PlayerSave.DeleteAll();
            return;
        }
        
        //Reload all the settings back into the encryptor and load up the previous save again
        PlayerSave.SetEncryptionMethod(_loadedCrypt);
        PlayerSave.SetSaveFile(_loadedSave);
        PlayerSave.Load();
    }
    
    public void OnApplicationQuit() {
        //Save the data on quit.
        PlayerSave.Save();
    }
}