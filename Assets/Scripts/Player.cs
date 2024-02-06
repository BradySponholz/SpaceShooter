using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject[] _shotType;
    [SerializeField]
    private GameObject[] _timer;
    private GameObject _sTimer;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private float _fireRate = .3f;
    private float _nextFire = .3f;
    private float _fireSpeedBoost = 1.1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _isSpeedShotActive = false;
    private bool _isShieldActive = false;
    private int _shotCount = 0;
    private int _speedCount = 0;
    [SerializeField]
    private int _coins;
    private UIManager _uiManager;
    private Animator _explosion;
    private PolygonCollider2D _collider;
    [SerializeField]
    private AudioClip _laserClip;
    [SerializeField]
    private AudioClip _explosionClip;
    [SerializeField]
    private AudioClip _damageClip;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private InputActionReference _moveActionToUse;


    void Start()
    {
        transform.position = new Vector3(0, -3, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is Null.");
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
        else
        {
            _audioSource.clip = _laserClip;
        }


    }

    void Update()
    {
        CalculateMovement();
        AutoFire();
    }

    // Player movement and boundry limits
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.5f, 5), 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9, 9), transform.position.y, 0);
    }

    //Autofire on MouseButton Hold
    void AutoFire()
    {
        if (Input.GetButton("Fire1") && Time.time > _nextFire)
        {
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

    private void LoadTimer()
    {
        _sTimer = Instantiate(_timer[0]);
    }

    //Damage behavior
    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shield.SetActive(false);
            Destroy(_sTimer);
            return;
        }

        _lives--;
        _shotCount = 0;
        _speedCount = 0;
        _isSpeedShotActive = false;
        AudioSource.PlayClipAtPoint(_damageClip, transform.position);

        _uiManager.UpdateLife(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            _explosion.SetTrigger("PlayerDeath");
            AudioSource.PlayClipAtPoint(_explosionClip, transform.position);
            _collider.enabled = false;
            Destroy(this.gameObject, 0.55f);
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

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shield.SetActive(true);
        LoadTimer();
        StartCoroutine(ShieldPowerDown());
    }

    public void AddCoins(int coins)
    {
        _coins += coins;
        _uiManager.UpdateCoins(_coins);
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(30f);
        _isShieldActive = false;
        _shield.SetActive(false);
    }
}
