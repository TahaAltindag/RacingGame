using UnityEngine;

namespace _Project.Scripts
{
    /// <summary>
    /// Controls the movement of NPC (non-player character) vehicles.
    /// </summary>
    public class NPCVehicleController : MonoBehaviour
    {
        #region Fields

        private float speed = 10f; // Speed at which the NPC vehicle moves forward (m/s)

        #endregion

        #region Unity Methods

        private void Update()
        {
            // Move the NPC vehicle forward at a constant speed
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the NPC vehicle with a specific speed.
        /// </summary>
        /// <param name="npcSpeed">The speed to set for the NPC vehicle.</param>
        public void Initialize(float npcSpeed)
        {
            speed = npcSpeed;
        }

        #endregion
    }
}