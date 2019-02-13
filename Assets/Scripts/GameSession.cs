using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    [SerializeField] Text scoreTxt;
    [SerializeField] Text liveTxt;

    private void Awake()
    {
        var currentObjects = FindObjectsOfType<GameSession>().Length;

        if (currentObjects > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreTxt.text = score.ToString();
        liveTxt.text = playerLives.ToString();
    }

    public void AddToScore(int scorePoint)
    {
        score += scorePoint;
        scoreTxt.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();

        }
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);

        Destroy(gameObject);
    }

    private void TakeLife()
    {
        playerLives--;

        var currentIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentIndex);

        liveTxt.text = playerLives.ToString();
    }
}
