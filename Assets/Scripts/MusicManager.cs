using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _introMusic;
    [SerializeField]
    private AudioSource _loopMusic;
    private float _pitch = 1f;
    private float _slowPitch = .9f;
    private float _time = 1f;
    private float _gameSpeed = .001f;
    private bool _isPlaying = false;


    void Start()
    {
        double duration = (double)_introMusic.clip.samples / _introMusic.clip.frequency;
        double startTime = AudioSettings.dspTime + 0.5;
        _introMusic.PlayScheduled(startTime);
        _loopMusic.PlayScheduled(startTime + duration);
    }

    public void GameStart()
    {
        _isPlaying = true;
        StartCoroutine(GameSpeed());
    }

    public void BackToGame()
    {
        StartCoroutine(UnPause());
    }

    public void GameOver()
    {
        StopCoroutine(GameSpeed());
    }

    IEnumerator UnPause()
    {
        _isPlaying = false;
        _loopMusic.pitch = _slowPitch;
        yield return new WaitForSeconds(2.25f);
        _isPlaying = true;
        _loopMusic.pitch = _pitch;
    }

    IEnumerator GameSpeed()
    {
        yield return new WaitForSeconds(16f);

        while (_isPlaying == true)
        {
            yield return new WaitForSeconds(30f);
            _time++;
            _pitch = 1 + (_time * _gameSpeed);
            _loopMusic.pitch = _pitch;
        }
    }
}
