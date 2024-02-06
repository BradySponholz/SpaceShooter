using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    private string _scoreLength = "0000000";
    [SerializeField]
    private Image _lifeImage;
    [SerializeField]
    private Sprite[] _lifeSprites;
    [SerializeField]
    private TMP_Text _gameOverText;
    [SerializeField]
    private TMP_Text _getReadyText;
    [SerializeField]
    private TMP_Text _endGameText;
    [SerializeField]
    private Button _restartButton;
    private GameManager _gameManager;
    [SerializeField]
    private GameObject _startEnemy;    
    

    void Start()
    {
        _scoreText.text = "Score: " + _scoreLength;
        _gameOverText.gameObject.SetActive(false);
        _getReadyText.gameObject.SetActive(true);
        _endGameText.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        Instantiate(_startEnemy, transform.position, Quaternion.identity);

        StartCoroutine(GetReadyFlicker());

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _endGameText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        _endGameText.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString(_scoreLength);
    }

    public void UpdateLife(int currentLife)
    {
        _lifeImage.sprite = _lifeSprites[currentLife];

        if (currentLife == 0)
        {
            GameOverSequence();
        }
    }

    public void Ready()
    {
        _getReadyText.gameObject.SetActive(false);
        StopCoroutine(GetReadyFlicker());
    }

    void GameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
        StartCoroutine(RestartButton());
    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator RestartButton()
    {
        yield return new WaitForSeconds(5.5f);
        _restartButton.gameObject.SetActive(true);
    }

    IEnumerator GetReadyFlicker()
    {
        while (true)
        {
            _getReadyText.text = "GET READY";
            yield return new WaitForSeconds(0.5f);
            _getReadyText.text = "";
            yield return new WaitForSeconds(0.5f);            
        }
    }
}
