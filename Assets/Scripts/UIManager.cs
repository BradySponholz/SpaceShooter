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
    private int _highScore;
    private bool _keepScore = true;
    private string _scoreLength = "0000000";
    [SerializeField]
    private TMP_Text _highScoreText;
    private string _highScoreLength = "0000000";
    [SerializeField]
    private TMP_Text _coinText;
    private string _coinLength = "0000000";
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
        _highScoreText.text = PlayerPrefs.GetInt("HighSchore").ToString(_highScoreLength);
        _scoreText.text = "Score: " + _scoreLength;
        _coinText.text = "Coin: " + _coinLength;
        _gameOverText.gameObject.SetActive(false);
        _getReadyText.gameObject.SetActive(true);
        _endGameText.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        StartCoroutine(GetReadyFlicker());
        StartCoroutine(ReadySequence());
        UpdateHighScore();

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

    IEnumerator UpdateScore()
    {
        while (_keepScore == true)
        {
            yield return new WaitForSeconds(.1f);
            _score++;
            _scoreText.text = "Score: " + _score.ToString(_scoreLength);
        }
    }

    public void UpdateHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (_score > PlayerPrefs.GetInt("HighScore"))
            {
                _highScore = _score;
                PlayerPrefs.SetInt("HighScore", _highScore);
                PlayerPrefs.Save();
            }
        }

        else
        {
            if (_score >= _highScore)
            {
                _highScore = _score;
                PlayerPrefs.SetInt("HighScore", _highScore);
                PlayerPrefs.Save();
            }
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
            _keepScore = false;
            GameOverSequence();
        }
    }

    public void Ready()
    {
        UpdateHighScore();
        _getReadyText.gameObject.SetActive(false);
        StartCoroutine(UpdateScore());
        StopCoroutine(GetReadyFlicker());
        StopCoroutine(ReadySequence());
    }

    void GameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        UpdateHighScore();
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
