using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pool
{
    public class ObjectPool : MonoBehaviour
    {
        #region Singleton

        public static ObjectPool Instance;
        private void Awake()
        {
            Instance = this;
        }

        #endregion

        private Dictionary<string, Queue<GameObject>> objectPoolDictionary = new ();
        
        [Tooltip("List of object pools")]
        [SerializeField] private List<MPool> pools;


        [Inject] private DiContainer diContainer;

        private void Start()
        {
            CreatePoolQueue();
        }

        /// <summary>
        /// Creates object pool queue filled with GameObject
        /// </summary>
        private void CreatePoolQueue()
        {
            foreach (MPool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                CachePoolObjects(pool, objectPool);
                objectPoolDictionary.Add(pool.poolTag, objectPool);
                
            }
        }

        /// <summary>
        /// Instantiate GO then disable them
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="objectPool"></param>
        private void CachePoolObjects(MPool pool, Queue<GameObject> objectPool)
        {
            for (int i = 0; i < pool.size; i++)
            {
                GameObject poolObject = diContainer.InstantiatePrefab(pool.prefab);
                poolObject.SetActive(false);

                objectPool.Enqueue(poolObject);
            }
        }

        /// <summary>
        /// Spawn GameObject from pool with the reseted parameters 
        /// </summary>
        /// <param name="poolTag"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public GameObject SpawnFromPool(string poolTag, Vector3 position, Quaternion rotation)
        {
            if (!objectPoolDictionary.ContainsKey(poolTag))
            {
                Debug.LogError("Pool with pool tag: " + poolTag + " doesn't exist");
                return null;
            }
            GameObject objectToSpawn =  objectPoolDictionary[poolTag].Dequeue();
            
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            
            objectPoolDictionary[poolTag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
    }
}