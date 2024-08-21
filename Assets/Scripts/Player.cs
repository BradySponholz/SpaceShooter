using Spawner;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20f;
    [SerializeField]
    private GameObject[] _shotType;
    [SerializeField]
    private GameObject[] _missileType;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private float _fireRate = .4f;
    private float _nextFire = .4f;
    private float _fireSpeedBoost = 1.1f;
    [SerializeField]
    private float _fireRateMissile = 1.7f;
    private float _nextFireMissile = 1.7f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private EnemySpawner _enemySpawner;
    private bool _isSpeedShotActive = false;
    private bool _isShieldActive = false;
    private bool _isMissileActive = false;
    private int _shotCount = 0;
    private int _speedCount = 0;
    private int _missleCount = 0;
    [SerializeField]
    private int _coins;
    private UIManager _uiManager;
    private Animator _animator;
    private PolygonCollider2D _collider;
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private GameObject _explosionHit;
    [SerializeField]
    private AudioClip _laserClip;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private InputActionReference _moveActionToUse;


    void Start()
    {
        transform.position = new Vector3(0, -11, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        _enemySpawner = GameObject.Find("Enemy_Spawner").GetComponent<EnemySpawner>();
        if (_enemySpawner == null)
        {
            Debug.LogError("The Enemy Spawner is NULL.");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is Null.");
        }

        _animator = GetComponent<Animator>();
        if (_animator == null)
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
        else
        {
            _audioSource.clip = _laserClip;
        }
    }

    void Update()
    {
        CalculateMovement();
        AutoFire();
        MissileFire();
    }

    // Player movement and boundry limits
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -16f, 17.5f), 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -13f, 13f), transform.position.y, 0);
    }

    //Autofire
    void AutoFire()
    {
        if (Time.time > _nextFire)
        {
            //LaserShot
            if (_shotCount > 3)
            {
                Instantiate(_shotType[4], transform.position, Quaternion.identity);
            }
            else if (_shotCount == 3)
            {
                Instantiate(_shotType[3], transform.position, Quaternion.identity);
            }
            else if (_shotCount == 2)
            {
                Instantiate(_shotType[2], transform.position, Quaternion.identity);
            }
            else if (_shotCount == 1)
            {
                Instantiate(_shotType[1], transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_shotType[0], transform.position, Quaternion.identity);
            }

            //SpeedShotActive
            if (_isSpeedShotActive == true)
            {
                _nextFire = Time.time + _fireRate / _fireSpeedBoost / _speedCount;
            }
            else
            {
                _nextFire = Time.time + _fireRate;
            }
            _audioSource.Play();
        }
    }

    void MissileFire()
    {
        if (Time.time > _nextFireMissile && _isMissileActive == true)
        {
            if (_missleCount > 2)
            {
                Instantiate(_missileType[2], transform.position, Quaternion.identity);
            }

            else if (_missleCount > 1)
            {
                Instantiate(_missileType[1], transform.position, Quaternion.identity);
            }

            else
            {
                Instantiate(_missileType[0], transform.position, Quaternion.identity);
            }

            if (_isSpeedShotActive == true)
            {
                _nextFireMissile = Time.time + _fireRateMissile / _fireSpeedBoost / _speedCount;
            }
            else
            {
                _nextFireMissile = Time.time + _fireRateMissile;
            }
            _audioSource.Play();
        }
    }

    //Damage behavior
    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shield.SetActive(false);
            Instantiate(_explosionHit, transform.position, Quaternion.identity);
            return;
        }

        _lives--;
        _shotCount = 0;
        _speedCount = 0;
        _isSpeedShotActive = false;
        Instantiate(_explosionHit, transform.position, Quaternion.identity);

        _uiManager.UpdateLife(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            _enemySpawner.OnPlayerDeath();
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _collider.enabled = false;
            Destroy(this.gameObject);
        }
    }

    //Powerup behaviors
    public void DoubleShotActive()
    {
        _shotCount++;
    }

    public void SpeedShotActive()
    {
        _speedCount++;
        _isSpeedShotActive = true;
    }

    public void MissleActive()
    {
        _missleCount++;
        _isMissileActive = true;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shield.SetActive(true);
    }

    public void AddCoins(int coins)
    {
        _coins += coins;
        _uiManager.UpdateCoins(_coins);
    }
}
