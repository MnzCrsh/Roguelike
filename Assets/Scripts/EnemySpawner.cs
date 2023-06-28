using System;
using System.Collections.Generic;
using Enemy;
using Pool;
using UnityEngine;
using Zenject;

namespace RoomEvents
{
    [RequireComponent(typeof(GameObject))]
    public class EnemySpawner : MonoBehaviour
    {
        public static event Action OnRoomCleared;

        [SerializeField] private ObjectPool objectPool;
        [SerializeField] private EnemyBehavior enemyBehavior;

        [Tooltip("Enemy spawn positions")]
        [SerializeField] private List<GameObject> positions = new();
        [SerializeField] private float spawnDelay = 1f;
        
        [SerializeField] private int enemyCount;
        
        [Inject] private void Construct(EnemyBehavior enemy) => enemyBehavior = enemy;

        private void Start()
        {
            enemyBehavior.OnDied += OnEnemyValueChanged;
            
            objectPool = ObjectPool.Instance;
        }

        private void SpawnEnemy()
        {
            foreach (GameObject position in positions)
            {
                ++enemyCount;
                
                objectPool.SpawnFromPool("EnemyPool",
                    position.transform.position, Quaternion.identity);
            }
            print("Number of spawned enemies: " + enemyCount);
        }

        private void OnEnemyValueChanged()
        {
            --enemyCount;
            print("Enemy count " + enemyCount);
            
            CheckEnemyState();
        }
        
        private void CheckEnemyState()
        {
            if (enemyCount <= 0)
            {
                OnRoomCleared?.Invoke();
                enemyBehavior.OnDied -= OnEnemyValueChanged;
            }
        }

        private void CheckPlayerCollision(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }
            
            DisableTriggerZone();
            Invoke(nameof(SpawnEnemy), spawnDelay);
        }

        private void DisableTriggerZone()
        {
            //Disables collider so it wouldn't activate twice
            GetComponent<Collider>().enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            CheckPlayerCollision(other);
        }
    }
}