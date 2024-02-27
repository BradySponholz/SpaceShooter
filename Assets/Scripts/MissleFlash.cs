using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleFlash : MonoBehaviour
{
    [SerializeField]
    private Material _flashMat;
    [SerializeField]
    private Material _redMat;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        while (true)
        {
            _spriteRenderer.material = _flashMat;
            yield return new WaitForSeconds(.04f);
            _spriteRenderer.material = _redMat;
            yield return new WaitForSeconds(.04f);
        }
    }
}
