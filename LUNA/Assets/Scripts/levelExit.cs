using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelExit : MonoBehaviour
{
    [SerializeField] float loadDelay;
    playerMovement player;
    private void Start()
    {
        player = FindObjectOfType<playerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            player.DisableControl();
            StartCoroutine(LoadNextLevel());
        }
        IEnumerator LoadNextLevel()
        {
            yield return new WaitForSecondsRealtime(loadDelay);
            player.EnableControl();
            int levelIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = levelIndex + 1;

            if (nextSceneIndex==SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            FindObjectOfType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(nextSceneIndex);

        }
    }
}
