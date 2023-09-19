using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
    [SerializeField] Canvas menuCanvas;
    [SerializeField] Canvas howToPlayCanvas;
    [SerializeField] Canvas creditsCanvas;
    [SerializeField] Canvas languagesCanvas;
    public void GameStart()
    {
        int currentSceneIndex=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex+1);
    }
    public void HowToPlay()
    {
        menuCanvas.gameObject.SetActive(false);
        howToPlayCanvas.gameObject.SetActive(true);
    }
    public void BackToMenu()
    {
        menuCanvas.gameObject.SetActive(true);
        howToPlayCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
        languagesCanvas.gameObject.SetActive(false);
    }
    public void Credits()
    {
        menuCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(true);
    }
    public void Languages()
    {
        menuCanvas.gameObject.SetActive(false);
        languagesCanvas.gameObject.SetActive(true);
    }
}
