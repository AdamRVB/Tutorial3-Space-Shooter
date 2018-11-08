using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text RestartText;
    public Text GameOverText;

    private bool GameOver;
    private bool Restart;
    private int score;

    private void Start()
    {
        GameOver = false;
        Restart = false;
        RestartText.text = "";
        GameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }
    private void Update()
    {
        if (Restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0, hazards.Length)]; 
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds (waveWait);

            if (GameOver)
            {
                RestartText.text = "Press 'R' for Restart";
                Restart = true;
                break;
            }
        }  
    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }
    void UpdateScore()
    {
        ScoreText.text = "Score: " + score;
    }
    public void gameOver()
    {
        GameOverText.text = "Game Over!";
        GameOver = true;
    }
}
