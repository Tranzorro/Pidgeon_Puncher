using UnityEngine;
using System.Collections;

public class DemoPlayerPrefAlt : MonoBehaviour {
    public Texture2D texture;

    //The PlayerSave can be used in the same way as PlayerPref, so it seemlessly intergrate with your projects.
    void Comparison()
    {
        //== Comparison of getting a level:
        int level = PlayerPrefs.GetInt("currentLevel", 2);
        level = PlayerSave.GetInt("currentLevel", 2);


        //== Comparison of getting a name:
        string playerName = PlayerPrefs.GetString("playerName", "Jeff");
        playerName = PlayerSave.GetString("playerName", "Jeff");

        //== Comparison of getting a texture:

        //string path = PlayerPrefs.GetString("avatar_path", Application.dataPath + " /avatar.png");
        //byte[] data = System.IO.File.ReadAllBytes(path);
        //Texture2D avatar = new Texture2D(1, 1); avatar.LoadImage(data);
        Texture2D avatar = PlayerSave.GetTexture2D("avatar", null);


        //Just using these so the "not used" warning doesn't appear
        level.ToString();
        playerName.ToString();
        avatar.ToString();
    }
    
    void Start()
    {
        //Set encryption to none so we can see it.
        PlayerSave.SetEncryptionMethod(SaveEncryptionMethod.None);

        //All saving functions
        //String
          PlayerSave.SetString("string", "value");

        //Floats
          PlayerSave.SetFloat("float", 0F);
          PlayerSave.SetFloats("floats", new float[] { 0F, 0.1F, 0.2F });

        //Ints
          PlayerSave.SetInt("int", 0);
          PlayerSave.SetInts("ints", new int[] { 0, 1, 2 });

        //Booleans
          PlayerSave.SetBoolean("bool", true);
          PlayerSave.SetBooleans("bools", new bool[] { true, false, false} );

        //Bytes
          PlayerSave.SetBytes("bytes", new byte[] { 0, 1, 2, 4, 8, 16, 32 });

        //Vectors
          PlayerSave.SetVector2("vector2", Vector2.up);
          PlayerSave.SetVector3("vector3", Vector3.one);
          PlayerSave.SetQuaternion("quaternion", transform.rotation);

        //Color
          PlayerSave.SetColor("color", Color.green);
        
        //Rectangle
          PlayerSave.SetRect("rect", new Rect(10, 10, 240, 25));

        //Time
          PlayerSave.SetDateTime("time", System.DateTime.Now);

        //Transform (pos, rot, scal combined)
          PlayerSave.SetTransform("transform", transform);
        
        //Texture
          PlayerSave.SetTexture2D("texture", texture);

        //Save the file
          PlayerSave.Save();
    }
}
