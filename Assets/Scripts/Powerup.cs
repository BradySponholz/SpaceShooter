using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

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
                if (_powerupID == 0)
                {
                    player.DoubleShotActive();
                }
                else if (_powerupID == 1)
                {
                    player.SpeedShotActive();
                }
                else if (_powerupID == 2)
                {
                    player.ShieldActive();
                }
            }
            Destroy(this.gameObject);
        }
    }
}
