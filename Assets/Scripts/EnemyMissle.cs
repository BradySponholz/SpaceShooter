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

    /*private void FixedUpdate()
    {
        if (_target != null)
        {
            Vector3 _direction = _playerStartPos - (Vector3)_rb.position;
            _direction.Normalize();
            Vector3 transformDown = transform.up * -1;
            float rotateAmount = Vector3.Cross(_direction, transformDown).z;
            _rb.angularVelocity = -rotateAmount * _rotateSpeed;
            _rb.velocity = transformDown * _speed;
        }
        else
        {
            _rb.velocity = _direction * _speed * Time.deltaTime;
            _rb.angularVelocity = 0;
        }

    Vector3 _direction = _playerStartPos - (Vector3)_rb.position;
            _direction.Normalize();
            Vector3 transformDown = transform.up * -1;
            float rotateAmount = Vector3.Cross(_direction, transformDown).z;
            _rb.angularVelocity = -rotateAmount * _rotateSpeed;
            _rb.velocity = transformDown * _speed;
            transform.position = Vector3.MoveTowards(transform.position, _playerStartPos, _speed * Time.deltaTime);

    }

    void FixedUpdate()
    {
        if (_player != null)
        {
            Vector2 direction = _rb.position - _playerStartPos;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, _rb.transform.up).z;
            _rb.angularVelocity = -rotateAmount * _rotateSpeed;
            _rb.velocity = transform.up * _speed;
            Quaternion rotation = Quaternion.LookRotation(forward: direction, upwards: Vector3.up);
            transform.rotation = Quaternion.Slerp(a: transform.rotation, b: rotation, t: _rotateSpeed * Time.deltaTime);

            if ((Vector2)transform.position == _playerStartPos)
            {
                transform.Translate(_rb.transform.up * _speed * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(_rb.transform.up *_speed * Time.deltaTime);
        }

        if (transform.position.y > 25 | transform.position.y < -25 | transform.position.x > 22 | transform.position.x < -22)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }*/

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


    /*private void FixedUpdate()
    {
        if (_isTracking == true)
        {
            _rb.velocity = transform.up * _speed;
            Vector3 direction = (_target.position - transform.position).normalized;
            float rotationSteer = Vector3.Cross(transform.up, direction).z;
            _rb.angularVelocity = rotationSteer * _steer * 10;
        }

        else
        {
            transform.position += transform.up * _speed;
        }
    }*/

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
