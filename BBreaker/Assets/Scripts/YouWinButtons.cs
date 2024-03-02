using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YouWinButtons : MonoBehaviour
{
    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void RestartButton()
    {
        gameManager.NewGame();
    }

    public void QuitGameButton()
    {
        gameManager.ExitGame();
    }
}
