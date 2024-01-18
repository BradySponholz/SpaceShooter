using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _doubleShot;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private float _fireRate = .3f;
    private float _nextFire = .3f;
    [SerializeField]
    private float _fireSpeedBoost = 2.5f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _isDoubleShotActive = false;
    private bool _isSpeedShotActive = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;


    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is Null.");
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
            if (_isDoubleShotActive == true)
            {
                Instantiate(_doubleShot, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            }

            if (_isSpeedShotActive == true)
            {
                _nextFire = Time.time + _fireRate / _fireSpeedBoost;
            }
            else
            {
                _nextFire = Time.time + _fireRate;
            }
        }
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shield.SetActive(false);
            return;
        }

        _lives--;
        _isDoubleShotActive = false;
        _isSpeedShotActive = false;

        _uiManager.UpdateLife(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void DoubleShotActive()
    {
        _isDoubleShotActive = true;
    }

    public void SpeedShotActive()
    {
        _isSpeedShotActive = true;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shield.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
