using System;
using System.Collections.Generic;
using Pool;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Enemy spawn positions")]
    [SerializeField] private List<GameObject> positions = new();
    
    private ObjectPool objectPool;
    
    private void Start()
    {
        objectPool = ObjectPool.Instance;
    }

    private void SpawnEnemy()
    {
        foreach (GameObject position in positions)
        {
            objectPool.SpawnFromPool("EnemyPool",
                position.transform.position, Quaternion.identity);
            
            Debug.Log("Enemy has been spawned at: " 
                      + position.transform.position + " position");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke(nameof(SpawnEnemy), 0.5f);
            Debug.Log("Player has been spoted");
        }
    }

    private void OnDrawGizmosSelected()
    {
        foreach (GameObject position in positions)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(position.transform.position,3f);
        }
    }
}
