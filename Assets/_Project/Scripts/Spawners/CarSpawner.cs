using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    // Spawns NPC cars in specified lanes
    public sealed class CarSpawner : Spawner
    {
        #region Fields

        [Header("Car Settings")]
        [SerializeField]
        private List<Lane> lanes; // List of lanes where cars can spawn

        #endregion

        #region Protected Methods

        protected override void InitializeSpawning()
        {
            float playerZ = playerTransform.position.z;
            foreach (var lane in lanes)
            {
                lane.nextSpawnZ = playerZ + Random.Range(lane.minSpawnInterval, lane.maxSpawnInterval);
            }
        }

        protected override void HandleSpawning(float playerZ)
        {
            foreach (var lane in lanes)
            {
                while (lane.nextSpawnZ < playerZ + spawnDistanceAhead)
                {
                    if (ShouldSpawn(lane))
                    {
                        SpawnCarAtLane(lane, lane.nextSpawnZ);
                    }

                    lane.nextSpawnZ += GetNextSpawnInterval(lane);
                }
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

        // Spawns an NPC car in the specified lane at the given Z position
        private void SpawnCarAtLane(Lane lane, float zPosition)
        {
            Vector3 spawnPosition = new Vector3(lane.xPosition, 0f, zPosition);
            GameObject npcCar = GetPooledObject();
            npcCar.transform.position = spawnPosition;
            npcCar.transform.rotation = Quaternion.identity;
            npcCar.SetActive(true);

            // Initialize NPC car speed
            NPCVehicleController npcController = npcCar.GetComponent<NPCVehicleController>();
            if (npcController != null)
            {
                npcController.Initialize(lane.speed);
            }
        }

        // Determines whether to spawn a car based on spawn chance
        private bool ShouldSpawn(Lane lane)
        {
            return Random.value < lane.spawnChance;
        }

        // Calculates the next spawn interval for a lane
        private float GetNextSpawnInterval(Lane lane)
        {
            return Random.Range(lane.minSpawnInterval, lane.maxSpawnInterval);
        }

        #endregion
    }
}
