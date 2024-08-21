using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmExplosion : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource is NULL.");
        }
        _audioSource.Play();
        StartCoroutine(DestroyExplosion());
    }

    IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(.55f);
        Destroy(this.gameObject);
    }
}
