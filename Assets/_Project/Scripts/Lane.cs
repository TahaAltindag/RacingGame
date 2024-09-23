using UnityEngine;

namespace _Project.Scripts
{
    /// <summary>
    /// Represents a lane in which NPC cars can spawn.
    /// </summary>
    [System.Serializable]
    public class Lane
    {
        public float xPosition;            // The X-axis position of the lane
        public float speed = 10f;          // Speed at which NPC cars in this lane will move

        // Spawning variables for this lane
        [HideInInspector] public float nextSpawnZ;   // Next Z-axis position to spawn a car
        public float minSpawnInterval = 20f;         // Minimum distance between spawns
        public float maxSpawnInterval = 40f;         // Maximum distance between spawns
        public float spawnChance = 0.5f;             // Probability of spawning a car at each interval

        // Note: This class is designed to be serialized and configured in the Unity Inspector
    }
}