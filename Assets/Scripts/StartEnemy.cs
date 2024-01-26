using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

public class StartEnemy : MonoBehaviour
{
    [SerializeField]
    private float _entrySpeed = 3.0f;
    [SerializeField]
    private float _sideScrollSpeed = 1.5f;
    private bool _direction = true;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private GameObject[] _powerUp;
    private Player _player;
    private Animator _animate;
    private PolygonCollider2D _collider;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private float _position;

    void Start()
    {
        transform.position = new Vector3(0, 25, 0);
        
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        _animate = GetComponent<Animator>();
        if (_animate == null)
        {
            Debug.LogError("The animator is NULL.");
        }

        _collider = GetComponent<PolygonCollider2D>();
        if (_collider == null)
        {
            Debug.LogError("The collider is NULL.");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is NULL.");
        }

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The SpawnManager in NULL.");
        }
    }

    
    void Update()
    {
        transform.Translate(Vector3.down * _entrySpeed * Time.deltaTime);
        GameStart();

        _position = transform.position.x;
        if (transform.position.x < _position)
        {
            _animate.SetBool("MvLeft", true);
        }

        else if (transform.position.x > _position)
        {
            _animate.SetBool("MvRight", true);
        }

        else if (transform.position.x == _position)
        {
            _animate.SetBool("MvLeft", false);
            _animate.SetBool("MvRight", false);
        }
    }

    void GameStart()
    {
        if (transform.position.y < 9)
        {
            _uiManager.Ready();
        }

        if (transform.position.y < 4.5f)
        {
            _entrySpeed = 0;
            SideToSide();
        }
    }

    void SideToSide()
    {
        if (_direction == true)
        {
            transform.Translate(Vector3.left * _sideScrollSpeed * Time.deltaTime);
            if (transform.position.x < -2)
            {
                _sideScrollSpeed = 0;
                _direction = false;
                _sideScrollSpeed = 1.5f;
            }
        }

        else if (_direction == false)
        {
            transform.Translate(Vector3.right * _sideScrollSpeed * Time.deltaTime);
            if (transform.position.x > 2)
            {
                _sideScrollSpeed = 0;
                _direction = true;
                _sideScrollSpeed = 1.5f;
            }
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
                _player.AddScore(100);
                ObjectDeath();
            }
        }
    }

    public void ObjectDeath()
    {
        _animate.SetTrigger("EnemyDeath");
        _collider.enabled = false;
        _spawnManager.Begin();
        Destroy(this.gameObject, 0.55f);
    }
}
