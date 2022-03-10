using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainUI : MonoBehaviour
{
    public Text highScoreText;
    public Text ScoreText;
    public GameObject GameOverText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MainManager.gameActive)
        {

            ScoreText.text = $"Score : {MainManager.score}";
        }
        GameOver();
    }
    public void GameOver()
    {
        if (PlayerController.energy <= 0)
        {
            MainManager.gameActive = false;
            GameOverText.SetActive(true);
            highScoreText.text = $"HIGH SCORE: {MainManager.highScore}";
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(1);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
