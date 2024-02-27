using Unity.VisualScripting;
using UnityEngine;
using Utility;
using System;

namespace Spawner
{
    [CreateAssetMenu(fileName = "PlayerTrackingShot", menuName = "Spawner/WeaponStrategy/PlayerTrackingShot")]
    public class TrackingShot : WeaponStrategy
    {
        [SerializeField] float trackingSpeed = 1.0f;

        Transform target;


        public override void Initialize()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }


        public override void Fire(Transform firePoint, LayerMask layer)
        {
            var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.transform.SetParent(firePoint);
            projectile.layer = (int)layer;

            var projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.SetSpeed(projectileSpeed);
            projectile.GetComponent<Projectile>().Callback = () =>
            {
                Vector3 direction = (target.position - projectile.transform.position).With(z: firePoint.position.z).normalized;

                Quaternion rotation = Quaternion.LookRotation(forward: direction, upwards: Vector3.forward);
                projectile.transform.rotation = Quaternion.Slerp(a: projectile.transform.rotation, b: rotation, t: trackingSpeed * Time.deltaTime);
            };

            Destroy(projectile, projectileLifetime);
        }
    }
}