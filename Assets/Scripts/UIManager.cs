using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        
        _scoreText.text = "Score: " + _scoreLength;
    }

    
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString(_scoreLength);
    }

    public void UpdateLife(int currentLife)
    {
        _lifeImage.sprite = _lifeSprites[currentLife];
    }
}
