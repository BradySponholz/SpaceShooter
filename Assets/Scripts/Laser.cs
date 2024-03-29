using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 18.0f;

    
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 24.5f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
