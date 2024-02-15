using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using static SpawnManager;

namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] List<EnemyType> enemyTypes;
        [SerializeField] int maxEnemies = 100;
        [SerializeField] float spawnInterval = 2f;

        [SerializeField]List<SplineContainer> splines;
        EnemyFactory enemyFactory;

        float spawnTimer;
        int enemiesSpawned;
        bool stopSpawning = false;

        void OnValidate()
        {
            splines = new List<SplineContainer>(collection:GetComponentsInChildren<SplineContainer>());
        }

        void Start()
        {
            enemyFactory = new EnemyFactory();
            StartCoroutine(SpawnEnemyRoutine());
            StartCoroutine(SpawnEnemyRoutine2());
        }

        void Update()
        {
            spawnTimer += Time.deltaTime;

            if (enemiesSpawned < maxEnemies && spawnTimer >= spawnInterval)
            {
                if (stopSpawning == false)
                {
                    spawnTimer = 0f;
                }
            }
        }

        IEnumerator SpawnEnemyRoutine()
        {
            yield return new WaitForSeconds(10f);

            while (stopSpawning == false)
            {
                EnemyType enemyType = enemyTypes[0];
                SplineContainer spline = splines[0];
                // TODO: Possible optimization - pool enemies
                enemyFactory.CreateEnemy(enemyType, spline);
                enemiesSpawned++;
                yield return new WaitForSeconds(7);
            }
        }

        IEnumerator SpawnEnemyRoutine2()
        {
            while (stopSpawning == false)
            {
                EnemyType enemyType = enemyTypes[1];
                SplineContainer spline = splines[2];
                // TODO: Possible optimization - pool enemies
                enemyFactory.CreateEnemy(enemyType, spline);
                enemiesSpawned++;
                yield return new WaitForSeconds(1.5f);
            }
        }

        public void OnPlayerDeath()
        {
            stopSpawning = true;
            StopAllCoroutines();
        }
    }
}
