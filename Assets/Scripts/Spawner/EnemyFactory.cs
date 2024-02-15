using UnityEngine;
using UnityEngine.Splines;

namespace Spawner
{
    public class EnemyFactory
    {
        public GameObject CreateEnemy(EnemyType enemyType, SplineContainer spline)
        {
            EnemyBuilder builder = new EnemyBuilder()
                .SetBasePrefab(enemyType.enemyPrefab)
                .SetSpline(spline)
                .SetSpeed(enemyType.speed);

            //Weapon in P3

            return builder.Build();
        }
    }
}
