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
    public float fireRate = .25f;
    private float _nextFire = .25f;
    private float _playerTime = 0.0f;
    [SerializeField]
    private int _lives = 5;
    private SpawnManager _spawnManager;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

    }

    void Update()
    {
        CalculateMovement();
        AutoFire();
    }

    //Autofire on MouseButton Hold
    void AutoFire()
    {
        _playerTime = _playerTime + Time.deltaTime;

        if (Input.GetButton("Fire1") && _playerTime > _nextFire)
        {
            _nextFire = _playerTime + fireRate;
            
            Instantiate(_laserPrefab, transform.position + new Vector3(0, .85f, 0), Quaternion.identity);

            _nextFire = _nextFire - _playerTime;
            _playerTime = 0.0F;
        }
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

    public void Damage()
    {
        _lives--;

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
