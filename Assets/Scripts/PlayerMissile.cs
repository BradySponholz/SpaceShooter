using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    private BoxCollider2D _collider;
    private Rigidbody2D _rb;
    private float _speed = 15f;
    private float _steer = 30f;
    private GameObject[] _enemy;
    private float _distanceToClosestEnemy;
    private GameObject _closestEnemy;
    private bool _closestEnemyFound = false;
    //[SerializeField]
    //private AudioSource _audioSource;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        if (_collider == null)
        {
            Debug.LogError("The collider is NULL.");
        }

        /*_audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource is NULL.");
        }*/

        _enemy = GameObject.FindGameObjectsWithTag("Enemy");
        _rb = GetComponent<Rigidbody2D>();

        _distanceToClosestEnemy = Mathf.Infinity;
        _closestEnemy = null;
    }

    private void Update()
    {
        HomingMissile();
        MoveTowardsEnemy();

        if (transform.position.y > 25 | transform.position.y < -25 | transform.position.x > 22 | transform.position.x < -22)
        {
            Destroy(this.gameObject);
        }
        /*if (_enemy != null)
        {
            _rb.velocity = transform.up * _speed;
            Vector3 direction = (_enemy.transform.position - transform.position).normalized;
            float rotationSteer = Vector3.Cross(transform.up, direction).z;
            _rb.angularVelocity = rotationSteer * _steer * 10;

            if (/*transform.position.y >= _enemy.transform.position.y | _enemy == null)
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
        }*/
    }

    private void HomingMissile()
    {
        _enemy = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in _enemy)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(_rb.transform.position, enemy.transform.position);

                if (distanceToEnemy < _distanceToClosestEnemy)
                {
                    _distanceToClosestEnemy = distanceToEnemy;
                    _closestEnemy = enemy;
                    _closestEnemyFound = true;
                }
            }
        }
    }

    private void MoveTowardsEnemy()
    {
        if (_closestEnemy != null)
        {
            if (_closestEnemyFound == true)
            {
                Vector3 direction = _closestEnemy.transform.position - _rb.transform.position;
                direction.Normalize();
                float rotationSteer = Vector3.Cross(transform.up, direction).z;
                _rb.angularVelocity = rotationSteer * _steer * 10;
                _rb.velocity = transform.up * _speed;
            }
        }

        else
        {
            _rb.angularVelocity = 0;
            _rb.velocity = transform.up * _speed;
        }
    }
}
