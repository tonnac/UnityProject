using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GameObject canvasRef;

    private bool gameOver = false;
    private bool restart = false;

    private int score = 0;
    private Text scoreText;
    private Text restartText;
    private Text gameoverText;

    void Start()
    {
        StartCoroutine(SpawnWaves());

        MonoBehaviour[] d = canvasRef.GetComponentsInChildren<MonoBehaviour>();
        for(int i=0; i<d.Length; ++i)
        {
            Text t = d[i].GetComponent<Text>();
            if(t != null)
            {
                string name = t.name;
                if(name == "Restart Text")
                {
                    restartText = t;
                    Debug.Log("Restart");
                }
                else if(name == "Gameover Text")
                {
                    gameoverText = t;
                    Debug.Log("Gameover");
                }
                else
                {
                    scoreText = t;
                    Debug.Log("Score");
                }
            }
        }

        restartText.text = "";
        gameoverText.text = "";

        UpdateScore();
    }

    void Update()
    {
        if(restart)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(startWait);
            for (int i = 0; i < hazardCount; ++i)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if(gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                Debug.Log("Restart");
                break;
            }
        }
    }
    
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void Gameover()
    {
        gameoverText.text = "Game Over";
        gameOver = true;
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
