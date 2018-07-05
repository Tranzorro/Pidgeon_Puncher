using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class DemoSaveSelector : MonoBehaviour {
    //The location we expect to see the game saved data in
    public string saveDirectory = "SavedGames/";

    //The current file we have loaded
    public string currentSaveFile = "";

    //Current saved games that we have loaded
    public SavedGame[] savedGames = new SavedGame[0];

    //All the rigidbodies we can modify
    public Rigidbody[] rigidbodies = new Rigidbody[0];

    //The force to apply to the rigidbodies
    public float mouseForce = 100F;

    #region MonoBehaviour Calls
    void Start()
    {
        //Load up all the rigidbodies
        rigidbodies = GameObject.FindObjectsOfType<Rigidbody>() as Rigidbody[];

        //Load up all the saved data intially.
        RefreshSaveData(true);

        //Set the default save file
        currentSaveFile = "ASaveFile.sav";
    }
    void OnGUI()
    {
        //If this button is pressed, we will save the game to the new path and name
        if (GUI.Button(new Rect(170, 10, 150, 25), "Create New Save")) SaveGame(Application.dataPath + "/" + saveDirectory + currentSaveFile, currentSaveFile);
        
        //the currentSaveFile field.
        currentSaveFile = GUI.TextField(new Rect(10, 10, 150, 25), currentSaveFile);

        //Itterate through the saved game list
        for (int i = 0; i < savedGames.Length; i++)
        {
            //Get the saved game
            SavedGame game = savedGames[i];

            //Draw the background box
            GUI.Box(new Rect(10, 45 + (i*60), 310, 50), game.name);

            //Load the file at the game's path when Load Game is pressed
            if (GUI.Button(new Rect(25, 45 + (i * 60) + 20, 100, 25), "Load Game"))
                LoadGame(game.path);

            //Save the game with its name to the appropriate path when Save Game is pressed
            if (GUI.Button(new Rect(140, 45 + (i * 60) + 20, 100, 25), "Save Game"))            
                SaveGame(game.path, game.name);
            

            if (GUI.Button(new Rect(255, 45 + (i * 60) + 20, 50, 25), "Delete"))
            {
                //The delete button was pressed, we need to delete the file and then fresh everything.
                if (File.Exists(game.path)) File.Delete(game.path);
                
                //Reload the list
                RefreshSaveData(true);
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //Get the ray from the current mouse positino
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Try to hit a rigidbody and then apply a force to it.
            if (Physics.Raycast(ray, out hit) && hit.rigidbody && !hit.rigidbody.isKinematic)
                hit.rigidbody.AddForceAtPosition(ray.direction * mouseForce, hit.point, ForceMode.Acceleration);
        }

        if (Input.GetMouseButtonDown(1))
        {
            //Get the ray from the current mouse positino
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Try to hit a rigidbody and then give it a random color
            if (Physics.Raycast(ray, out hit) && hit.rigidbody)
            {
                //Pick some random r,g,b values
                float r = Random.Range(0F, 1F);
                float g = Random.Range(0F, 1F);
                float b = Random.Range(0F, 1F);

                //Apply the renderer colour with this.
                hit.rigidbody.GetComponent<Renderer>().material.color = new Color(r, g, b);
            }
        }
    }
    #endregion

    #region Save Data Manipulation
    public void RefreshSaveData(bool firstRun = false)
    {
        //Store the current settings, so we don't loose them on load
        string sav = PlayerSave.GetSaveFile();
        if (!firstRun) PlayerSave.Save();

        //Get all the save files at the path
        string[] saveFiles = GetSaveFiles(Application.dataPath + "/" + saveDirectory);
        List<SavedGame> games = new List<SavedGame>();

        //Itterate through all of them, getting the save data
        for (int i = 0; i < saveFiles.Length; i++)
        {
            //Load up the save
            PlayerSave.SetSaveFile(saveFiles[i]);
            PlayerSave.Load();

            //fetch the data about the save
            string name = PlayerSave.GetString("save_name", "A Player Save");
            System.DateTime time = PlayerSave.GetDateTime("save_time", System.DateTime.Now);
            string path = PlayerSave.GetSaveFile();

            //Store the save as a new SavedGame data
            games.Add(new SavedGame(name, path, time));
        }

        //Clear the array for readiness of the goods
        savedGames = null;

        //M'aiq has the goods for you.
        savedGames = games.OrderBy(o=>o.time).Reverse().ToArray<SavedGame>() as SavedGame[];

        //Clear all the temporarly loaded settings
        PlayerSave.DeleteAll();

        //Reload the previous settings
        PlayerSave.SetSaveFile(sav);
        if (!firstRun) PlayerSave.Load();
    }
   
    public void LoadGame(string path)
    {
        //Set the current save to the path we retrived        
        PlayerSave.SetSaveFile(path);

        //Load the save as it would still contain the previous one.
        PlayerSave.Load();

        //Load all the game data
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            //Apply the position, rotation and scale
            PlayerSave.GetTransform("rigidbody_" + i + "_pos", rigidbodies[i].transform);

            //Apply the colour
            rigidbodies[i].GetComponent<Renderer>().material.color = PlayerSave.GetColor("rigidbody_" + i + "_color", Color.white);

            //Apply the velocity
            rigidbodies[i].velocity = PlayerSave.GetVector3("rigidbody_" + i + "_velocity", Vector3.zero);

            //Apply the angular velocity
            rigidbodies[i].angularVelocity = PlayerSave.GetVector3("rigidbody_" + i + "_angularVelocity", Vector3.zero);
        }
    }
    public void SaveGame(string path, string name)
    {
        //Save all the game data
        PlayerSave.SetString("save_name", name);
        PlayerSave.SetDateTime("save_time", System.DateTime.Now);

        //Save the rigidbodies
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            //Save the position, rotation and scale
            PlayerSave.SetTransform("rigidbody_" + i + "_pos", rigidbodies[i].transform);

            //Save the colour
            PlayerSave.SetColor("rigidbody_" + i + "_color", rigidbodies[i].GetComponent<Renderer>().material.color);

            //Save the velocity
            PlayerSave.SetVector3("rigidbody_" + i + "_velocity", rigidbodies[i].velocity);

            //Save the angular velocity
            PlayerSave.SetVector3("rigidbody_" + i + "_angularVelocity", rigidbodies[i].angularVelocity);
        }


        //PlayerSave does not automaticly save at all. 
        //The PlayerSaveManager saves on recompile and ApplicationQuit, but only if its in the current scene
        PlayerSave.SetSaveFile(path);
        PlayerSave.Save();
        
        //Reload all the data
        RefreshSaveData();
    }
    #endregion

    public string[] GetSaveFiles(string directory)
    {
        //The directory does not exist, so we obviously have no saves.
        if (!Directory.Exists(directory)) return new string[0];

        //Return a Directory search for .sav files in set directory
        return Directory.GetFiles(directory, "*.sav");
    }

}

[System.Serializable]
public class SavedGame
{
    //The name of the save
    public string name;

    //The path of the save
    public string path;

    //The time of the save
    public System.DateTime time;

    //Handy Constructors are handy ;)
    public SavedGame(string name, string path, System.DateTime time)
    {
        this.name = name;
        this.path = path;
        this.time = time;
    }
}