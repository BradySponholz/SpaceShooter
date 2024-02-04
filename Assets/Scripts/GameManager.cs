using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    [SerializeField]
    private TMP_Text _endGameText;

    private void Start()
    {
        _endGameText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _endGameText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void ResumeGame()
    {
        _endGameText.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
