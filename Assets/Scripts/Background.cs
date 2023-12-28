using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private float _speed = .2f;
    private MeshRenderer _renderer;

    
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    
    void Update()
    {
        _renderer.material.mainTextureOffset = new Vector2(0f, Time.time * _speed);
    }
}
