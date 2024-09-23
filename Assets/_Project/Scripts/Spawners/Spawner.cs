using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    // Abstract base class for spawning objects in the game world
    public abstract class Spawner : MonoBehaviour
    {
        #region Fields

        [Header("Prefabs")]
        [SerializeField]
        protected List<GameObject> prefabs; // List of prefabs to spawn

        [Header("Object Pooling")]
        [SerializeField]
        protected int poolSize = 50; // Size of the object pool

        protected Queue<GameObject> objectPool; // Queue for object pooling

        [Header("Spawning Settings")]
        [SerializeField]
        protected float spawnDistanceAhead = 100f; // Distance ahead of the player to spawn objects

        [SerializeField]
        protected float despawnDistanceBehind = 50f; // Distance behind the player to despawn objects

        [Header("References")]
        [SerializeField]
        protected Transform playerTransform; // Reference to the player's transform

        private float lastPlayerZ; // Last recorded Z position of the player

        #endregion

        #region Unity Methods

        protected virtual void Start()
        {
            InitializePool();
            lastPlayerZ = playerTransform.position.z;
            InitializeSpawning();
        }

        protected virtual void Update()
        {
            float playerZ = playerTransform.position.z;

            // Handle spawning of new objects
            HandleSpawning(playerZ);

            // Despawn objects that are far behind the player
            if (playerZ - lastPlayerZ >= despawnDistanceBehind)
            {
                DespawnObjectsBehind(playerZ - despawnDistanceBehind);
                lastPlayerZ = playerZ;
            }
        }

        #endregion

        #region Private Methods

        // Initializes the object pool with inactive game objects
        private void InitializePool()
        {
            objectPool = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
        }

        #endregion

        #region Protected Methods

        // Retrieves an object from the pool or creates a new one if necessary
        protected GameObject GetPooledObject()
        {
            if (objectPool.Count > 0)
            {
                GameObject obj = objectPool.Dequeue();
                objectPool.Enqueue(obj);
                return obj;
            }
            else
            {
                GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
                objectPool.Enqueue(obj);
                return obj;
            }
        }

        // Abstract methods for derived classes to implement specific spawning logic
        protected abstract void InitializeSpawning();
        protected abstract void HandleSpawning(float playerZ);
        protected abstract void DespawnObjectsBehind(float zLimit);

        #endregion
    }
}
