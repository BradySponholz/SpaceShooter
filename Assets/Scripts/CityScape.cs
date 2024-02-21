using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityScape : MonoBehaviour
{
    private float speed = 1.9f;
    [SerializeField]
    private float end;
    [SerializeField]
    private float start;
    private float _time = 1f;
    private float _gameSpeed = .05f;
    public bool keepPlaying = true;

    private void Start()
    {
        StartCoroutine(SpeedIncrease());
    }

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y <= end)

        {
            Vector3 pos = new Vector3(0, start, 5);
            transform.position = pos;
        }
    }

    IEnumerator SpeedIncrease()
    {
        while (keepPlaying == true)
        {
            yield return new WaitForSeconds(30f);
            _time++;
            speed = 1.3f + (_time * _gameSpeed);
        }
    }
}
