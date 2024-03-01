using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] brickStates;
    public int health { get; private set; }
    public bool unbreakable;
    public int points = 100;        // Per Hit
    private static GameManager gameManager;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        if (!unbreakable)
            gameManager.numBricks++;
    }

    private void Start()
    {
        if (!this.unbreakable)
        {
            this.health = this.brickStates.Length;
            this.spriteRenderer.sprite = this.brickStates[this.health - 1];
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Ball ball = other.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            Hit();
        }
    }

    private void Hit()
    {
        if (this.unbreakable)
        {
            return;
        }

        this.health--;
        if (this.health <= 0)
        {
            gameManager.bricksBroken++;
            Destroy(this.gameObject);
        }
        else
        {
            this.spriteRenderer.sprite = this.brickStates[this.health - 1];
        }

        gameManager.Hit(this);
    }
}
