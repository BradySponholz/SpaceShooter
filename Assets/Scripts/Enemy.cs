using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private int _lives = 2;
    private Player _player;
    private Animator _explosion;
    private PolygonCollider2D _collider;
    [SerializeField]
    private AudioSource _audioSource;

    void Start()
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

        if (transform.position.y < -8f)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 12, 0);
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
                _player.AddScore(10);
                ObjectDeath();
            }
        }
    }

    public void ObjectDeath()
    {
        _explosion.SetTrigger("EnemyDeath");
        _audioSource.Play();
        _collider.enabled = false;
        Destroy(this.gameObject, 0.55f);
    }
}
