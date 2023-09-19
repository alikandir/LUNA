using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class gameSession : MonoBehaviour
{

    public int playerDeaths;
    [SerializeField] TextMeshProUGUI deathsString;
    [SerializeField] TextMeshProUGUI deathsText;
    [SerializeField] TextMeshProUGUI totalDeathsLastScreen;
    
    
    
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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            
            deathsText.gameObject.SetActive(false);
            totalDeathsLastScreen.gameObject.SetActive(true);
            deathsString.gameObject.SetActive(false);
            totalDeathsLastScreen.text = playerDeaths.ToString();
        }
        else
        {
            deathsText.gameObject.SetActive(true);
            totalDeathsLastScreen.gameObject.SetActive(false);
            deathsString.gameObject.SetActive(true);
            deathsText.text = playerDeaths.ToString();
        }
    }

    public IEnumerator ProcessPlayerDeath(float deathAnimationLength)
    {
       
            yield return new WaitForSeconds(deathAnimationLength);
        
            TakeLife();
    }

    void TakeLife()
    {
        playerDeaths++;
        FindObjectOfType<DrillBehaviour>().TimerReset();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        deathsText.text = playerDeaths.ToString();

    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    
}
