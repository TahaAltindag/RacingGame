using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    // Spawns buildings along specified lines as the player moves
    public sealed class BuildingSpawner : Spawner
    {
        #region Fields

        [Header("Building Settings")]
        [SerializeField]
        private List<SpawningLine> spawningLines; // Lines along which buildings will be spawned

        [Header("Randomness")]
        [SerializeField]
        private float initialSpawnOffset = -10f; // Initial offset for spawning buildings

        #endregion

        #region Protected Methods

        protected override void InitializeSpawning()
        {
            float playerZ = playerTransform.position.z;
            // Initialize the next spawn Z position for each line
            foreach (var line in spawningLines)
            {
                line.nextSpawnZ = playerZ + initialSpawnOffset;
            }
        }

        protected override void HandleSpawning(float playerZ)
        {
            foreach (var line in spawningLines)
            {
                while (line.nextSpawnZ < playerZ + spawnDistanceAhead)
                {
                    if (ShouldSpawn(line))
                    {
                        SpawnObjectAtLine(line, line.nextSpawnZ);
                    }

                    line.nextSpawnZ += GetNextSpawnInterval(line);
                }
            }
        }

        protected override void DespawnObjectsBehind(float zLimit)
        {
            // Deactivate objects that are behind the zLimit
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

        // Spawns a building at a specific line and Z position
        private void SpawnObjectAtLine(SpawningLine line, float zPosition)
        {
            Vector3 spawnPosition = new Vector3(line.xPosition, line.yPosition, zPosition);
            GameObject building = GetPooledObject();
            building.transform.position = spawnPosition;
            building.transform.rotation = Quaternion.identity;
            building.SetActive(true);
        }

        // Determines whether to spawn an object based on spawn chance
        private bool ShouldSpawn(SpawningLine line)
        {
            return Random.value < line.spawnChance;
        }

        // Calculates the next spawn interval with optional randomness
        private float GetNextSpawnInterval(SpawningLine line)
        {
            if (line.spawnIntervalRandomness > 0f)
            {
                return line.spawnInterval + Random.Range(-line.spawnIntervalRandomness, line.spawnIntervalRandomness);
            }
            else
            {
                return line.spawnInterval;
            }
        }

        #endregion

        // Serializable class representing a line along which buildings can be spawned
        [System.Serializable]
        public class SpawningLine
        {
            public float xPosition; // X-axis position of the line
            public float yPosition = 0f; // Y-axis position (height)

            [HideInInspector]
            public float nextSpawnZ; // Next Z-axis position to spawn a building

            public float spawnInterval = 10f; // Base interval between spawns
            public float spawnIntervalRandomness = 0f; // Randomness added to the spawn interval
            public float spawnChance = 0.5f; // Probability of spawning a building at each interval
        }
    }
}
