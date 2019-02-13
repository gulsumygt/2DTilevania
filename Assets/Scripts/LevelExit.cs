using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] float levelExisSlowMoFactor = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LevelLoad());
    }
    
    IEnumerator LevelLoad()
    {
        Time.timeScale = levelExisSlowMoFactor;
        
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        Time.timeScale = 1f;

        var currentIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentIndex+1);
    }
}
