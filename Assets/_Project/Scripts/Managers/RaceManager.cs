using System;
using Cysharp.Threading.Tasks; // For async tasks
using DG.Tweening; // For animations
using TMPro; // For text UI
using UnityEngine;

namespace _Project.Scripts
{
    // Manages the race state, including countdown and timing
    public class RaceManager : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private TMP_Text countdownText; // Reference to the UI text for the countdown

        // Events to notify other components about race state changes
        public static event Action OnRaceStart;
        public static event Action OnRaceEnd;
        public static event Action<float> OnRaceTimeUpdated;

        private float raceTime = 0f; // Tracks the elapsed race time
        private bool raceOngoing = false; // Indicates if the race is currently ongoing

        // Singleton instance of the RaceManager
        public static RaceManager Instance { get; private set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Initialize singleton instance
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            // Start the countdown coroutine
            StartCountdownAsync().Forget();
        }

        private void Update()
        {
            if (raceOngoing)
            {
                // Increment race time and notify subscribers
                raceTime += Time.deltaTime;
                OnRaceTimeUpdated?.Invoke(raceTime);
            }
        }

        #endregion

        #region Public Methods

        // Asynchronous method to handle the race countdown
        public async UniTaskVoid StartCountdownAsync()
        {
            int countdown = 5;
            while (countdown > 0)
            {
                // Update the countdown text and animate it
                countdownText.text = countdown.ToString();
                countdownText.transform.localScale = Vector3.one * 2;
                countdownText.transform.DOScale(1, 0.5f).SetEase(Ease.OutBounce);
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                countdown--;
            }

            // Display "START!" and notify that the race has begun
            countdownText.text = "START!";
            countdownText.transform.localScale = Vector3.one * 2;
            countdownText.transform.DOScale(1, 0.5f).SetEase(Ease.OutBounce);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            countdownText.gameObject.SetActive(false);

            raceOngoing = true;
            OnRaceStart?.Invoke();
        }

        // Method to end the race and notify subscribers
        public void EndRace()
        {
            if (!raceOngoing) return;

            raceOngoing = false;
            OnRaceEnd?.Invoke();
        }

        #endregion
    }
}
