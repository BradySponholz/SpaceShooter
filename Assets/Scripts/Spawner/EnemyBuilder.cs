using UnityEngine;
using UnityEngine.Splines;
using Utility;

namespace Spawner
{
    public class EnemyBuilder
    {
        public GameObject enemyPrefab;
        public SplineContainer splineContainer;
        public GameObject weaponPrefab;
        public float speed;

        public EnemyBuilder SetBasePrefab(GameObject prefab)
        {
            enemyPrefab = prefab;
            return this;
        }

        public EnemyBuilder SetSpline(SplineContainer spline)
        {
            this.splineContainer = spline;
            return this;
        }

        public EnemyBuilder SetWeaponPrefab(GameObject prefab)
        {
            weaponPrefab = prefab;
            return this;
        }

        public EnemyBuilder SetSpeed(float speed)
        {
            this.speed = speed;
            return this;
        }

        public GameObject Build()
        {
            GameObject instance = GameObject.Instantiate(enemyPrefab);

            SplineAnimate splineAnimate = instance.GetOrAdd<SplineAnimate>();
            splineAnimate.Container = splineContainer;
            splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
            splineAnimate.ObjectUpAxis = SplineAnimate.AlignAxis.ZAxis; //ZAxis
            splineAnimate.ObjectForwardAxis = SplineAnimate.AlignAxis.YAxis; //YAxis
            splineAnimate.MaxSpeed = speed;

            // Weapon in P3

            instance.transform.position = splineContainer.EvaluatePosition(0f);

            return instance;
        }
    }
}
