using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class StartEnemy : MonoBehaviour
{
    [SerializeField]
    private float _entrySpeed = 3.0f;
    private float _finalSpeed = 0f;
    [SerializeField]
    private int _lives = 1;
    [SerializeField]
    private GameObject[] _powerUp;
    private Player _player;
    private Animator _animate;
    private PolygonCollider2D _collider;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    [SerializeField]
    private AudioSource _audioSource;

    void Start()
    {
        transform.position = new Vector3(0, 20, 0);

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

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource is NULL.");
        }
    }
    
    void Update()
    {
        transform.Translate(Vector3.down * _entrySpeed * Time.deltaTime);
        GameStart();
    }

    void GameStart()
    {
        //if (transform.position.y < 9)
        {
            //_uiManager.Ready();
        }

        if (transform.position.y < 5.5f)
        {
            _entrySpeed = Mathf.Lerp(_entrySpeed, _finalSpeed, Time.deltaTime);
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
                _collider.enabled = false;
                _animate.SetTrigger("EnemyDeath");
                _audioSource.Play();
                StartCoroutine(ObjectDeath());
            }
        }
    }

    IEnumerator ObjectDeath()
    {
        yield return new WaitForSeconds(.25f);
        //_spawnManager.Begin();
        Destroy(this.gameObject, 0.55f);
    }
}
