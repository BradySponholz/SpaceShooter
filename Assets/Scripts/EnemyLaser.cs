using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private BoxCollider2D _collider;
    [SerializeField]
    private float _speed = 12f;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        if (_collider == null)
        {
            Debug.LogError("The collider is NULL.");
        }
    }

    void Update()
    {
        transform.position += transform.up * (_speed * Time.deltaTime);

        if (transform.position.y > 25 | transform.position.y < -25 | transform.position.x > 22 | transform.position.x < -22)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
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
                Destroy(this.gameObject);
            }
        }
    }
}
