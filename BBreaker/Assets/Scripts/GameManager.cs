using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public int level = 1;
    public int score = 0;
    public int lives = 3;
    public int bricksBroken = 0;
    public int numBricks = 0;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;
        this.bricksBroken = 0;
        this.numBricks = 0;

        LoadLevel(1);
    }

    private void LoadLevel(int lvl)
    {
        this.level = lvl;

        SceneManager.LoadScene(this.level);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
    }

    private void ResetLevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
    }

    private void GameOver()
    {
        //SceneManager.LoadScene("GameOver");
        NewGame();
    }

    public void Hit(Bricks brick)
    {
        this.score += brick.points;

        if (BeatLevel())
        {
            LoadLevel(this.level + 1);
        }
    }

    public void LoseLife()
    {
        this.lives--;

        if (this.lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }

    private bool BeatLevel()
    {
        if (bricksBroken >= numBricks)
            return true;
        else
            return false;
    }
}
