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
        [SerializeField] float spawnIntervalT1 = .75f;
        [SerializeField] List<EnemyType> enemyTypesT1;
        int enemiesSpawnedT1;

        [SerializeField] int maxEnemiesT2 = 10;
        [SerializeField] float spawnIntervalT2 = 4f;
        [SerializeField] List<EnemyType> enemyTypesT2;
        int enemiesSpawnedT2;

        [SerializeField] List<SplineContainer> splines;
        EnemyFactory enemyFactory;

        float spawnTimer;
        bool stopSpawning = false;

        void OnValidate()
        {
            splines = new List<SplineContainer>(collection: GetComponentsInChildren<SplineContainer>());
        }

        void Start()
        {
            enemyFactory = new EnemyFactory();
            StartCoroutine(SpawnEnemyRoutine1());
            StartCoroutine(SpawnEnemyRoutine2());
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

            if (enemiesSpawnedT2 < maxEnemiesT2 && spawnTimer >= spawnIntervalT2)
            {
                if (stopSpawning == false)
                {
                    spawnTimer = 0f;
                }
            }
        }

        IEnumerator SpawnEnemyRoutine1()
        {
            yield return new WaitForSeconds(1f);

            while (stopSpawning == false)
            {
                EnemyType enemyType = enemyTypesT1[Random.Range(0, 3)];
                SplineContainer spline = splines[Random.Range(0, 9)];
                // TODO: Possible optimization - pool enemies
                enemyFactory.CreateEnemy(enemyType, spline);
                enemiesSpawnedT1++;
                yield return new WaitForSeconds(spawnIntervalT1);
            }
        }

        IEnumerator SpawnEnemyRoutine2()
        {
            yield return new WaitForSeconds(20f);

            while (stopSpawning == false)
            {
                EnemyType enemyType = enemyTypesT2[Random.Range(0, 3)];
                SplineContainer spline = splines[Random.Range(10, 13)];
                // TODO: Possible optimization - pool enemies
                enemyFactory.CreateEnemy(enemyType, spline);
                enemiesSpawnedT2++;
                yield return new WaitForSeconds(spawnIntervalT2);
            }
        }

        public void OnPlayerDeath()
        {
            stopSpawning = true;
            StopAllCoroutines();
        }
    }
}
