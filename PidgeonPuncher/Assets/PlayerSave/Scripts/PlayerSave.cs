using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

public class PlayerSave
{
    //The file encryption key must be kept secure, so it is hard coded so Serilization doesn't have to catch it.
    //Use http://www.passwordsgenerator.net/ for a good strong encryption key.
    //Some encryption methods may require this to be modified to a more suitable length.
    static string encryptKey = "Ev$};-[dc-2$(,(5~#qPyR@Ed/ysCE^!";

    //The Dictionary used to store the settings
    static Dictionary<string, string> playerData = new Dictionary<string, string>();

    //The current save file
    static string currentSave = "{data_path}/saveData";

    //The current encryption methods
    static SaveEncryptionMethod ecnryptMethod = SaveEncryptionMethod.Rijndael;


    /// <summary>
    /// Should we log errors that are caught when loading a save?
    /// </summary>
    public static bool LogLoadFailures = false;

    #region Getters
    /// <summary>
    /// Tries to fetch the float from key. Returns defaultValue if it fails.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static float GetFloat(string key, float defaultValue = 0F)
    {
        //Get the string
        string s_value = GetString(key, "NA");

        //Try to parse the string into a float, then return it
        float r_value;
        if (float.TryParse(s_value, out r_value)) return r_value;

        //If all else fails, return the default value
        return defaultValue;
    }
    
    /// <summary>
    /// Tries to fetch the float array from key. Returns empty array if it fails.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static float[] GetFloats(string key)
    {
        //Get the string and make sure its valud
        string value = GetString(key, "NA");
        if (value == "NA") return new float[0];

        //Convert the string into bytes and prepare the array
        byte[] bytes = Convert.FromBase64String(value);
        float[] values = new float[0];

        //Create a memory stream from the bytes and then a BinaryReader
        using (MemoryStream memory = new MemoryStream(bytes))
        using (BinaryReader br = new BinaryReader(memory))
        {
            //Get the count of the array, then prepare the array
            int count = br.ReadInt32();
            values = new float[count];

            //Itterate the count and get all the values
            for (int i = 0; i < count; i++) values[i] = br.ReadSingle();
        }

        //return the values
        return values;
    }
       
    /// <summary>
    /// Tries to fetch the int from key. Returns defaultValue if it fails.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static int GetInt(string key, int defaultValue = 0) {
      
        //Get the int
        string s_value = GetString(key, "NA");

        //try to parse it and return the parsed value
        int r_value;
        if (int.TryParse(s_value, out r_value)) return r_value;

