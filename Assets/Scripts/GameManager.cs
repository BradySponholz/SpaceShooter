using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _menuUI;
    [SerializeField]
    private GameObject _gameUI;
    [SerializeField]
    private GameObject _spawnManager;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private GameObject _flyIn;



    private void Start()
    {
        _menuUI.gameObject.SetActive(true);
        _gameUI.gameObject.SetActive(false);
        _spawnManager.gameObject.SetActive(false);
        _player.gameObject.SetActive(false);
        _flyIn.gameObject.SetActive(false);
    }

    private void Update()
    {
       
    }


    public void StartGame()
    {
        _gameUI.gameObject.SetActive(true);
        _spawnManager.gameObject.SetActive(true);
        _menuUI.gameObject.SetActive(false);
        StartCoroutine(GameReady());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    IEnumerator GameReady()
    {
        _flyIn.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        _player.gameObject.SetActive(true);
        _flyIn.gameObject.SetActive(false);
        EndTransition();
    }

    private void EndTransition()
    {
        StopCoroutine(GameReady());
    }
}
