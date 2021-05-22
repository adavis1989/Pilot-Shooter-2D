using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{

    [SerializeField]
    private GameObject _Enemyprefab;
    [SerializeField]
    private GameObject _EnemyContainer;
    private bool _StopSpawning = false;
    [SerializeField]
    private GameObject[] _powerups;



    




    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRountine());
        StartCoroutine(SpawnPowerupRoutine());
    }


    IEnumerator SpawnEnemyRountine()
    {
        while (_StopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_Enemyprefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }

    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_StopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 7, 0);
            int RandomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[RandomPowerup], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));

        }
    }

    public void OnPlayerDeath()
    {
        _StopSpawning = true;



    }


}
