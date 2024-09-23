using UnityEngine;

namespace _Project.Scripts
{
    // Handles player input and implements the IInputHandler interface
    public class InputHandler : MonoBehaviour, IInputHandler
    {
        #region Fields

        // Public properties to expose throttle and brake inputs
        public float Throttle { get; private set; }
        public float Brake { get; private set; }

        #endregion

        #region Unity Methods

        // Called once per frame to update input values
        private void Update()
        {
            // Set Throttle to 1 if 'W' key is pressed, else 0
            Throttle = Input.GetKey(KeyCode.W) ? 1f : 0f;
            // Set Brake to 1 if 'S' key is pressed, else 0
            Brake = Input.GetKey(KeyCode.S) ? 1f : 0f;
        }

        #endregion
    }
}