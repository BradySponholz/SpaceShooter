using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Animator _transition;

    public void LoadGame()
    {
        StartCoroutine(SceneSwitch());
    }

    IEnumerator SceneSwitch()
    {
        _transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(1);

    }
}
