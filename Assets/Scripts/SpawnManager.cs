using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject _powerUpPrefab;
    [SerializeField]
    private GameObject _speedPowerUpPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnTripleShotPowerUpRoutine());
       //StartCoroutine(SpawnSpeedPowerUpRoutine()); refazer igual do curso
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-11.0f, 11.0f), 7.0f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab,posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(3.0f, 8.0f)); //chance later
        }
    }

    IEnumerator SpawnTripleShotPowerUpRoutine()
    {
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-11.0f, 11.0f), 7.0f, 0);
            GameObject newPowerUp = Instantiate(_powerUpPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(7.0f);
        }
    }
    /*
    IEnumerator SpawnSpeedPowerUpRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-10.0f, 10.0f), 7.0f, 0);
            GameObject newPowerUp = Instantiate(_speedPowerUpPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(12.0f);
        }
    }
    */

    public void IsPlayerDeath()
    {
        _stopSpawning = true;
    }
}
