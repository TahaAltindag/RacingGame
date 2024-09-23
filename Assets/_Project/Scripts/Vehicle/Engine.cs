using System;
using UnityEngine;

namespace _Project.Scripts
{
    /// <summary>
    /// Handles the engine logic, including RPM calculation and gear shifting.
    /// </summary>
    public class Engine : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float maxRPM = 7000f; // Maximum engine RPM
        [SerializeField] private float minRPM = 1000f; // Minimum engine RPM
        [SerializeField] private float[] gearSpeeds;   // Max speeds for each gear (km/h)

        public event Action<float> OnRPMChanged; // Event triggered when RPM changes
        public event Action<int> OnGearChanged;  // Event triggered when gear changes

        private int currentGear = 1;      // Current gear number
        private float engineRPM = 1000f;  // Current engine RPM

        private SoundManager soundManager; // Reference to the SoundManager component

        #endregion

        #region Unity Methods

        private void Start()
        {
            // Convert gear max speeds from km/h to m/s
            for (int i = 0; i < gearSpeeds.Length; i++)
            {
                gearSpeeds[i] = Utility.KmHToMS(gearSpeeds[i]);
            }

            soundManager = GetComponent<SoundManager>();

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
        /// Called when the vehicle's speed changes. Updates RPM and handles gear shifts.
        /// </summary>
        /// <param name="currentSpeed">The current speed of the vehicle (m/s).</param>
        private void OnSpeedChanged(float currentSpeed)
        {
            UpdateEngineRPM(currentSpeed);
            HandleGearShifts();
        }

        /// <summary>
        /// Updates the engine RPM based on the current speed and gear.
        /// </summary>
        /// <param name="currentSpeed">The current speed of the vehicle (m/s).</param>
        private void UpdateEngineRPM(float currentSpeed)
        {
            float maxGearSpeed = gearSpeeds[currentGear - 1];
            engineRPM = minRPM + (currentSpeed / maxGearSpeed) * (maxRPM - minRPM);
            engineRPM = Mathf.Clamp(engineRPM, minRPM, maxRPM);

            // Invoke the RPM changed event
            OnRPMChanged?.Invoke(engineRPM);
        }

        /// <summary>
        /// Handles automatic gear shifting based on engine RPM.
        /// </summary>
        private void HandleGearShifts()
        {
            // Shift up if RPM exceeds 95% of maxRPM and not already in top gear
            if (currentGear < gearSpeeds.Length && engineRPM >= maxRPM * 0.95f)
            {
                currentGear++;
                engineRPM = minRPM + (engineRPM - maxRPM * 0.95f);
                OnGearChanged?.Invoke(currentGear);
            }
            // Shift down if RPM falls below 105% of minRPM and not already in first gear
            else if (currentGear > 1 && engineRPM <= minRPM * 1.05f)
            {
                currentGear--;
                engineRPM = maxRPM - (minRPM * 1.05f - engineRPM);
                OnGearChanged?.Invoke(currentGear);
            }
        }

        #endregion
    }
}
