using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private float _speed = 3.6f;
    [SerializeField]
    private int _coinID;
    [SerializeField]
    private AudioClip _coinClip;

    // Update is called once per frame
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

            AudioSource.PlayClipAtPoint(_coinClip, transform.position);

            if (player != null)
            {
                switch (_coinID)
                {
                    case 0:
                        player.AddCoins(5);
                        break;
                    case 1:
                        player.AddCoins(10);
                        break;
                    case 2:
                        player.AddCoins(50);
                        break;
                    default:
                        Debug.Log("default Value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
