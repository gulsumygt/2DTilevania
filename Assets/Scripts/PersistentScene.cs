using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentScene : MonoBehaviour
{
    int startSceneIndex;

    private void Awake()
    {
        var curentObjects = FindObjectsOfType<PersistentScene>().Length;

        if (curentObjects > 1)
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
        startSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex != startSceneIndex)
        {
            Destroy(gameObject);
        }
    }
}
