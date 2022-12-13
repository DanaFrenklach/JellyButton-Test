using JellyButton.Exam.Core.Data;
using UnityEngine;

namespace JellyButton.Exam.Game.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        
        [Space][Header("Distance Settings")]
        [SerializeField] private float _distance = 5f;
        [SerializeField] private float _distanceOnBoostMultiplier = 0.5f;
        [SerializeField] private float _distanceDamping = 8.0f;
        [SerializeField] private float _distanceDampingOnBoost = 3.0f;
        
        [Space][Header("Height Settings")]
        [SerializeField] private float _height = 2f;
        [SerializeField] private float _heightOnBoostMultiplier = 0.5f;
        [SerializeField] private float _heightDamping = 8.0f;
        [SerializeField] private float _heightDampingOnBoost = 3.0f;

        [Space] [Header("Field of View Settings")]
        [SerializeField] private float _fieldOfView = 60f;
        [SerializeField] private float _fieldOfViewOnBoost = 62f;
        [SerializeField] private float _fieldOfViewDamping = 8.0f;
        [SerializeField] private float _fieldOfViewDampingOnBoost = 3.0f;
        
        [Space][Header("Rotation Settings")]
        [SerializeField] private float _rotation = 0.4f;
        [SerializeField] private float _rotationDamping = 2.0f;

        private Transform _transform;
        private UnityEngine.Camera _camera;

        private void Awake()
        {
            // Early out if we don't have a target
            if (!_target)
            {
                enabled = false;
            }
            
            _transform = transform;
            _camera = GetComponent<UnityEngine.Camera>();
        }

        private void LateUpdate()
        {
            Follow();
        }

        private void Follow()
        {
            var currentPosition = _transform.position;
            var targetPosition = _target.position;

            // Calculate the current rotation angles
            var wantedRotationAngle = _target.eulerAngles.y;
            var currentRotationAngle = _transform.eulerAngles.y;
            
            // Calculate desired height and distance from the target
            var wantedHeight = targetPosition.y + _height * (GlobalData.IsSpeeding ? _heightOnBoostMultiplier : 1);
            var wantedDistance = _distance * (GlobalData.IsSpeeding ? _distanceOnBoostMultiplier : 1);

            // Calculate current height and distance
            var currentHeight = currentPosition.y;
            var currentDistance = targetPosition.z - currentPosition.z;

            // Damp the rotation around the y-axis
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping * Time.deltaTime);

            // Damp the height
            var heightDamp = GlobalData.IsSpeeding ? _heightDampingOnBoost : _heightDamping;
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamp * Time.deltaTime);
            
            // Damp the distance
            var distanceDamp = GlobalData.IsSpeeding ? _distanceDampingOnBoost : _distanceDamping;
            currentDistance = Mathf.Lerp(currentDistance, wantedDistance, distanceDamp * Time.deltaTime);
            
            // Calculate the field of view
            var wantedFieldOfView = GlobalData.IsSpeeding ? _fieldOfViewOnBoost : _fieldOfView;
            var currentFieldOfView = _camera.fieldOfView;
            var fieldOfViewDamp = GlobalData.IsSpeeding ? _fieldOfViewDampingOnBoost : _fieldOfViewDamping;
            currentFieldOfView = Mathf.Lerp(currentFieldOfView, wantedFieldOfView, fieldOfViewDamp * Time.deltaTime);

            // Convert the angle into a rotation
            var currentRotation = Quaternion.Euler(0, currentRotationAngle / 2, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            currentPosition = targetPosition - currentRotation * Vector3.forward * currentDistance;
            currentPosition.y = currentHeight;
            _transform.position = currentPosition;
            
            // Set the current field of view
            _camera.fieldOfView = currentFieldOfView;

            // Always look at the target
            _transform.LookAt(_target);
            
            // further control over the viewing angle
            _transform.rotation = Quaternion.Euler(_transform.rotation.eulerAngles * _rotation);
        }
    }
}