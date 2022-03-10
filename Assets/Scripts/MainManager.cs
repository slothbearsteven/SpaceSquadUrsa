using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static int highScore;
    public static int score;


    public static bool gameActive;
    public static MainManager Instance { get; private set; }

    // Update is called once per frame
    void Update()
    {
        SetHighScore();

    }




    void SetHighScore()
    {
        if (score >= highScore)
        {
            highScore = score;
            SaveHighScore();
        }
    }




    private void Awake()
    {
        score = 0;
        gameActive = true;
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();

    }
    [System.Serializable]
    class SaveData
    {
        public int highScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();

        data.highScore = highScore;


        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);


            highScore = data.highScore;
        }
    }
}

