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
    [SerializeField]
    private GameObject _powerupContainer;
    [SerializeField]
    private GameObject[] _powerups;
    private UIManager _uiManager;

    private bool _stopSpawning = false;
    
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnEnemyRoutine1());
        StartCoroutine(SpawnPowerupRoutine());
    }

    void Update()
    {
        
    }

    //Contains communication that stops GetReadyFlicker()
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(5f);
        _uiManager.Ready();

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 9, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.2f);
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
        yield return new WaitForSeconds(15f);

        while (_stopSpawning == false)
        {
            int randomPowerup = Random.Range(0, 3);
            Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 9, 0);
            GameObject newPowerup = Instantiate(_powerups[randomPowerup], posToSpawn, Quaternion.identity);
            newPowerup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(20f);
        }
    }
}
