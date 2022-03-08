using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int highScore;
    public static int score;

    public Text ScoreText;
    public static bool gameActive;
    public GameObject GameOverText;
    public static MainManager Instance;
    void Start()
    {
        score = 0;
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
        ScoreText.text = $"Score : {score}";
    }

    public void GameOver()
    {
        if (PlayerController.energy <= 0)
        {
            gameActive = false;
            GameOverText.SetActive(true);
        }
    }





    private void Awake()
    {
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

