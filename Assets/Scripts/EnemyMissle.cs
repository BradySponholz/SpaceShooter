using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissle : MonoBehaviour
{
    private BoxCollider2D _collider;
    private Player _player;
    private Rigidbody2D _rb;
    private float _speed = 10f;
    private float _steer = 20f;
    private Transform _target;
    private Vector3 _targetStartPos;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        if (_collider == null)
        {
            Debug.LogError("The collider is NULL.");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        _target = GameObject.Find("Player").transform;
        _targetStartPos = _target.transform.position;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_player != null)
        {
            _rb.velocity = transform.up * _speed;
            Vector3 direction = (_targetStartPos - transform.position).normalized;
            float rotationSteer = Vector3.Cross(transform.up, direction).z;
            _rb.angularVelocity = rotationSteer * _steer * 10;

            if (transform.position == _targetStartPos | transform.position.y <= _targetStartPos.y)
            {
                _rb.angularVelocity = 0;
                transform.Translate(Vector3.up * Time.deltaTime);
            }
        }

        else
        {
            _rb.angularVelocity = 0;
            transform.Translate(Vector3.up * Time.deltaTime);
        }

        if (transform.position.y > 25 | transform.position.y < -25 | transform.position.x > 22 | transform.position.x < -22)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
                Destroy(this.gameObject);
            }
        }
    }
}
