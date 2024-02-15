using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashDamage : MonoBehaviour
{
    [SerializeField]
    private Material _flashMat;
    private float _duration = .02f;
    private SpriteRenderer _spriteRenderer;
    private Material _originalMat;
    private Coroutine _flashRoutine;

    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _originalMat = _spriteRenderer.material;
    }

    
    void Update()
    {
        
    }

    public void Flash()
    {
        if (_flashRoutine != null)
        {
            StopCoroutine( _flashRoutine );
        }

        _flashRoutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        _spriteRenderer.material = _flashMat;
        yield return new WaitForSeconds(_duration);
        _spriteRenderer.material = _originalMat;
        yield return new WaitForSeconds(_duration);
        _spriteRenderer.material = _flashMat;
        yield return new WaitForSeconds(_duration);
        _spriteRenderer.material = _originalMat;
        yield return new WaitForSeconds(_duration);
        _spriteRenderer.material = _flashMat;
        yield return new WaitForSeconds(_duration);
        _spriteRenderer.material = _originalMat;
        yield return new WaitForSeconds(_duration);
        _flashRoutine = null;
    }
}
