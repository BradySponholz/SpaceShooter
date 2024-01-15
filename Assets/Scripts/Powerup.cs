using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float _speed = 2.0f;

    [SerializeField] //0 = DoubleShot, 1 = SpeedShot, 2 = Shields
    private int _powerupID;


    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8f)
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
                switch (_powerupID)
                {
                    case 0:
                        player.DoubleShotActive();
                        break;
                    case 1:
                        player.SpeedShotActive();
                        break;
                    case 2:
                        player.ShieldActive();
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
