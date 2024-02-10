using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _lives = 5;
    private Player _player;
    private Animator _explosion;
    private PolygonCollider2D _collider;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _enemyShot;
    [SerializeField]
    private float _fireRate = 2.2f;
    private float _nextFire = -1f;
    [SerializeField]
    private GameObject _coin;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        _explosion = GetComponent<Animator>();
        if (_explosion == null)
        {
            Debug.LogError("The animator is NULL.");
        }

        _collider = GetComponent<PolygonCollider2D>();
        if (_collider == null)
        {
            Debug.LogError("The collider is NULL.");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource is NULL.");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < 17f && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            Instantiate(_enemyShot, transform.position, Quaternion.identity);
        }

        if (transform.position.y < -25f)
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
                ObjectDeath();
            }
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Damage();
        }
    }

    public void Damage()
    {
        _lives--;

        if (_lives < 1)
        {
            if (_player != null)
            {
                ObjectDeath();
            }
        }
    }

    public void ObjectDeath()
    {
        _explosion.SetTrigger("EnemyDeath");
        _audioSource.Play();
        _collider.enabled = false;
        Instantiate(_coin, transform.position, Quaternion.identity);
        Destroy(this.gameObject, 0.55f);
    }
}
