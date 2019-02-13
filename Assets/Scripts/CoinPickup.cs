using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinClips;
    [SerializeField] int scorePointToAdd = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinClips, Camera.main.transform.position);

        Destroy(gameObject);

        var gameSession = FindObjectOfType<GameSession>();
        gameSession.AddToScore(scorePointToAdd);
    }
}
