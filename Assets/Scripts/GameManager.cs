using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    [SerializeField]
    private Animator _transition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            StartCoroutine(StartOver());
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    IEnumerator StartOver()
    {
        _transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(0);
    }
}
