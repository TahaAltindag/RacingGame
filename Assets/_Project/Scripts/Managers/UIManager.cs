using UnityEngine;
using TMPro;

namespace _Project.Scripts
{
    // Manages the game's UI elements like speedometer, RPM display, gear indicator, and timer
    public class UIManager : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private TMP_Text speedometerText; // UI text for speed display
        [SerializeField]
        private TMP_Text rpmText; // UI text for RPM display
        [SerializeField]
        private TMP_Text gearText; // UI text for gear display
        [SerializeField]
        private TMP_Text timerText; // UI text for timer display

        private Engine engine; // Reference to the Engine component

        #endregion

        #region Unity Methods

        private void Start()
        {
            engine = GetComponent<Engine>();

            // Subscribe to events to update UI elements
            VehicleController.OnSpeedChanged += UpdateSpeed;

            if (engine != null)
            {
                engine.OnRPMChanged += UpdateRPM;
                engine.OnGearChanged += UpdateGear;
            }

            RaceManager.OnRaceTimeUpdated += UpdateTimer;
            RaceManager.OnRaceEnd += DisplayRaceEnd;
        }

        private void OnDestroy()
        {
            // Unsubscribe from events
            VehicleController.OnSpeedChanged -= UpdateSpeed;

            if (engine != null)
            {
                engine.OnRPMChanged -= UpdateRPM;
                engine.OnGearChanged -= UpdateGear;
            }

            RaceManager.OnRaceTimeUpdated -= UpdateTimer;
            RaceManager.OnRaceEnd -= DisplayRaceEnd;
        }

        #endregion

        #region Private Methods

        // Updates the speedometer UI text
        private void UpdateSpeed(float currentSpeed)
        {
            float speedKmh = Utility.MStoKmH(currentSpeed);
            speedometerText.text = $"Speed: {speedKmh:F1} km/h";
        }

        // Updates the RPM UI text
        private void UpdateRPM(float engineRPM)
        {
            rpmText.text = "RPM: " + engineRPM.ToString("F0");
        }

        // Updates the gear UI text
        private void UpdateGear(int currentGear)
        {
            gearText.text = currentGear.ToString();
        }

        // Updates the race timer UI text
        private void UpdateTimer(float raceTime)
        {
            timerText.text = "Time: " + raceTime.ToString("F2") + " s";
        }

        // Displays a message when the race ends
        private void DisplayRaceEnd()
        {
            timerText.text += " - Race Finished!";
        }

        #endregion
    }
}
