using UnityEngine;

namespace _Project.Scripts
{
    /// <summary>
    /// Controls the camera to follow and orbit around the target object (e.g., the player's vehicle).
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform target;   // The target to follow (e.g., the player's vehicle)
        [SerializeField] private float distance = 5.0f;  // Distance from the target
        [SerializeField] private float xSpeed = 120.0f;  // Speed of horizontal rotation
        [SerializeField] private float ySpeed = 120.0f;  // Speed of vertical rotation

        private float x = 0.0f;  // Current horizontal rotation angle
        private float y = 0.0f;  // Current vertical rotation angle

        #endregion

        #region Unity Methods

        private void Start()
        {
            // Initialize rotation angles based on the current rotation
            var angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;
        }

        private void LateUpdate()
        {
            if (target)
            {
                // Adjust camera angles based on mouse input
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                // Create rotation quaternion from angles
                var rotation = Quaternion.Euler(y, x, 0);

                // Calculate position based on rotation and distance from the target
                var position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

                // Apply rotation and position to the camera
                transform.rotation = rotation;
                transform.position = position;
            }
        }

        #endregion
    }
}