using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using Spawner;

public class GameManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _menuUI;
    [SerializeField]
    private GameObject _gameUI;
    [SerializeField]
    private GameObject _spawnManager;
    [SerializeField]
    private GameObject _enemySpawner;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private GameObject _flyIn;



    private void Start()
    {
        _menuUI.SetActive(true);
        _gameUI.SetActive(false);
        _spawnManager.SetActive(false);
        _enemySpawner.SetActive(false);
        _player.gameObject.SetActive(false);
        _flyIn.SetActive(false);
    }

    public void StartGame()
    {
        _gameUI.SetActive(true);
        _menuUI.SetActive(false);
        StartCoroutine(GameReady());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    IEnumerator GameReady()
    {
        _flyIn.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        _player.gameObject.SetActive(true);
        _flyIn.SetActive(false);
        _spawnManager.SetActive(true);
        _enemySpawner.SetActive(true);
        EndTransition();
    }

    private void EndTransition()
    {
        StopCoroutine(GameReady());
    }
}
