using System.Collections;
using System.Collections.Generic;
using Capnp;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float WaitRespawnTime;
    public float Radius;
    public GameObject EnemyArrow;
    public static EnemySpawning Instance;
    [HideInInspector]
    public bool LookingAtEnemy = false;
        
    private Vector3 _randomPos;
    private GameObject _enemytoLookAt;

    private void Start()
    {
        _enemytoLookAt = null;
        Instance = this;
        LookingAtEnemy = false;
        EnemyArrow.SetActive(false);
        StartCoroutine("EnemySpawnRoutine");
    }

    private void Update()
    {
        if(LookingAtEnemy)
            EnemyArrow.SetActive(true);
        else
        {
            GameObject enemy = NewEnemyFound();
            if (enemy != null)
                _enemytoLookAt = enemy;
            else
                EnemyArrow.SetActive(false);
        }
        
        if(_enemytoLookAt != null)
            EnemyArrow.transform.LookAt(_enemytoLookAt.transform);
    }


    IEnumerator EnemySpawnRoutine()
    {
        while (true)
        {
            _randomPos = (Random.onUnitSphere + new Vector3(0.0f, 0.0f, -1.0f)) * Radius;

            GameObject enemey = Instantiate(EnemyPrefab, _randomPos, Quaternion.identity);

            if (!LookingAtEnemy)
            {
                LookingAtEnemy = true;
                _enemytoLookAt = enemey;

            }

            yield return new WaitForSeconds(WaitRespawnTime);
        }
        
    }

    private GameObject NewEnemyFound()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
            return enemy;
        else
            return null;
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
