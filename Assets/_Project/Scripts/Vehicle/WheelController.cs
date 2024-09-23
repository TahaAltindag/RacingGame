using UnityEngine;

namespace _Project.Scripts
{
    /// <summary>
    /// Controls the rotation of the vehicle's wheels based on the current speed.
    /// </summary>
    public class WheelController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform[] wheelTransforms; // Array of wheel transforms to rotate
        [SerializeField] private float wheelRadius = 0.34f;   // Radius of the wheels (m)

        #endregion

        #region Unity Methods

        private void Start()
        {
            // Subscribe to speed changes from the VehicleController
            VehicleController.OnSpeedChanged += OnSpeedChanged;
        }

        private void OnDestroy()
        {
            // Unsubscribe from events to prevent memory leaks
            VehicleController.OnSpeedChanged -= OnSpeedChanged;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Called when the vehicle's speed changes. Rotates the wheels accordingly.
        /// </summary>
        /// <param name="currentSpeed">The current speed of the vehicle (m/s).</param>
        private void OnSpeedChanged(float currentSpeed)
        {
            RotateWheels(currentSpeed);
        }

        /// <summary>
        /// Rotates the wheels based on the current speed.
        /// </summary>
        /// <param name="currentSpeed">The current speed of the vehicle (m/s).</param>
        private void RotateWheels(float currentSpeed)
        {
            float rotationAngle = Utility.CalculateWheelRotationAngle(currentSpeed, Time.deltaTime, wheelRadius);

            foreach (Transform wheel in wheelTransforms)
            {
                // Rotate the wheel around its local X-axis
                wheel.Rotate(Vector3.right, rotationAngle);
            }
        }

        #endregion
    }
}