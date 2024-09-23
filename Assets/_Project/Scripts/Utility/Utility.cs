using UnityEngine;

namespace _Project.Scripts
{
    /// <summary>
    /// Provides utility functions for unit conversions and calculations.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Converts speed from kilometers per hour (km/h) to meters per second (m/s).
        /// </summary>
        /// <param name="kmh">Speed in kilometers per hour.</param>
        /// <returns>Speed in meters per second.</returns>
        public static float KmHToMS(float kmh)
        {
            return kmh / 3.6f;
        }

        /// <summary>
        /// Converts speed from meters per second (m/s) to kilometers per hour (km/h).
        /// </summary>
        /// <param name="ms">Speed in meters per second.</param>
        /// <returns>Speed in kilometers per hour.</returns>
        public static float MStoKmH(float ms)
        {
            return ms * 3.6f;
        }

        /// <summary>
        /// Calculates the rotation angle for a wheel based on speed, time, and wheel radius.
        /// </summary>
        /// <param name="speed">Speed of the vehicle (m/s).</param>
        /// <param name="deltaTime">Time since last frame (s).</param>
        /// <param name="wheelRadius">Radius of the wheel (m).</param>
        /// <returns>Rotation angle in degrees.</returns>
        public static float CalculateWheelRotationAngle(float speed, float deltaTime, float wheelRadius)
        {
            float wheelCircumference = 2f * Mathf.PI * wheelRadius;
            return (speed * deltaTime) / wheelCircumference * 360f;
        }

        /// <summary>
        /// Normalizes a value within a specified range to a value between 0 and 1.
        /// </summary>
        /// <param name="value">The value to normalize.</param>
        /// <param name="min">Minimum value of the original range.</param>
        /// <param name="max">Maximum value of the original range.</param>
        /// <returns>Normalized value between 0 and 1.</returns>
        public static float NormalizeValue(float value, float min, float max)
        {
            return (value - min) / (max - min);
        }
    }
}
