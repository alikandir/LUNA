using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelExit : MonoBehaviour
{
    [SerializeField] float loadDelay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            StartCoroutine(LoadNextLevel());
        
        }

        IEnumerator LoadNextLevel()
        {
            yield return new WaitForSecondsRealtime(loadDelay);
            int levelIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = levelIndex + 1;

            if (nextSceneIndex==SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);

        }
    }
}
