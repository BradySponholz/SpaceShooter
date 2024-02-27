using System;
using UnityEngine;

namespace Spawner
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed;
        private Collider2D _collider;

        Transform parent;

        public void SetSpeed(float speed) => this.speed = speed;
        public void SetParent(Transform parent) => this.parent = parent;

        public Action Callback;

        private void Start()
        {
            _collider = GetComponent<Collider2D>();
            if (_collider == null)
            {
                Debug.LogError("The collider is NULL.");
            }
        }

        private void Update()
        {
            transform.SetParent(p: null);
            transform.position += transform.up * (speed * Time.deltaTime);

            Callback?.Invoke();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (tag == "Player")
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
}