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
    private string _scoreLength = "00000000";
    [SerializeField]
    private TMP_Text _highScoreText;
    private string _highScoreLength = "00000000";
    [SerializeField]
    private TMP_Text _coinText;
    private string _coinLength = "000000";
    //[SerializeField]
    //private Image _lifeImage;
    //[SerializeField]
    //private Sprite[] _lifeSprites;
    [SerializeField]
    private TMP_Text _gameOverText;
    [SerializeField]
    private TMP_Text _getReadyText;
    [SerializeField]
    private TMP_Text _endGameText;
    [SerializeField]
    private Button _restartButton;
    private GameManager _gameManager;
    private MusicManager _gameMusic;
    //private float _time = 1f;
    //private float _gameSpeed = .05f;
    private float _gameRate = 1f;


    void Start()
    {
        _highScoreText.text = PlayerPrefs.GetInt("HighSchore").ToString(_highScoreLength);
        _scoreText.text = "" + _scoreLength;
        _coinText.text = "" + _coinLength;
        _gameOverText.gameObject.SetActive(false);
        _getReadyText.gameObject.SetActive(true);
        _endGameText.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _gameMusic = GameObject.Find("Background_Audio").GetComponent<MusicManager>();

        StartCoroutine(GetReadyFlicker());
        StartCoroutine(ReadySequence());
        StartCoroutine(GameScoreSpeed());
        UpdateHighScore();
        _gameMusic.GameStart();

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

    public void PauseGame()
    {
        if (_keepScore == true)
        {
            _endGameText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        _endGameText.gameObject.SetActive(false);
        StartCoroutine(ReturnToGame());
    }

    public void IncreaseScore(int points)
    {
        if (_keepScore == true)
        {
            _score += points;
            _scoreText.text = "" + points.ToString(_scoreLength);
        }
    }

    IEnumerator UpdateScore()
    {
        while (_keepScore == true)
        {
            yield return new WaitForSeconds(.05f);
            _score++;
            _scoreText.text = "" + _score.ToString(_scoreLength);
        }
    }

    IEnumerator UpdateScore2()
    {
        while (_keepScore == true)
        {
            yield return new WaitForSeconds(.035f);
            _score++;
            _scoreText.text = "" + _score.ToString(_scoreLength);
        }
    }

    IEnumerator UpdateScore3()
    {
        while (_keepScore == true)
        {
            yield return new WaitForSeconds(.02f);
            _score++;
            _scoreText.text = "" + _score.ToString(_scoreLength);
        }
    }

    IEnumerator UpdateScore4()
    {
        while (_keepScore == true)
        {
            yield return new WaitForSeconds(.01f);
            _score++;
            _scoreText.text = "" + _score.ToString(_scoreLength);
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
        _coinText.text = "" + playerCoins.ToString(_coinLength);
    }

    public void UpdateLife(int currentLife)
    {
        //_lifeImage.sprite = _lifeSprites[currentLife];

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
        _gameMusic.GameOver();
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
        yield return new WaitForSeconds(2f);
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

    IEnumerator ReturnToGame()
    {
        Time.timeScale = .75f;
        _gameMusic.BackToGame();
        _getReadyText.gameObject.SetActive(true);
        _getReadyText.text = "3";
        yield return new WaitForSeconds(0.375f);
        _getReadyText.text = "";
        yield return new WaitForSeconds(0.375f);
        _getReadyText.text = "2";
        yield return new WaitForSeconds(0.375f);
        _getReadyText.text = "";
        yield return new WaitForSeconds(0.375f);
        _getReadyText.text = "1";
        yield return new WaitForSeconds(0.375f);
        _getReadyText.text = "";
        yield return new WaitForSeconds(0.375f);
        _getReadyText.gameObject.SetActive(false);
        Time.timeScale = _gameRate;
    }

    IEnumerator ReadySequence()
    {
        yield return new WaitForSeconds(3.5f);
        Ready();
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator GameScoreSpeed()
    {
        while (_keepScore == true)
        {
            yield return new WaitForSeconds(300);
            StopCoroutine(UpdateScore());
            StartCoroutine(UpdateScore2());
            yield return new WaitForSeconds(300);
            StopCoroutine(UpdateScore2());
            StartCoroutine(UpdateScore3());
            yield return new WaitForSeconds(300);
            StopCoroutine(UpdateScore3());
            StartCoroutine(UpdateScore4());
            yield return new WaitForSeconds(300);
            StartCoroutine(UpdateScoreRepeat());
            StopCoroutine(GameScoreSpeed());
        }
    }

    IEnumerator UpdateScoreRepeat()
    {
        while (_keepScore == true)
        {
            yield return new WaitForSeconds(300);
            StartCoroutine(UpdateScore());
        }
    }
}
