using UnityEngine;

namespace _Project.Scripts
{
    /// <summary>
    /// Controls the movement of a UI needle (e.g., speedometer or RPM gauge) based on vehicle data.
    /// </summary>
    public class NeedleController : MonoBehaviour
    {
        #region Fields

        [Header("Needle Settings")]
        [SerializeField] private RectTransform needleTransform;  // UI element representing the needle

        [SerializeField] private float minAngle = -130f;         // Angle corresponding to the minimum value
        [SerializeField] private float maxAngle = 50f;           // Angle corresponding to the maximum value
        [SerializeField] private float minValue = 0f;            // Minimum value (e.g., 0 km/h or 0 RPM)
        [SerializeField] private float maxValue = 260f;          // Maximum value (e.g., max speed or max RPM)

        [Header("Smoothing")]
        [SerializeField] private float smoothTime = 0.1f;        // Time for the needle movement to smooth out

        [Header("Event Source")]
        [SerializeField] private VehicleController vehicleController; // Reference to the VehicleController
        [SerializeField] private Engine engine;                       // Reference to the Engine component
        [SerializeField] private NeedleType needleType;               // Type of needle (Speedometer or RPM)

        private float currentValue = 0f;    // Current value to display
        private float needleVelocity = 0f;  // Velocity reference for smoothing

        #endregion

        /// <summary>
        /// Specifies the type of needle to control.
        /// </summary>
        public enum NeedleType
        {
            Speedometer,
            RPM
        }

        #region Unity Methods

        private void OnEnable()
        {
            // Subscribe to the appropriate event based on the needle type
            switch (needleType)
            {
                case NeedleType.Speedometer when vehicleController != null:
                    VehicleController.OnSpeedChanged += UpdateNeedle;
                    break;
                case NeedleType.RPM when engine != null:
                    engine.OnRPMChanged += UpdateNeedle;
                    break;
            }
        }

        private void OnDisable()
        {
            // Unsubscribe from events to prevent memory leaks
            switch (needleType)
            {
                case NeedleType.Speedometer when vehicleController != null:
                    VehicleController.OnSpeedChanged -= UpdateNeedle;
                    break;
                case NeedleType.RPM when engine != null:
                    engine.OnRPMChanged -= UpdateNeedle;
                    break;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the needle based on the new value (speed or RPM).
        /// </summary>
        /// <param name="newValue">The new value to display.</param>
        private void UpdateNeedle(float newValue)
        {
            currentValue = newValue;

            // Convert speed from m/s to km/h if displaying speed
            if (needleType == NeedleType.Speedometer)
            {
                currentValue = Utility.MStoKmH(currentValue);
            }

            UpdateNeedleRotation();
        }

        /// <summary>
        /// Calculates and applies the rotation to the needle based on the current value.
        /// </summary>
        private void UpdateNeedleRotation()
        {
            // Normalize the value to a 0-1 range
            float normalizedValue = Mathf.InverseLerp(minValue, maxValue, currentValue);

            // Calculate the target angle based on the normalized value
            float targetAngle = Mathf.Lerp(minAngle, maxAngle, normalizedValue);

            // Smoothly rotate the needle to the target angle
            float smoothAngle = Mathf.SmoothDampAngle(needleTransform.localEulerAngles.z, targetAngle,
                ref needleVelocity, smoothTime);

            needleTransform.localRotation = Quaternion.Euler(0f, 0f, smoothAngle);
        }

        #endregion
    }
}
