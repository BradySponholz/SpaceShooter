using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyPrefab1;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerupContainer;
    [SerializeField]
    private GameObject[] _powerups;

    private bool _stopSpawning = false;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Begin()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnEnemyRoutine1());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnDefenseRoutine());
        return;
    }

   
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 9, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(.5f);
        }

        
    }

    IEnumerator SpawnEnemyRoutine1()
    {
        yield return new WaitForSeconds(5f);

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

    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            int randomPowerup = Random.Range(1, 3);
            Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 9, 0);
            GameObject newPowerup = Instantiate(_powerups[randomPowerup], posToSpawn, Quaternion.identity);
            newPowerup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(15f);
        }
    }

    IEnumerator SpawnDefenseRoutine()
    {
        yield return new WaitForSeconds(45f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 9, 0);
            GameObject newPowerup = Instantiate(_powerups[0], posToSpawn, Quaternion.identity);
            newPowerup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(45f);
        }
    }
}
