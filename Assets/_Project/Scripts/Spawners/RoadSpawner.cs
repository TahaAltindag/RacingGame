using UnityEngine;

namespace _Project.Scripts
{
    // Spawns road segments as the player moves forward
    public sealed class RoadSpawner : Spawner
    {
        #region Fields

        [Header("Road Settings")]
        [SerializeField]
        private float roadLength = 5f; // Length of each road segment

        private float nextSpawnZ; // Next Z position to spawn a road segment

        #endregion

        #region Protected Methods

        protected override void InitializeSpawning()
        {
            float playerZ = playerTransform.position.z;
            // Start spawning a few segments behind the player
            nextSpawnZ = Mathf.Floor(playerZ / roadLength) * roadLength - roadLength * 2;
        }

        protected override void HandleSpawning(float playerZ)
        {
            while (nextSpawnZ < playerZ + spawnDistanceAhead)
            {
                SpawnObjectsAt(nextSpawnZ);
                nextSpawnZ += roadLength;
            }
        }

        protected override void DespawnObjectsBehind(float zLimit)
        {
            foreach (GameObject obj in objectPool)
            {
                if (obj.activeSelf && obj.transform.position.z < zLimit)
                {
                    obj.SetActive(false);
                }
            }
        }

        #endregion

        #region Private Methods

        // Spawns a road segment at the specified Z position
        private void SpawnObjectsAt(float zPosition)
        {
            GameObject roadSegment = GetPooledObject();
            roadSegment.transform.position = new Vector3(0f, 0f, zPosition);
            roadSegment.transform.rotation = Quaternion.identity;
            roadSegment.SetActive(true);
        }

        #endregion
    }
}