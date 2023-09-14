using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class gameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    
    private void Awake()
    {
        
        int numGameSessions = FindObjectsOfType<gameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = "Lives: " + playerLives.ToString();
        scoreText.text = "Score: " + score.ToString();
    }

    public IEnumerator ProcessPlayerDeath(float deathAnimationLength)
    {
        yield return new WaitForSeconds(deathAnimationLength);
        
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        { ResetGameSession();}

    }

    void TakeLife()
    {
        playerLives--;
        FindObjectOfType<DrillBehaviour>().TimerReset();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = "Lives: "+playerLives.ToString();

    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void IncreaseScore(int pointsAdded)
    {
        score+=pointsAdded;
        scoreText.text = "Score: " + score.ToString();
    }
}
