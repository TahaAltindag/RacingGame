using System;
using UnityEngine;

namespace _Project.Scripts
{
    /// <summary>
    /// Controls the vehicle's movement based on player input and manages race state.
    /// </summary>
    public class VehicleController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float acceleration = 5f;    // Acceleration rate (m/s^2)
        [SerializeField] private float deceleration = 10f;   // Deceleration rate (m/s^2)
        [SerializeField] private Transform finishLine;       // Reference to the finish line

        public static event Action<float> OnSpeedChanged;    // Event triggered when speed changes

        private float currentSpeed = 0f;     // Current speed of the vehicle (m/s)
        private bool raceStarted = false;    // Indicates if the race has started

        private IInputHandler inputHandler;  // Reference to the input handler

        #endregion

        #region Unity Methods

        private void Start()
        {
            inputHandler = GetComponent<IInputHandler>();

            // Subscribe to race events from the RaceManager
            RaceManager.OnRaceStart += StartRace;
            RaceManager.OnRaceEnd += StopVehicle;
        }

        private void Update()
        {
            if (raceStarted)
            {
                HandleMovement();
                CheckFinishLine();
            }
        }

        private void OnDestroy()
        {
            // Unsubscribe from events to prevent memory leaks
            RaceManager.OnRaceStart -= StartRace;
            RaceManager.OnRaceEnd -= StopVehicle;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles vehicle acceleration, deceleration, and movement.
        /// </summary>
        private void HandleMovement()
        {
            // Handle acceleration and deceleration based on player input
            if (inputHandler.Throttle > 0f)
            {
                currentSpeed += acceleration * inputHandler.Throttle * Time.deltaTime;
            }
            else if (inputHandler.Brake > 0f)
            {
                currentSpeed -= deceleration * inputHandler.Brake * Time.deltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0f);
            }
            else
            {
                // Apply natural deceleration when no input is given
                currentSpeed -= deceleration * 0.5f * Time.deltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0f);
            }

            // Move the vehicle forward
            MoveVehicle();

            // Notify subscribers about the speed change
            OnSpeedChanged?.Invoke(currentSpeed);
        }

        /// <summary>
        /// Moves the vehicle forward based on the current speed.
        /// </summary>
        private void MoveVehicle()
        {
            Vector3 forwardMovement = transform.forward * (currentSpeed * Time.deltaTime);
            transform.Translate(forwardMovement, Space.World);
        }

        /// <summary>
        /// Checks if the vehicle has crossed the finish line.
        /// </summary>
        private void CheckFinishLine()
        {
            if (transform.position.z >= finishLine.position.z)
            {
                raceStarted = false;
                RaceManager.Instance.EndRace();
            }
        }

        /// <summary>
        /// Called when the race starts.
        /// </summary>
        private void StartRace()
        {
            raceStarted = true;
        }

        /// <summary>
        /// Stops the vehicle when the race ends.
        /// </summary>
        private void StopVehicle()
        {
            raceStarted = false;
            currentSpeed = 0f;
            OnSpeedChanged?.Invoke(currentSpeed);
        }

        #endregion
    }
}
