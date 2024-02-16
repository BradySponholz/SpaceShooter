using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using static SpawnManager;

namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] int maxEnemiesT1 = 20;
        [SerializeField] float spawnIntervalT1 = .1f;
        [SerializeField] List<EnemyType> enemyTypesT1;
        [SerializeField]List<SplineContainer> splinesT1;
        int enemiesSpawnedT1;

        EnemyFactory enemyFactory;

        float spawnTimer;
        bool stopSpawning = false;

        void OnValidate()
        {
            splinesT1 = new List<SplineContainer>(collection: GetComponentsInChildren<SplineContainer>());
        }

        void Start()
        {
            enemyFactory = new EnemyFactory();
            StartCoroutine(SpawnEnemyRoutine());
        }

        void Update()
        {
            spawnTimer += Time.deltaTime;

            if (enemiesSpawnedT1 < maxEnemiesT1 && spawnTimer >= spawnIntervalT1)
            {
                if (stopSpawning == false)
                {
                    spawnTimer = 0f;
                }
            }
        }

        IEnumerator SpawnEnemyRoutine()
        {
            yield return new WaitForSeconds(5f);

            while (stopSpawning == false)
            {
                EnemyType enemyType = enemyTypesT1[Random.Range(0, 3)];
                SplineContainer spline = splinesT1[Random.Range(0, 9)];
                // TODO: Possible optimization - pool enemies
                enemyFactory.CreateEnemy(enemyType, spline);
                enemiesSpawnedT1++;
                yield return new WaitForSeconds(spawnIntervalT1);
            }
        }

        public void OnPlayerDeath()
        {
            stopSpawning = true;
            StopAllCoroutines();
        }
    }
}
