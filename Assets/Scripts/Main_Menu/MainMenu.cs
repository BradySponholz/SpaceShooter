using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private GameManager _gameManager;
    //private Animator _camera;

    private void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    public void ClickStart()
    {
        _gameManager.StartGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
