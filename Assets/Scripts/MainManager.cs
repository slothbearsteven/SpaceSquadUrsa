using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{

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
    {//if the current score is ever equal to or higher than the current score, the high score will be replaced in that instance and saved.
        if (score >= highScore)
        {
            highScore = score;
            SaveHighScore();
        }
    }




    private void Awake()
    {
        //Whenever the scene plays (as it is not destroyed on load, therefore it awakes instead of starting for each time the game is restarted.)
        //The variables for the game are set to their default values, and the highscore is loaded from the saved files.
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
    {//uses the currently set  high score to save a json file of said score, allowing it to persist after the game is exited.
        SaveData data = new SaveData();

        data.highScore = highScore;


        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {//checks for the json file then loads the highscore if said file is detected
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);


            highScore = data.highScore;
        }
    }
}