        //return default value if all else fails
        return defaultValue;
    }
    
    /// <summary>
    /// Tries to fetch the int array from key. Returns empty array if it fails.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static int[] GetInts(string key)
    {
        //For documentation, see GetFloats
        string value = GetString(key, "NA");
        if (value == "NA") return new int[0];

        byte[] bytes = Convert.FromBase64String(value);
        int[] values = new int[0];

        using (MemoryStream memory = new MemoryStream(bytes))        
            using (BinaryReader br = new BinaryReader(memory))
            {
                int count = br.ReadInt32();
                values = new int[count];
                for (int i = 0; i < count; i++) values[i] = br.ReadInt32();
            }

        return values;
    }

    /// <summary>
    /// Tries to fetch the byte array from key. Returns empty array if it fails.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static byte[] GetBytes(string key)
    {
        //Fetch the string and make sure its valid
        string value = GetString(key, "NA");
        if (value == "NA") return new byte[0];

        //Convert the string to bytes
        return Convert.FromBase64String(value);
    }

    /// <summary>
    /// Tries to fetch the Bool from key. Returns defaultValue if it fails.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static bool GetBoolean(string key, bool defaultValue = false)
    {
        //Fetch the string and make sure its valid
        string value = GetString(key, "NA");
        if (value == "NA") return defaultValue;

        //Parse it, then return it
        bool b;
        if (!bool.TryParse(value, out b)) return defaultValue;

        return b;
    }
    
    /// <summary>
    /// Tries to fetch the Bool Array from key. Returns empty array if it fails.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool[] GetBooleans(string key)
    {
        //For documentation, see GetFloats
        string value = GetString(key, "NA");
        if (value == "NA") return new bool[0];

        byte[] bytes = Convert.FromBase64String(value);
        bool[] values = new bool[0];

        using (MemoryStream memory = new MemoryStream(bytes))
        using (BinaryReader br = new BinaryReader(memory))
        {
            int count = br.ReadInt32();
            values = new bool[count];
            for (int i = 0; i < count; i++) values[i] = br.ReadBoolean();
        }

        return values;
    }

    /// <summary>
    /// Tries to fetch the Vector2 from key. Returns defaultValue if it fails.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Vector2 GetVector2(string key, Vector2 defaultValue)
    {
        //Fetch the string and make sure its valid
        string value = GetString(key, "NA");
        if (value == "NA") return defaultValue;

        //Split it up appropriately.
        //Using this method as its easier than reading BinaryStreams
        string[] values = value.Split(':');
        if (values.Length != 2) return defaultValue;

        //Parse the values
        float x, y;
        if (!float.TryParse(values[0], out x)) return defaultValue;
        if (!float.TryParse(values[1], out y)) return defaultValue;

        //return the vector
        return new Vector2(x, y);
    }
   
    /// <summary>
    /// Tries to fetch the Vector3 from key. Returns defaultValue if it fails.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Vector3 GetVector3(string key, Vector3 defaultValue)
    {
        //Fetch the string and make sure its valid
        string value = GetString(key, "NA");
        if (value == "NA") return defaultValue;

        //Split it up appropriately.
        //Using this method as its easier than reading BinaryStreams
        string[] values = value.Split(':');
        if (values.Length != 3) return defaultValue;

        //Parse the values
        float x, y, z;
        if (!float.TryParse(values[0], out x)) return defaultValue;
        if (!float.TryParse(values[1], out y)) return defaultValue;
        if (!float.TryParse(values[2], out z)) return defaultValue;

        //return the vector
        return new Vector3(x, y, z);
    }
    
    /// <summary>
    /// Tries to fetch the Color from key. Returns defaultValue if it fails.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Color GetColor(string key, Color defaultValue)
    {
        //Fetch the string and make sure its valid
        string value = GetString(key, "NA");
        if (value == "NA") return defaultValue;

        //Split it up appropriately.
        //Using this method as its easier than reading BinaryStreams
        string[] values = value.Split(':');
        if (values.Length != 4) return defaultValue;

        //Parse all the values, totally not ripped from Vector3 with one extra value.
        float x, y, z, a;
        if (!float.TryParse(values[0], out x)) return defaultValue;
        if (!float.TryParse(values[1], out y)) return defaultValue;
        if (!float.TryParse(values[2], out z)) return defaultValue;
        if (!float.TryParse(values[3], out a)) return defaultValue;

        //Return the colour. (I am Australian, that is spelt correctly)
        return new Color(x, y, z, a);
    }
    
    /// <summary>
    /// Tries to fetch the Rect from key. Returns defaultValue if it fails.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Rect GetRect(string key, Rect defaultValue)
    {
        //Fetch the string and make sure its valid
        string value = GetString(key, "NA");
        if (value == "NA") return defaultValue;

        //Split it up appropriately.
        //Using this method as its easier than reading BinaryStreams
        string[] values = value.Split(':');
        if (values.Length != 4) return defaultValue;

        //Parse all the information. Totally not ripped from teh GetColor
        float x, y, z, a;
        if (!float.TryParse(values[0], out x)) return defaultValue;
        if (!float.TryParse(values[1], out y)) return defaultValue;
        if (!float.TryParse(values[2], out z)) return defaultValue;
        if (!float.TryParse(values[3], out a)) return defaultValue;

        //return the rect
        return new Rect(x, y, z, a);
    }
    
    /// <summary>
    /// Tries to fetch the Texture2D from key. Returns defaultValue if it fails.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Texture2D GetTexture2D(string key, Texture2D defaultValue = null)
    {
        //Get teh value and make sure its valid
        string value = GetString(key, "NA");
        if (value == "NA") return defaultValue;

        //Convert the string into bytes
        byte[] bytes = Convert.FromBase64String(value);

        //Prepare a empty texture
        Texture2D texture = new Texture2D(0, 0);

        //Load the bytes
        texture.LoadImage(bytes);

        //Return the texture
        return texture;
    }
   
    /// <summary>
    /// Tries to fetch the Quaternion from key. Returns defaultValue if it fails.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Quaternion GetQuaternion(string key, Quaternion defaultValue)
    {
        //Real lazy here, just fetch the Euler angles and apply it
        Vector3 rot = GetVector3(key, defaultValue.eulerAngles);
        return Quaternion.Euler(rot);
    }
    
    /// <summary>
    /// Tries to fetch the String from key. Returns defaultValue if it fails.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static string GetString(string key, string defaultValue = "") {
        //Try to get the string
        string value;
        if (playerData.TryGetValue(key, out value)) return value;

        //otherwise return the default value.
        return defaultValue;
    }
   
    /// <summary>
    /// Tries to fetch the Transform information and apply it to tranform.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="transform"></param>
    public static void GetTransform(string key, Transform transform)
    {
        //Get teh value and make sure its valid
        string value = GetString(key, "NA");
        if (value == "NA") return;

        //Split it into 9 pieces
        string[] v = value.Split(':');
        if (v.Length != 9) return;

        //prepare the vectors
        Vector3 pos = new Vector3();
        Vector3 rot = new Vector3();
        Vector3 sca = new Vector3();

        //Parse position
        if (!float.TryParse(v[0], out pos.x)) return;
        if (!float.TryParse(v[1], out pos.y)) return;
        if (!float.TryParse(v[2], out pos.z)) return;

        //Parse rotation
        if (!float.TryParse(v[3], out rot.x)) return;
        if (!float.TryParse(v[4], out rot.y)) return;
        if (!float.TryParse(v[5], out rot.z)) return;

        //Parse scale
        if (!float.TryParse(v[6], out sca.x)) return;
        if (!float.TryParse(v[7], out sca.y)) return;
        if (!float.TryParse(v[8], out sca.z)) return;

        //Apply it all
        transform.position = pos;
        transform.rotation = Quaternion.Euler(rot);
        transform.localScale = sca;
    }
    
    /// <summary>
    /// Tries to fetch the DateTime from key. Returns defaultValue if it fails.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static DateTime GetDateTime(string key, DateTime defaultValue)
    {
        //Get teh value and make sure its valid
        string value = GetString(key, "NA");
        if (value == "NA") return defaultValue;

        //Convert the value into a long
        long t;
        if (!long.TryParse(value, out t)) return defaultValue;

        //Get the time from that long
        return DateTime.FromBinary(t);
    }
    #endregion
    
    #region Setters

    //Floats
    /// <summary>
    /// Sets the given float to key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetFloat(string key, float value) { SetString(key, value.ToString()); }
    
    /// <summary>
    /// Sets the given float array to key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetFloats(string key, float[] value)
    {
        //We have a invalid value :C
        if (value == null) return;

        //Start a memory stream to store the data in
        using (MemoryStream stream = new MemoryStream())
        {
            //Create a BinaryWrite from the memory stream
            using (BinaryWriter bw = new BinaryWriter(stream))
            {
                //Write the length of the array
                bw.Write(value.Length);

                //Itterate through writing every value
                for (int i = 0; i < value.Length; i++) bw.Write(value[i]);
            }

            //Get the written bytes
            byte[] bytes = stream.ToArray();

            //Convert the bytes to a string
            string data = Convert.ToBase64String(bytes);

            //Store the string.
            SetString(key, data);
        }
    }

    //Integers
    /// <summary>
    /// Sets the given int to key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetInt(string key, int value) { SetString(key, value.ToString()); }
    
    /// <summary>
    /// Sets the given int array to key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetInts(string key, int[] value)
    {
        //For documentation, see "SetFloats(string key, float[] value)"
        if (value == null) return;

        using (MemoryStream stream = new MemoryStream())
        {
            using (BinaryWriter bw = new BinaryWriter(stream))
            {
                bw.Write(value.Length);
                for (int i = 0; i < value.Length; i++) bw.Write(value[i]);
            }

            byte[] bytes = stream.ToArray();
            string data = Convert.ToBase64String(bytes);
            SetString(key, data);
        }
    }

    //Bytes
    /// <summary>
    /// Sets the given byte array to key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetBytes(string key, byte[] value)
    {
        //a invalid value!
        if (value == null) return;

        //Turn the bytes into tasty tasty string data
        string data = Convert.ToBase64String(value);

        //Store the string.
        SetString(key, data);
    }
    
    //Booleans
    /// <summary>
    /// Sets the given bool to key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetBoolean(string key, bool value) { SetString(key, value.ToString()); }
    
    /// <summary>
    /// Sets the given bool array to key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetBooleans(string key, bool[] value)
    { 
        //For documentation, see "SetFloats(string key, float[] value)"       
        if (value == null) return;

        using (MemoryStream stream = new MemoryStream())
        {
            using (BinaryWriter bw = new BinaryWriter(stream))
            {
                bw.Write(value.Length);
                for (int i = 0; i < value.Length; i++) bw.Write(value[i]);
            }

            byte[] bytes = stream.ToArray();
            string data = Convert.ToBase64String(bytes);
            SetString(key, data);
        }
    }

    //Vectors
    /// <summary>
    /// Sets the given Vector2 to key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetVector2(string key, Vector2 value) { SetString(key, value.x + ":" + value.y); }
    
    /// <summary>
    /// Sets the given Vector3 to key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetVector3(string key, Vector3 value) { SetString(key, value.x + ":" + value.y + ":" + value.z); }
    
    /// <summary>
    /// Sets the current Quaternion to key. It is first converted to Euler angles.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetQuaternion(string key, Quaternion value) { SetVector3(key, value.eulerAngles);  }

    //Color
    /// <summary>
    /// Sets the given colour to key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetColor(string key, Color value) { SetString(key, value.r + ":" + value.g + ":" + value.b + ":" + value.a); }

    //Texture
    /// <summary>
    /// Sets the given Texture2D to key. Texture2D must be readable.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="storage"></param>
    public static void SetTexture2D(string key, Texture2D value, TextureStorageMethod storage = TextureStorageMethod.PNG) {
        //A invalid value D:
        if (value == null) return;
        
        //Get the appropraite bytes
        byte[] bytes = storage == TextureStorageMethod.PNG ? value.EncodeToPNG() : value.EncodeToJPG();

        //Thats right, we are turning this picture into a string! Oh shnap!
        string picture = Convert.ToBase64String(bytes);

        //Now save dem picture string nice a safley
        SetString(key, picture);
    }
   
    //String
    /// <summary>
    /// Sets the given string to key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetString(string key, string value) {
        //Well.... this... is much more simple than all the other setters...
        playerData[key] = value;
    }
    
    //Rectanlge
    /// <summary>
    /// Sets the given rect to key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="rect"></param>
    public static void SetRect(string key, Rect rect) { SetString(key, rect.x + ":" + rect.y + ":" + rect.width + ":" + rect.height); }

    //Transform (Scale, Position and Rotation)
    /// <summary>
    /// Sets the transforms position, rotation and local scale to key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetTransform(string key, Transform value)
    {   
        //Get its position, rotation and scale in Vector3's
        Vector3 pos = value.position;
        Vector3 rot = value.rotation.eulerAngles;
        Vector3 sca = value.localScale;

        //Turn it into one long string
        string save = pos.x + ":" + pos.y + ":" + pos.z;
        save += ":" + rot.x + ":" + rot.y + ":" + pos.z;
        save += ":" + sca.x + ":" + sca.y + ":" + sca.z;

        //Save that one long string
        SetString(key, save);
    }
    
    //Time
    /// <summary>
    /// Sets the give DateTime to key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="time"></param>
    public static void SetDateTime(string key, DateTime time)
    {
        //Get the long from the time.
        long t = time.ToBinary();

        //Store it as a string
        SetString(key, t.ToString());
    }
    
    #endregion

    #region Helpers
    /// <summary>
    /// Check if it contains a key
    /// </summary>
    /// <param name="key">The key to check</param>
    /// <returns>True if the key exists</returns>
    public static bool HasKey(string key) { return playerData.ContainsKey(key); }
    
    /// <summary>
    /// Saves the settings to the GetSaveFile() path with specified encryption.
    /// </summary>
    public static void Save()
    {
        //We double set the path just incase the default containts special values.
        PlayerSave.SetSaveFile(currentSave);

        //Check if we have anything to save. If not, don't even bother opening the streams.
        if (playerData.Count == 0) return;

        //Should we encrypt?
        bool doCrypt = ecnryptMethod != SaveEncryptionMethod.None;

        //If the directory does not exist, create it. M'aiq is very practical. He has no need for mysticism.
        if (!Directory.Exists(Path.GetDirectoryName(currentSave))) Directory.CreateDirectory(Path.GetDirectoryName(currentSave));

        //Create the file stream
        using (FileStream fs = new FileStream(currentSave, FileMode.Create))
        {
            //Prepare the cryptostream
            CryptoStream cs = null;
            if (doCrypt) cs = GetCryptoStream(fs, CryptoStreamMode.Write);

            //Create the BinaryWriter using the cryptostream if we are encrypting, otherwise the filestream
            using (BinaryWriter bw = new BinaryWriter(doCrypt ? (Stream)cs : fs))
            {
                //First write how many keys there is
                bw.Write(playerData.Count);

                //Itterate through each of the keys
                foreach (string key in playerData.Keys)
                {
                    //Write the key
                    bw.Write(key);

                    //Write the value
                    bw.Write(playerData[key]);
                }
            }

            //Close the cryptostream.
            if (doCrypt) cs.Close();
        }
    }
    
    /// <summary>
    /// Loads all settings from the save file. Clears the current settings.
    /// </summary>
    /// <returns>false if it was unable to load. Can be caused by non-existent file or bad encryption</returns>
    public static bool Load()
    {
        //Clear all current settings.
        playerData.Clear();

        //If teh file does not exist, return.
        if (!File.Exists(currentSave)) return false;

        try
        {
            //Should we decrypt?
            bool doCrypt = ecnryptMethod != SaveEncryptionMethod.None;

            //Create the file stream.
            using (FileStream fs = new FileStream(currentSave, FileMode.Open))
            {
                //prepare the crypto stream
                CryptoStream cs = null;
                if (doCrypt) cs = GetCryptoStream(fs, CryptoStreamMode.Read);

                //Create the binaryreader form either the cryptostream of the filestream.
                using (BinaryReader br = new BinaryReader(doCrypt ? (Stream)cs : fs))
                {
                    //First read the keys and how many keys there is
                    int count = br.ReadInt32();

                    //Itterate through all the keys
                    for (int i = 0; i < count; i++)
                    {
                        //Read the key
                        string key = br.ReadString();

                        //Read its value
                        string value = br.ReadString();

                        //add it to the list
                        playerData.Add(key, value);
                    }
                }

                //Close the cryptostream
                if (doCrypt) cs.Close();
            }
        }
        catch (Exception e)
        {
            if (LogLoadFailures) Debug.LogError("Unable to decrypt save. " + e.Message);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Sets the current save file. {data_path} will become Application.dataPath and {persistent_data_path} will become Application.persistantDataPath
    /// </summary>
    /// <param name="save">The path to the save file.</param>
    public static void SetSaveFile(string save)
    {
        //Empty file names are invalid.
        if (save == "") return;

        //Replace the {data_path} and {persistent_data_path} tags.
        save = save.Replace("{data_path}", Application.dataPath);
        save = save.Replace("{persistent_data_path}", Application.persistentDataPath);

        //Set the current path
        currentSave = save;
    }
    
    /// <summary>
    /// Returns the current path to the save file
    /// </summary>
    /// <returns>Path of the save file</returns>
    public static string GetSaveFile() { return currentSave; }
    
    /// <summary>
    /// Sets the current encryption method
    /// </summary>
    /// <param name="method">The encryption to set too.</param>
    public static void SetEncryptionMethod(SaveEncryptionMethod method)
    {
        ecnryptMethod = method;
    }
    
    /// <summary>
    /// Get the current encryption method being used.
    /// </summary>
    /// <returns>The current encryption method.</returns>
    public static SaveEncryptionMethod GetEncryptionMethod()
    {
        return ecnryptMethod;
    }

    /// <summary>
    /// Deletes all the keys and values from the save. If deleteFile is true, then it will delete the save file too.
    /// </summary>
    /// <param name="deleteFile"></param>
    public static void DeleteAll(bool deleteFile = false) { playerData.Clear(); if (deleteFile && File.Exists(currentSave)) File.Delete(currentSave); }

    /// <summary>
    /// Deletes the passed key from the stored save.
    /// </summary>
    /// <param name="key"></param>
    public static void DeleteKey(string key) { playerData.Remove(key); }


    #endregion

    #region Private internal methods
    static CryptoStream GetCryptoStream(FileStream fs, CryptoStreamMode mode) {

        //Get the Cipher bytes using the encryptKey
        byte[] kbytes, vbytes;
        GetCipherBytes(encryptKey, out kbytes, out vbytes);

        //Prepare the algorithm
        SymmetricAlgorithm alg;


        //Create the appropriate alogrithm. Defaults to AES
        switch (ecnryptMethod)
        {
                //Sadly AES is broken for what ever reason. Might be a mono-issue.
            //default:
            //    alg = Aes.Create();
            //    break;

            case SaveEncryptionMethod.DES:
                alg = DES.Create();
                break;

            case SaveEncryptionMethod.TripleDES:
                alg = TripleDES.Create();
                break;

            case SaveEncryptionMethod.RC2:
                alg = RC2.Create();
                break;

            //case SaveEncryptionMethod.Rijndael:
            default:
                alg = Rijndael.Create();
                break;
        }

        //Prepare the transform
        ICryptoTransform transform;

        //Create the Descryptor if we are reading, otherwise the encryptor.
        if (mode == CryptoStreamMode.Read) 
            transform = alg.CreateDecryptor(kbytes, vbytes);
        else
            transform =  alg.CreateEncryptor(kbytes, vbytes);

        //Return the new stream.
        return new CryptoStream(fs, transform, mode);
    } 
    static void GetCipherBytes(string password, out byte[] kBytes, out byte[] vBytes)
    {
        //M'aiq knows much, and tells some. M'aiq knows many things others do not.
        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password,
            new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 
            0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

        kBytes = pdb.GetBytes(32);
        vBytes = pdb.GetBytes(16);
    }
    #endregion
}

/// <summary>
/// The format to save textures as in the save file.
/// </summary>
public enum TextureStorageMethod
{
    PNG,
    JPG
}

/// <summary>
/// Type of encryption to use in the save file. Specific methods may require specific keys.
/// </summary>
public enum SaveEncryptionMethod
{
    None,
    //AES,
    DES,
    TripleDES,
    RC2,
    Rijndael
}