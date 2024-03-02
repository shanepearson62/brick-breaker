using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [Header("Global Object References")]
    public GameObject inGameCanvasRef;
    public GameObject heartsRef;
    public TextMeshProUGUI scoreRef;

    // Find Object Refs on Level Load
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }

    [Header("Game Variables")]
    public int level = 1;
    public int score = 0;
    public int lives = 3;
    public int bricksBroken = 0;
    public int numBricks = 0;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(inGameCanvasRef);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    void Start()
    {
        NewGame();
    }

    // FOR DEV TOOLS 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadLevel(2);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadLevel(3);
        }
    }

    public void NewGame()
    {
        this.score = 0;
        this.lives = 3;
        this.bricksBroken = 0;
        this.numBricks = 0;
        scoreRef.text = $"Score: {this.score}";

        //Reset Heart UI
        for (int i = 0; i < heartsRef.transform.childCount; i++)
        {
            heartsRef.transform.GetChild(i).gameObject.SetActive(true);
        }

        LoadLevel(1);
        //LoadLevel(2);       // Only leave on when Testing lvl2
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
        scoreRef.text = $"Score: {this.score}";

        if (BeatLevel())
        {
            LoadLevel(this.level + 1);
            if (this.level + 1 >= SceneManager.sceneCountInBuildSettings)
            {
                // turn on player wins canvas & unload lvl
                inGameCanvasRef.SetActive(false);
            }
        }
    }

    public void LoseLife()
    {
        this.lives--;
        heartsRef.transform.GetChild(lives).gameObject.SetActive(false);

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

    public void ExitGame()
    {
        Application.Quit();
    }
}
