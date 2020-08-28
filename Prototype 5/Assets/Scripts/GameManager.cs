using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    float spawnRate;
    float minSpawnRate;
    float maxSpawnRate;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverUIObjects;
    public GameObject titleScreenUIObjects;
   //public TextMeshProUGUI gameOverText;
    //public TextMeshProUGUI titleText;
    //public Button restartButton;
    //public Button easyButton;
    //public Button normalButton;
    //public Button hardButton;
    private enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }

    Difficulty selectedDifficulty;

    private int score;
    public bool isGameOver;
    


    // Start is called before the first frame update
    void Start()
    {
        //isGameOver = false;
        //StartCoroutine(SpawnTarget());
        
        
        score = 0;
        scoreText.text = "SCORE: " + score;
        SetTitleScreenUI();
      



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while (!isGameOver)
        {
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        
        
            score += scoreToAdd;
            scoreText.text = "SCORE: " + score;
        
    }

    public void GameOver()
    {
        gameOverUIObjects.gameObject.SetActive(true);
        isGameOver = true;       
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGameEasy()
    {
        selectedDifficulty = Difficulty.Easy;
        SetGameScreenUI();
        isGameOver = false;
        SetSpawnRate(selectedDifficulty);
        StartCoroutine(SpawnTarget());

    }

    public void StartGameNormal()
    {
        selectedDifficulty = Difficulty.Normal;
        SetGameScreenUI();
        isGameOver = false;
        SetSpawnRate(selectedDifficulty);
        StartCoroutine(SpawnTarget());

    }

    public void StartGameHard()
    {
        selectedDifficulty = Difficulty.Hard;
        SetGameScreenUI();
        isGameOver = false;
        SetSpawnRate(selectedDifficulty);
        StartCoroutine(SpawnTarget());

    }

    private void SetTitleScreenUI()
    {
        scoreText.gameObject.SetActive(false);
        gameOverUIObjects.gameObject.SetActive(false);
        titleScreenUIObjects.gameObject.SetActive(true);
        
    }

    private void SetGameScreenUI()
    {
        scoreText.gameObject.SetActive(true);
        titleScreenUIObjects.gameObject.SetActive(false);
    }

    private void SetSpawnRate(Difficulty diff)
    {
        if (diff == Difficulty.Easy)
        {
            minSpawnRate = 1.5f;
            maxSpawnRate = 3.0f;
        }

        else if (diff == Difficulty.Normal)
        {
            minSpawnRate = 1.0f;
            maxSpawnRate = 2.5f;
        }

        else
        {
            minSpawnRate = 0.5f;
            maxSpawnRate = 1.0f;
        }
    }
        


}
