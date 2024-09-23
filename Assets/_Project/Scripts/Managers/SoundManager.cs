using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace _Project.Scripts
{
    // Manages the game's sound effects, specifically the engine sound
    public class SoundManager : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private EventReference engineEventReference; // Reference to the engine sound event in FMOD

        private EventInstance engineEvent; // Instance of the engine sound event

        #endregion

        #region Unity Methods

        private void Start()
        {
            // Create and start the engine sound event
            engineEvent = RuntimeManager.CreateInstance(engineEventReference);
            RuntimeManager.AttachInstanceToGameObject(engineEvent, transform);
            engineEvent.start();

            // Subscribe to engine RPM changes to update the sound
            var engine = GetComponent<Engine>();
            engine.OnRPMChanged += OnRPMChanged;

            // Subscribe to race end event to stop the engine sound
            RaceManager.OnRaceEnd += StopEngineSound;
        }

        private void OnDestroy()
        {
            // Release the engine sound event and unsubscribe from events
            engineEvent.release();

            var engine = GetComponent<Engine>();
            if (engine != null)
            {
                engine.OnRPMChanged -= OnRPMChanged;
            }

            RaceManager.OnRaceEnd -= StopEngineSound;
        }

        #endregion

        #region Private Methods

        // Updates the engine sound based on RPM changes
        private void OnRPMChanged(float engineRPM)
        {
            // Normalize RPM value between 0 and 1 for FMOD parameter
            float normalizedRPM = Utility.NormalizeValue(engineRPM, 1000f, 9000f);
            engineEvent.setParameterByName("RPM", normalizedRPM);
        }

        // Stops the engine sound when the race ends
        private void StopEngineSound()
        {
            engineEvent.stop(STOP_MODE.IMMEDIATE);
        }

        #endregion
    }
}
