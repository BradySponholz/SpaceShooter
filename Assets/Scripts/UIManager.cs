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
    private int _score;
    private string _scoreLength = "0000000";
    [SerializeField]
    private TMP_Text _coinText;
    private string _coinLength = "0000000";
    private bool _stopScoring = false;
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
    

    void Start()
    {
        _scoreText.text = "Score: " + _scoreLength;
        _coinText.text = "Coin: " + _coinLength;
        _gameOverText.gameObject.SetActive(false);
        _getReadyText.gameObject.SetActive(true);
        _endGameText.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        StartCoroutine(GetReadyFlicker());
        StartCoroutine(ReadySequence());

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

    private void UpdateScore()
    {
        while (_stopScoring == false)
        {
            _score = (int)Time.time / 5;
            _scoreText.text = "Score: " + _score.ToString(_scoreLength);
        }
    }

    public void UpdateCoins(int playerCoins)
    {
        _coinText.text = "Coin: " + playerCoins.ToString(_coinLength);
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
        UpdateScore();
        StopCoroutine(GetReadyFlicker());
        StopCoroutine(ReadySequence());
    }

    void GameOverSequence()
    {
        _stopScoring = true;
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
            _getReadyText.text = "3";
            yield return new WaitForSeconds(0.5f);
            _getReadyText.text = "";
            yield return new WaitForSeconds(0.5f);
            _getReadyText.text = "2";
            yield return new WaitForSeconds(0.5f);
            _getReadyText.text = "";
            yield return new WaitForSeconds(0.5f);
            _getReadyText.text = "1";
            yield return new WaitForSeconds(0.5f);
            _getReadyText.text = "";
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator ReadySequence()
    {
        yield return new WaitForSeconds(3.5f);
        Ready();
        yield return new WaitForSeconds(1.0f);
    }
}
