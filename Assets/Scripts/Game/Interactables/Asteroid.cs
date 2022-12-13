using JellyButton.Exam.Core.GameEvents;
using JellyButton.Exam.Core.ObjectPooling;
using UnityEngine;

namespace JellyButton.Exam.Game.Interactables
{
    /// <summary>
    /// Responsible for spinning the asteroid and checking if it has passed the spaceship.
    /// Also implements methods that allow "outsiders" to interact with it.
    /// </summary>
    public class Asteroid : Pooled, IInteractable
    {
        [SerializeField] private float _rotationSpeed = 100f;

        [Space] [Header("Game Events")]
        [SerializeField] private GameEvent _onHitAsteroid;
        [SerializeField] private GameEvent _onPassedAsteroid;
        
        private Transform _transform;
        private bool _passedSpaceship;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnDisable()
        {
            _passedSpaceship = false;
        }

        /// <summary>
        /// Might be called on collision. Results in game over.
        /// </summary>
        public void Interact()
        {
            _onHitAsteroid.Raise();
        }

        private void Update()
        {
            Spin();
            CheckIfPassedSpaceship();
        }

        private void CheckIfPassedSpaceship()
        {
            if (_passedSpaceship || !(_transform.position.z < -1)) return;
            
            _passedSpaceship = true;
            _onPassedAsteroid.Raise();
        }

        private void Spin()
        {
            var currentRotation = _transform.rotation.eulerAngles;
            var currentYAxisAngle = currentRotation.y >= 360 ? 0 : currentRotation.y;
            var desiredYAxisAngle = currentYAxisAngle + Time.deltaTime * _rotationSpeed;
            _transform.rotation = Quaternion.Euler(currentRotation.x, desiredYAxisAngle, currentRotation.z);
        }
    }
}
