using UnityEngine;
using Utility;

namespace Spawner
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponStrategy weaponStrategy;
        [SerializeField] protected Transform firePoint;
        [SerializeField, Layer] protected int layer;


        private void OnValidate() => layer = gameObject.layer;

        private void Start() => weaponStrategy.Initialize();

        public void SetWeaponStrategy(WeaponStrategy strategy)
        {
            weaponStrategy = strategy;
            weaponStrategy.Initialize();
        }
    }
}