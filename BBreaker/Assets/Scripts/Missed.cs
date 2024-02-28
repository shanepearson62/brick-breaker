using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missed : MonoBehaviour
{
    private static GameManager gameManager;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Ball ball = other.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            // Lose a life
            gameManager.LoseLife();
        }
    }
}
