using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(fileName = "SaveManager", menuName = "Managers/SaveManager", order = 0)]
public class SaveManager : ScriptableObject 
{
    [SerializeField] HealthManager manager;

    public GameObject playerPrefab;

    public void CreateSave(Save save) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, save);
        stream.Close();
    }

    public void LoadSave(Save save) 
    {
        SceneManager.LoadScene(save.sceneName);
        Instantiate(playerPrefab, new Vector3(save.x, save.y, save.z), Quaternion.identity);
    }    
}

[System.Serializable]
public class Save 
{
        public string sceneName;

        public float x;
        public float y;
        public float z;

        public int health;
    }