using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerupContainer;
    [SerializeField]
    private GameObject[] _powerups;

    private bool _stopSpawning = false;
    
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        //StartCoroutine(SpawnPowerupRoutine());
        //StartCoroutine(SpawnDefenseRoutine());
    }
   
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(.5f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), 28, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab[Random.Range(0, 2)], posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(45f);
        }      
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    /*IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            int randomPowerup = Random.Range(1, 4);
            Vector3 posToSpawn = new Vector3(Random.Range(-9f, 9f), 25, 0);
            GameObject newPowerup = Instantiate(_powerups[randomPowerup], posToSpawn, Quaternion.identity);
            newPowerup.transform.parent = _powerupContainer.transform;
            StopCoroutine(SpawnPowerupRoutine());
            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator SpawnDefenseRoutine()
    {
        yield return new WaitForSeconds(60f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9f, 9f), 25, 0);
            GameObject newPowerup = Instantiate(_powerups[0], posToSpawn, Quaternion.identity);
            newPowerup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(100f);
        }
    }*/
}
