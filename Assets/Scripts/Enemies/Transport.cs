using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _lives = 10;
    private Player _player;
    private PolygonCollider2D _collider;
    private UIManager _uiManager;
    private FlashDamage _flash;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private GameObject[] _powerups;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        _collider = GetComponent<PolygonCollider2D>();
        if (_collider == null)
        {
            Debug.LogError("The collider is NULL.");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is NULL");
        }


        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource is NULL.");
        }

        _flash = GetComponent<FlashDamage>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

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

        if (other.tag == "Missile")
        {
            Destroy(other.gameObject);
            _audioSource.Play();
            Damage();
        }
    }

    public void Damage()
    {
        _lives--;

        if (_lives > 0)
        {
            _flash.Flash();
        }

        else
        {
            if (_player != null)
            {
                ObjectDeath();
            }
        }
    }

    public void ObjectDeath()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Instantiate(_powerups[Random.Range(0, 4)], transform.position, transform.rotation);
        _collider.enabled = false;
        _uiManager.IncreaseScore(200);
        Destroy(this.gameObject);
    }
}
