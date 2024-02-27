using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2Enemy : MonoBehaviour
{
    [SerializeField]
    private int _lives = 5;
    private Player _player;
    private Animator _explosion;
    private PolygonCollider2D _collider;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _coin;
    private UIManager _uiManager;
    private FlashDamage _flash;
    [SerializeField]
    private GameObject _enemyLaser;
    [SerializeField]
    private GameObject _enemyMissle;
    private int _startFire = 0;
    private int _missleCount = 0;
    private int _laserCount = 0;
    private bool _stopSpawning = false;

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

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is NULL");
        }

        _flash = GetComponent<FlashDamage>();
    }

    private void Update()
    {
        if (transform.position.y < 17f && _startFire < 1)
        {
            StartCoroutine(FireLaser());
            StartCoroutine(FireMissle());
            _startFire++;
        }


        if (transform.position.y < -25f)
        {
            Destroy(this.gameObject);
        }

        if (_player == null)
        {
            _stopSpawning = true;
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

    IEnumerator FireLaser()
    {
        while (_stopSpawning == false && _laserCount < 5)
        {
            Instantiate(_enemyLaser, transform.position, transform.rotation);
            _laserCount++;
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator FireMissle()
    {
        yield return new WaitForSeconds(1);

        while (_stopSpawning == false && _missleCount < 2)
        {
            Instantiate(_enemyMissle, transform.position, transform.rotation);
            _missleCount++;
            yield return new WaitForSeconds(3f);
        }
    }

    public void ObjectDeath()
    {
        _stopSpawning = true;
        _explosion.SetTrigger("EnemyDeath");
        _audioSource.Play();
        _collider.enabled = false;
        _uiManager.IncreaseScore(500);
        Instantiate(_coin, transform.position, Quaternion.identity);
        Destroy(this.gameObject, 0.55f);
    }
}
