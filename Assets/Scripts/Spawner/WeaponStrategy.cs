using UnityEngine;

namespace Spawner
{
    public abstract class WeaponStrategy : ScriptableObject
    {
        [SerializeField] int damage = 1;
        [SerializeField] float fireRate = 1f;
        [SerializeField] protected float projectileSpeed = 10f;
        [SerializeField] protected float projectileLifetime = 4f;
        [SerializeField] protected GameObject projectilePrefab;

        public int Damage => damage;
        public float FireRate => fireRate;

        public virtual void Initialize()
        {

        }

        public abstract void Fire(Transform firePoint, LayerMask layer);
    }
}