using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Image _timerFill;
    [SerializeField]
    private Image _shieldIcon;
    private float _time = 29.5f;
    private float _remainingTime;
    private float _timeRemoved = .1f;


    void Start()
    {
        TimeStart(_time);
    }

    
    void Update()
    {

    }

    public void TimeStart(float Second)
    {
        _remainingTime = Second;
        StartCoroutine(UpdateTime());
    }

    IEnumerator UpdateTime()
    {
        while (_remainingTime >= 0)
        {
            _timerFill.fillAmount = Mathf.InverseLerp(0f, _time, _remainingTime);
            _remainingTime -= _timeRemoved;
            yield return new WaitForSeconds(.1f);
        }
        TimeOver();
    }
    private void TimeOver()
    {
        Destroy(this.gameObject);
    }
}
