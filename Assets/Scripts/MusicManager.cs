using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _introMusic;
    [SerializeField]
    private AudioSource _loopMusic;

    
    void Start()
    {
        double duration = (double)_introMusic.clip.samples / _introMusic.clip.frequency;
        double startTime = AudioSettings.dspTime + 0.5;
        _introMusic.PlayScheduled(startTime);
        _loopMusic.PlayScheduled(startTime + duration);
    }

    
    void Update()
    {
        
    }
}
