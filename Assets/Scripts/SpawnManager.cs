using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyPrefab1;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;
    
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnRoutine1());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 9, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.2f);
        }

        
    }

    IEnumerator SpawnRoutine1()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 9, 0);
            GameObject newEnemy1 = Instantiate(_enemyPrefab1, posToSpawn, Quaternion.identity);
            newEnemy1.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3f);
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
