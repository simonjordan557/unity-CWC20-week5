using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    float minSpawnRate = 1.0f;
    float maxSpawnRate = 3.0f;
    float spawnRate;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    private int score;
    public bool isGameOver;


    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        StartCoroutine(SpawnTarget());
        score = 0;
        scoreText.text = "SCORE: " + score;
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

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
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameOver = true;       
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
